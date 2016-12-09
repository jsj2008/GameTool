using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Text;
using PublicUtilities;

namespace WowTools
{
    public class TranslateModeOne : TranslateBase
    {
        public bool IsAddBefore
        { get; set; }

        public bool IsAddAfter
        { get; set; }

        public string AddBeforeChars
        { get; set; }

        public string AddAfterChars
        { get; set; }

        public string AddChars
        { get; set; }

        public TranslateModeOne(ModeOneParams oneParams)
        {
            this.IsAddBefore = oneParams.IsAddBefore;
            this.AddBeforeChars = oneParams.AddBeforeChars;
            this.IsAddAfter = oneParams.IsAddAfter;
            this.AddAfterChars = oneParams.AddAfterChars;
            this.AddChars = oneParams.AddChars;
        }

        public override string Start(string line)
        {
            string result = string.Empty;
            if (TextHelper.IsNumber(line))
            {
                if (IsAddBefore)
                {
                    line = AddBeforeChars + line;
                }
                if (IsAddAfter)
                {
                    line = line + AddAfterChars;
                }
                return line;
            }
            else
            {
                if (line.Length > 0)
                {
                    char c = line[line.Length - 1];
                    if (TextHelper.IsNumber(c))
                    {
                        int num = Convert.ToInt32(c.ToString()) + 1;
                        if (num == 10)
                        {
                            num = 0;
                        }
                        result = line.Substring(0, line.Length - 1) + num.ToString();
                    }
                    else if (TextHelper.IsChar(c))
                    {
                        result = string.Format("{0}{1}", line, AddChars);
                    }
                    else
                    {
                        result = "非数字或字母-->>>" + line;
                    }
                }
            }

            return result;
        }
    }

    public class TranslateModeTwo : TranslateBase
    {
        public string AddChars
        { get; private set; }

        public TranslateModeTwo(ModeTwoParams twoParams)
        {
            this.AddChars = twoParams.AddChars;
        }

        public override string Start(string line)
        {
            return string.Format("{0}{1}", line, this.AddChars);
        }
    }

    public class TranslateModeThree : TranslateBase
    {
        public string AddChars
        { get; set; }

        public int Index
        { get; set; }

        public bool IsInsert
        { get; set; }

        public bool IsDel
        {
            get;
            set;
        }

        public TranslateModeThree(ModeThreeParams threeParams)
        {
            this.IsInsert = threeParams.IsInsert;
            this.IsDel = threeParams.IsDel;
            this.Index = threeParams.Index;
            this.AddChars = threeParams.AddChars;
        }

        private string Insert(string rawLine, int index, string str)
        {
            if (index <= 0)
            {
                return string.Format("{0}{1}", str, rawLine);
            }
            else if (index > (rawLine.Length - 1))
            {
                return string.Format("{0}{1}", rawLine, str);
            }
            else
            {
                return rawLine.Insert(index, str);
            }
        }

        private string Delete(string rawLine, int index)
        { 
            if (index <= 0 && rawLine.Length > 1)
            {
                return rawLine.Substring(1);
            }
            else if (index >= rawLine.Length - 1 && rawLine.Length > 1)
            {
                return rawLine.Substring(0, rawLine.Length - 1);
            }
            else
            {
                return string.Format("{0}{1}", rawLine.Substring(0, index), rawLine.Substring(index + 1));
            }
        }

        public override string Start(string line)
        {
            string result = string.Empty;
            if (this.IsInsert)
            {
                result = this.Insert(line, Index, AddChars);
            }

            if (this.IsDel)
            {
                result = this.Delete(line, Index);
            }

            return result;
        }
    }
}
