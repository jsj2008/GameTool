using PublicUtilities;

namespace GuildWar
{
    /// <summary>
    /// GuildWar.Xml
    /// </summary>
    public class SetConfig : WowSetConfig
    {
        private readonly static string FileName = "GuildWar.Xml";
        public readonly new static SetConfig Instance = new SetConfig();

        public SetConfig()
        {
            this.XmlFile = new XmlIniFile(FileName);
        }
    }
}
