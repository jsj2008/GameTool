using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RSTool
{
    /// <summary>
    /// 江湖帐号
    /// </summary>
    public class RSAccountItem : RiftAccountItem
    {
        public override bool IsNeedRedial
        {
            get
            {
                return LoginState.LoginTooMuch == this.State ||
                        LoginState.Catpcha == this.State ||
                        LoginState.NetworkFailure == this.State;
            }
        }
        public override string AccountDetail
        {
            get
            {
                return string.Format("序号:{0}, EMAIL:{1}，密码:{2}，状态:{3}", Index, EMail, Password, StateComment);
            }
        }

        public override string ToString()
        {
            return this.AccountDetail;
        }
    }
}
