using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace WebDetection
{
    class TWBattleLoginHttper: SeaBattleLoginHttper
    {
        //private const string ContentFormat = "accountName={0}+&password={1}+";
        private const string POSTCONTENTFORMAT = "accountName={0}&password={1}&persistLogin=on";
        private const string POSTCONTENTWITHCAPTCHAFORMAT = "accountName={0}&password={1}&securityAnswer={2}";

        public TWBattleLoginHttper(DetectionParamsItem paramsItem)
            : base(paramsItem)
        {
            this.PostContentFormat = TWBattleLoginHttper.POSTCONTENTFORMAT;
            this.PostContentWithCaptchaFormat = TWBattleLoginHttper.POSTCONTENTWITHCAPTCHAFORMAT;
        }    }
}
