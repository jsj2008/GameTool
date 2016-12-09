using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace GuildWar
{
    /// <summary>
    /// GuildWar帐号
    /// </summary>
    public class FightAccountItem : RiftAccountItem
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
