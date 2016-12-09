using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PublicUtilities
{
    public class PwdSetConfig : WowSetConfig
    {
        private readonly static string PwdSetXML = "破宝.Xml";
        public readonly new static PwdSetConfig Instance = new PwdSetConfig();

        public PwdSetConfig()
        {
            this.XmlFile = new XmlIniFile(PwdSetXML);
        }
    }
}
