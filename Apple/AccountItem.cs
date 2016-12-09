using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace Apple
{
    /// <summary>
    /// 苹果帐号
    /// </summary>
    public class AccountItem : RiftAccountItem
    {
        public string Location { get; set; }
        public override string AccountDetail
        {
            get
            {
                string detail = string.Format("序号:{0}, EMAIL:{1}，密码:{2}，状态:{3}", Index, EMail, Password, StateComment);
                if (!string.IsNullOrEmpty(Location))
                {
                    detail = string.Format("{0} ，国家:{1}", detail, Location);
                }
                return detail;
            }
        }

        public  override bool IsLoginSucceed
        {
            get
            {
                return LoginState.LoginSucceed == this.State ||
                       LoginState.LoginSucceedAndVerified == this.State||
                       LoginState.AdditionalQuestion == this.State;
            }
        }

        public override string ToString()
        {
            return this.AccountDetail;
        }
    }
}
