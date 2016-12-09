using PublicUtilities;

namespace Apple
{
    /// <summary>
    /// 苹果.Xml
    /// </summary>
    public class SetConfig : WowSetConfig
    {
        private readonly static string FileName = "苹果.Xml";
        public readonly new static SetConfig Instance = new SetConfig();

        public SetConfig()
        {
            this.XmlFile = new XmlIniFile(FileName);
        }
    }
}
