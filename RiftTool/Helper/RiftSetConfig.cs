using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RiftTool
{
    /// <summary>
    /// 裂隙.Xml
    /// </summary>
    public class RiftSetConfig : WowSetConfig
    {
        private readonly static string FiftName = "裂隙.Xml";
        public readonly new static RiftSetConfig Instance = new RiftSetConfig();

        public RiftSetConfig()
        {
            this.XmlFile = new XmlIniFile(FiftName);
        }
    }
}
