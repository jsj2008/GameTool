using PublicUtilities;

namespace XBOX
{
    /// <summary>
    /// XBOX.Xml
    /// </summary>
    public class SetConfig : WowSetConfig
    {
        private readonly static string FileName = "XBOX.Xml";
        public readonly new static SetConfig Instance = new SetConfig();

        public SetConfig()
        {
            this.XmlFile = new XmlIniFile(FileName);
        }
    }
}
