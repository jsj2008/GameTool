using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using PublicUtilities;
using LitJson;

namespace WebDetection
{
    public enum HttpHeaderType
    {
        Normal,
        Json,
        XmlHttpRequest
    }

    public class USBattleCharGetter : GameHttperBase
    {
        protected List<BattleCharItem> characterList = new List<BattleCharItem>();
        protected int availableLeve = 0;
        protected GameServer GameServer = GameWowServers.USServer;
        protected HttpHeaderType httpHeaderType = HttpHeaderType.Normal;

        public USBattleCharGetter(int level, DetectionParamsItem paramsItem, LogManagerBase logManager,
            string cookieStr, CookieContainer cookieContainer)
            : base(paramsItem, logManager)
        {
            availableLeve = level;
            this.GameServer = paramsItem.CurrentGameServer;
            this.cookie = cookieStr;
            this.cookieContainer = cookieContainer;
        }

        protected override void SetHttpRequestGetHeader(HttpWebRequest httpRequest)
        {
            SetHttpRequestHeader(httpRequest, false, this.httpHeaderType);
        }

        protected virtual void SetHttpRequestHeader(HttpWebRequest httpRequest, bool isPost, HttpHeaderType headerType)
        {
            bool isSetXMLHttpRequest = false;
            if (isPost)
            {
                base.SetHttpRequestPostHeader(httpRequest);
                httpRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                isSetXMLHttpRequest = true;
                httpRequest.Accept = @"*/*";
                //httpRequest.Referer = @"http://eu.battle.net/wow/en/forum/";
                httpRequest.Referer = this.DetectionParamsItem.CurrentGameServer.BattleGetGUrl;
            }
            else
            {
                base.SetHttpRequestGetHeader(httpRequest);
            }

            switch (headerType)
            {
                case HttpHeaderType.Json:
                    httpRequest.UserAgent = @".NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; CIBA)";
                    httpRequest.Accept = @"application/json, text/javascript, */*; q=0.01";

                    if (!isSetXMLHttpRequest)
                        httpRequest.Headers.Add("x-requested-with", "XMLHttpRequest");
                    //httpRequest.Referer = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/";
                    httpRequest.Referer = this.DetectionParamsItem.CurrentGameServer.BattleGetGUrl;
                    httpRequest.ContentLength = 0;
                    break;
                case HttpHeaderType.XmlHttpRequest:
                    httpRequest.Accept = "*/*";
                    httpRequest.Referer = this.DetectionParamsItem.CurrentGameServer.BattleCharacterUrl;
                    httpRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                    break;
                default:
                    break;
            }
        }

        protected override void SetHttpRequestPostHeader(HttpWebRequest httpRequest)
        {
            SetHttpRequestHeader(httpRequest, true, this.httpHeaderType);
        }

        #region Get char details

