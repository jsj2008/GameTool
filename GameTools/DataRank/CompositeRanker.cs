using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PublicUtilities;

namespace WowTools
{
    public class CompositeRanker : RankerBase
    {
        public CompositeRanker(DataInsertParams param)
        {
            this.DataParams = param;
            this.CreateOutputDirectory(this.DataParams.RawFilePath);
            this.CreateFileWriter("字母数字混合");
        }

        public override bool ParsePassword(AccountItem account)
        {
            account.SplitChars = this.DataParams.TargetSplitChars;
            account.Password = this.AddOrDel(account.Password);
            return this.WriteToFile(account);
        }
    }
}
