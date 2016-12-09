using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using PublicUtilities;

namespace WowTools
{
    public class NumberRanker : RankerBase
    {
        public NumberRanker(DataInsertParams param)
        {
            this.DataParams = param;
            this.CreateOutputDirectory(this.DataParams.RawFilePath);
            this.CreateFileWriter("纯数字");
        }

        public override bool ParsePassword(AccountItem account)
        {
            account.SplitChars = this.DataParams.TargetSplitChars;
            if (IsNumber(account.Password))
            {
                account.Password = this.AddOrDel(account.Password);
                return this.WriteToFile(account);                
            }

            return false;
        }

        private bool IsNumber(string line)
        {
            bool b = Regex.IsMatch(line, @"^\d+$");
            return b;
        }
    }
}
