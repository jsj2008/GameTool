using PublicUtilities;

namespace RSTool
{
    /// <summary>
    /// 江湖.Xml
    /// </summary>
    public class RSSetConfig : WowSetConfig
    {
        private readonly static string RSFileName = "江湖.Xml";
        public readonly new static RSSetConfig Instance = new RSSetConfig();

        public RSSetConfig()
        {
            this.XmlFile = new XmlIniFile(RSFileName);
        }
    }
}
