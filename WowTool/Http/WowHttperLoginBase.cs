using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace WebDetection
{
    public abstract class WowHttperLoginBase : GameHttperBase
    {
        protected UserAccountItem curretnAccountItem = null;
        public WowHttperLoginBase(DetectionParamsItem paramsItem, LogManagerBase logManager)
            : base(paramsItem, logManager)
        {
        }
        public abstract UserAccountItem GetAccountDetail(UserAccountItem accountItem);

        public abstract UserAccountItem TakeMoreTryForErrored(UserAccountItem accountItem);

        public virtual void GetCharacters(UserAccountItem userItem)
        {
        }
    }

}
