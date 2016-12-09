using PublicUtilities;
namespace PwdReset
{

    public abstract class PwdResetHttperBase : GameHttperBase
    {
        //email=oldsklhollywood@yahoo.com&firstName=Troy&secondName=Lindberg
        private const string POSTUSERFORMAT = "email={0}&firstName={1}&secondName={2}";
        //sqa-option=on&secretQuestionAnswer=TESTANSWER
        private const string POSTSECURITYANSWERFORMAT = "sqa-option=on&secretQuestionAnswer={0}";

        //email=roddabomb@verizon.net&firstName=1112123&secondName=12312&securityText=TESTCAPACHA;
        private const string POSTCAPTCHAFORMAT = "email={0}t&firstName={1}&secondName={2}&securityText={3}";

        /// <summary>
        /// 获取角色POST格式
        /// </summary>
        protected string PostUserFormat
        { get; set; }

        /// 提交安全回答POST格式
        /// </summary>
        protected string PostSecurityAnswerFormat
        { get; set; }

        public PwdResetHttperBase(DetectionParamsItem paramsItem)
            : base(paramsItem,WowLogManager.Instance)
        {
            this.PostUserFormat = PwdResetHttperBase.POSTUSERFORMAT;
            this.PostSecurityAnswerFormat = PwdResetHttperBase.POSTSECURITYANSWERFORMAT;
            this.PostContentWithCaptchaFormat = PwdResetHttperBase.POSTCAPTCHAFORMAT;
        }

        public abstract PwdResetItem GetItemState(PwdResetItem item);
    }
}