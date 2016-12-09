using System.Text.RegularExpressions;
using PublicUtilities;

namespace WowTools
{
    public class CharRanker : RankerBase
    {
        public CharRanker(DataInsertParams param)
        {
            this.DataParams = param;
            this.CreateOutputDirectory(this.DataParams.RawFilePath);
            this.CreateFileWriter("纯字母");
        }

        public override bool ParsePassword(AccountItem account)
        {
            account.SplitChars = this.DataParams.TargetSplitChars;
            if (IsAllChar(account.Password))
            {
                account.Password = this.AddOrDel(account.Password);
                return WriteToFile(account);
            }
            return false;
        }      

        private bool IsAllChar(string line)
        {
            bool b = Regex.IsMatch(line, @"^[A-Za-z]+$");
            return b;
        }
    }
}