        public List<BattleCharItem> GetCharacter()
        {
            string url = this.DetectionParamsItem.CurrentGameServer.BattleCharacterUrl;
            string html = this.ReadFromUrl(url);

            characterList.Clear();
            if (TextHelper.IsContains(html, "No game license was found. Please add one under your account", "No characters were found.") &&
                !TextHelper.IsContains(html, "<div class=\"no-results hide\">No characters were found</div>"))
            {
                return characterList;
            }
            //Remove duplicate chars
            int startIndex = html.IndexOf("Manage Characters");
            if (startIndex > 0)
            {
                html = html.Substring(startIndex);
            }

            string enCharStart_1 = "<a href=\"/wow/en/character/";
            string enCharStart_2 = "class=\"char pinned\"";
            string enCharStart_3 = "<a href=\"javascript:;\"";

            while (TextHelper.IsContains(html, enCharStart_1, enCharStart_2, enCharStart_3))
            {
                //The first char isn't have index
                string item = HtmlParser.GetOuterTextFromHtml(enCharStart_3, "</a>", 1, html);
                if (string.IsNullOrEmpty(item))
                {
                    item = HtmlParser.GetOuterTextFromHtml(enCharStart_1, "</a>", 1, html);
                }

                if (string.IsNullOrEmpty(item))
                {
                    item = HtmlParser.GetOuterTextFromHtml(enCharStart_2, "</a>", 1, html);
                }

                if (!string.IsNullOrEmpty(item))
                {
                    html = html.Replace(item, "");
                    if (!TextHelper.IsContains(item, "CharSelect.pin(", "<span class=\"name\">", "class=\"icon-frame frame-14 \""))
                    {
                        continue;
                    }
                    int index = GetIndex(item);
                    string name = TextHelper.TrimHtml(GetName(item));
                    string lvlStr = TextHelper.TrimHtml(GetLvlStr(item));
                    int lvl = GetLvl(lvlStr);
                    string realm = TextHelper.TrimHtml(GetRealm(item));
                    string href = GetItemHref(item);

                    //May some char not as up format
                    if (string.IsNullOrEmpty(lvlStr) || string.IsNullOrEmpty(name))
                    {
                        name = HtmlParser.GetOutterPropertyFromHtml(item, "data-tooltip");
                        item = item.Replace(HtmlParser.GetOuterTextFromHtml("<img src=", "/>", 1, item), "");
                        item = item.Replace(HtmlParser.GetOuterTextFromHtml("<img src=", "/>", 1, item), "");
                        lvlStr = HtmlParser.GetInnerTextFromHtml(item);
                        lvl = GetLvl(lvlStr);

                        string[] values = name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string s in values)
                        {
                            if (s.StartsWith("("))
                            {
                                realm = s; break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(lvlStr) && !string.IsNullOrEmpty(name))
                    {
                        BattleCharItem ci = new BattleCharItem()
                        {
                            Index = index,
                            Name = name,
                            LevelStr = lvlStr,
                            Level = lvl,
                            Realm = realm,
                            Href = string.Format("{0}{1}", this.GameServer.DomainUrl, href)
                        };
                        if (lvl >= availableLeve)
                        {
                            characterList.Add(ci);
                        }
                    }
                }
            }

            if (this.DetectionParamsItem.IsGetGValue)
            {
                this.GetGValue(characterList);
            }
            return characterList;
        }

        private int GetIndex(string html)
        {
            string startStr = "CharSelect.pin(";
            if (!TextHelper.IsContains(html, startStr))
            {
                return 0;
            }

            html = HtmlParser.GetOutterPropertyFromHtml(html, "onclick");
            int startIndex = html.IndexOf(startStr) + startStr.Length;
            int endIndex = html.IndexOf(",");
            if (startIndex < html.Length)
            {
                string index = html.Substring(startIndex, endIndex - startIndex);
                if (TextHelper.IsNumber(index))
                {
                    return TextHelper.StringToInt(index);
                }
            }
            return 0;
        }

        private string GetItemHref(string html)
        {
            string outterText = HtmlParser.GetOuterTextFromHtml("<a href=\"", "rel=\"np\">", 1, html);
            string href = HtmlParser.GetOutterPropertyFromHtml(outterText, "href");
            return href;
        }

        private string GetName(string html)
        {
            string outterText = HtmlParser.GetOuterTextFromHtml("<span class=\"name", "</span>", 1, html);
            string name = HtmlParser.GetInnerTextFromHtml(outterText);
            return name;
        }

        private string GetLvlStr(string html)
        {
            string outterText = HtmlParser.GetOuterTextFromHtml("<span class=\"class", "</span>", 1, html);
            string lvlStr = HtmlParser.GetInnerTextFromHtml(outterText);
            return lvlStr;
        }

        private int GetLvl(string lvlStr)
        {
            if (string.IsNullOrEmpty(lvlStr)) return 0;

            string[] items = lvlStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in items)
            {
                if (TextHelper.IsNumber(s))
                {
                    return TextHelper.StringToInt(s);
                }
            }

            return 0;
        }

