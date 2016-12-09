using System.Net;
using System.Collections.Generic;
using PublicUtilities;

namespace PwdReset
{
    public class USPwdResetHttper : PwdResetHttperBase
    {
        private const string QuestionUrl = "https://us.battle.net/account/support/password-reset-secret-answer.html";


        public USPwdResetHttper(DetectionParamsItem paramsItem)
            : base(paramsItem)
        {
            //New query need cookie be empty;
            this.ClearCookie();
        }

        public override PwdResetItem GetItemState(PwdResetItem item)
        {
            if (null != item)
            {
                string html = string.Empty;
                GetLoginContent(item, ref html);
                if (!string.IsNullOrEmpty(html))
                {
                    int i = 0;
                    do
                    {
                        item.State = this.GetState(html);
                        if (item.IsValidName)
                        {
                            break;
                        }
                        else if (item.IsNeedCaptcha)
                        {
                            string captcha = string.Empty;
                            if (this.GetCatcha(html, ref captcha) && !string.IsNullOrEmpty(captcha))
                            {
                                this.GetLoginContentWithCaptcha(item, captcha, ref  html);
                            }
                        }

                    } while (i++ < this.DetectionParamsItem.ErrorRepeatCount);

                    if (item.IsValidName)
                    {
                        string question = this.GetQuestion(html);
                        PostAnswer(question);
                    }
                }
            }

            return item;
        }

        #region Answer Question

        private bool PostAnswer(string question)
        {
            if (!string.IsNullOrEmpty(question))
            {
                string answer = "test";
                string postData = string.Format(this.PostSecurityAnswerFormat, answer);
                string html = this.ReadUrlContent(QuestionUrl, postData);
                if (IsContains(html, "<strong>An error has occurred.</strong>", "Invalid secret question. Please try again.",
                    "發生了一個錯誤", "無效的提示答案。請重試一次"))
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        private Dictionary<string, IList<string>> answerDB = new Dictionary<string, IList<string>>();
        private void GetAnswers()
        {
        }

        #endregion

        #region Get Captcha
        HtmlParseHelper htmpParseHelper = new HtmlParseHelper();
        private bool GetCatcha(string html, ref string captchaCode)
        {
            if (!string.IsNullOrEmpty(html) && this.IsContains(html, "<img src=\"/account/captcha.jpg"))
            {
                string outterText = htmpParseHelper.GetOuterTextFromHtml("<img src=\"/account/captcha.jpg", "/>", 1, html);
                if (!string.IsNullOrEmpty(outterText))
                {
                    string srcUrl = htmpParseHelper.GetOutterPropertyFromHtml(outterText, "src");
                    if (!string.IsNullOrEmpty(srcUrl))
                    {
                        captchaCode = this.GetCaptchaCodeFormUrl(string.Format("{0}{1}", this.DetectionParamsItem.CurrentGameServer.DomainUrl, srcUrl));
                        return true;
                    }
                }

            }
            return false;
        }
        #endregion

        #region Get Question

        private string GetQuestion(string html)
        {
            string question = htmpParseHelper.GetOuterTextFromHtml("Secret Question:", "</p>", 1, html);
            if (!string.IsNullOrEmpty(question))
            {
                question = htmpParseHelper.GetInnerTextFromHtml("<p>", "</p>", question);
            }

            if (string.IsNullOrEmpty(question))
            {
                question = htmpParseHelper.GetOuterTextFromHtml("提示問題:", "</p>", 1, html);
                if (!string.IsNullOrEmpty(question))
                {
                    question = htmpParseHelper.GetInnerTextFromHtml("<p>", "</p>", question);
                }
            }
            return question;
        }

        protected bool GetLoginContent(PwdResetItem item, ref string content)
        {
            if (null != item)
            {
                string postData = string.Format(this.PostUserFormat, item.EMail, item.FirstName, item.SecondName);
                content = string.Empty;
                LogManager.Info(string.Format("-> post data:{0}", postData));

                content = this.ReadUrlContent(DetectionParamsItem.CurrentGameServer.PasswordResetUrl, postData);
                return true;
            }

            return false;
        }

        protected bool GetLoginContentWithCaptcha(PwdResetItem item, string captchaCode, ref string content)
        {
            if (null != item)
            {
                string postData = string.Format(this.PostContentWithCaptchaFormat, item.EMail, item.FirstName, item.SecondName, captchaCode);
                LogManager.Info(string.Format("-> post data:{0}", postData));
                content = string.Empty;
                content = this.ReadUrlContent(DetectionParamsItem.CurrentGameServer.PasswordResetUrl, postData);
                return true;
            }

            return false;
        }

        protected PwdResetState GetState(string content)
        {
            if (IsContains(content, "/account/captcha.jpg", "The security input you entered was invalid, please try again"))
            {
                return PwdResetState.NeedCatcha;
            }
            //选择游戏帐号
            else if (IsContains(content, "This e-mail/name combination does not exist", "此電子郵件地址或帳號名稱不存在"))
            {
                return PwdResetState.EmailOrNameNotExisted;
            }
            else if (IsContains(content, HttpHelperBase.HTTPERROR))
            {
                return PwdResetState.HTTPERROR;
            }

            else if (IsContains(content, "Answer my secret question", "回答我的安全提示問題"))
            {
                return PwdResetState.SecretQuestion;
            }
            return PwdResetState.Unknown;
        }

        #endregion
    }
}