        private string GetRealm(string html)
        {
            string outterText = HtmlParser.GetOuterTextFromHtml("<span class=\"realm", "</span>", 1, html);
            string realm = HtmlParser.GetInnerTextFromHtml(outterText);
            return realm;
        }

        #endregion

        #region Get battle G

        public void GetGValue(IList<BattleCharItem> charList)
        {
            if ((null == charList) || (charList.Count == 0)) return;
            foreach (BattleCharItem item in charList)
            {
                this.GetGValue(item);
            }
        }

        protected bool isQueryed = false;
        protected virtual void GetGValue(BattleCharItem item)
        {
            if (null == item) return;
            //index=0&xtoken=74c15f4a-347a-4c8e-b7aa-26704ac45ad9
            const string PostGFormat = "index={0}{1}";
            //lastMailId=0&xtoken=24d9b826-251b-4e42-ad80-dc775ad6ccee
            const string firstPostGFormate = "lastMailId=0{0}";

            string gUrl = this.DetectionParamsItem.CurrentGameServer.BattleGSelectCharUrl;
            string postData = string.Empty;
            if (item.Index <= 0)
            {
                postData = string.Format(firstPostGFormate, this.GetGxstoken());
            }
            else
            {
                postData = string.Format(PostGFormat, item.Index, this.GetGxstoken());
            }
            string html = string.Empty;
            if (item.Index > 0)
                html = this.ReadUrlContent(gUrl, postData);
            //html = this.ReadFromUrl(item.Href);
            if (!isQueryed)
            {
                this.httpHeaderType = HttpHeaderType.XmlHttpRequest;
                html = this.ReadFromUrl(this.DetectionParamsItem.CurrentGameServer.BattleGetGUrl);
                this.httpHeaderType = HttpHeaderType.Normal;
                isQueryed = true;
            }
            int i = 0;
            do
            {
                this.httpHeaderType = HttpHeaderType.Json;
                html = this.ReadFromUrl(this.DetectionParamsItem.CurrentGameServer.BattleGetGUrlMoney, true);
                this.httpHeaderType = HttpHeaderType.Normal;

                string moneyDiv = "\"money\"";
                if (TextHelper.IsContains(html, moneyDiv))
                {
                    JsonData jd = JsonMapper.ToObject(html);
                    int money = (int)jd["money"];
                    item.Money = money.ToString();
                    item.Gold = money / 10000;
                    item.Sliver = (money - item.Gold * 10000) / 100;
                    item.Copper = money - item.Gold * 10000 - item.Sliver * 100;
                    return;
                }
                this.LogManager.Error(string.Format("Char:{0} get G 值失败，开始重试:{1}", item.Name, postData));
            } while (i++ < 1);

            item.Money = "取不到G值或者出错";
        }

        protected string xtoken = string.Empty;
        protected string GetGxstoken()
        {
            // return "&xtoken=4e5473e3-0963-4baf-aa02-088f2fca1657";
            if (!string.IsNullOrEmpty(this.xtoken)) return this.xtoken;

            string tokenName = "xstoken";
            string tokenEnd = ";";
            if (!string.IsNullOrEmpty(this.cookie) && TextHelper.IsContains(this.cookie, tokenName))
            {
                string cookieStr = this.cookie;
                int startIndex = cookieStr.IndexOf(tokenName, 0, StringComparison.CurrentCultureIgnoreCase);
                startIndex = startIndex < 0 ? 0 : startIndex;
                int endIndex = cookieStr.IndexOf(tokenEnd, startIndex, StringComparison.CurrentCultureIgnoreCase);

                if (endIndex <= 0)
                {
                    this.xtoken = "&" + cookieStr.Substring(startIndex);
                }
                else
                {
                    //Except the last ;
                    this.xtoken = "&" + cookieStr.Substring(startIndex, endIndex - startIndex + tokenEnd.Length - 1);
                }

                return this.xtoken;
            }
            return "&xstoken=null";
        }

        #endregion

    }
}
