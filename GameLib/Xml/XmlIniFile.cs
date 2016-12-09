using System;
using System.Collections;
using System.Xml.Serialization;

namespace PublicUtilities
{
    public class ParamItem
    {
        private string _name = "";
        private string _value = "";

        [XmlAttribute(AttributeName = "Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlAttribute(AttributeName = "Value")]
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public ParamItem()
        {
        }
        public ParamItem(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }

    public class XmlIniSection
    {
        private IList _props = new ArrayList();
        private string _name="";

        [XmlAttribute]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlArrayItem(Type = typeof(ParamItem), ElementName = "Param")]
        [XmlArray]
        public IList Props
        {
            get { return _props; }
            set { _props = value; }
        }

        public XmlIniSection()
        {

        }

        public ParamItem Find(string paramname)
        { 
            foreach (ParamItem param in Props)
            {
                if (param.Name == paramname) {
                    return param;
                }
            }
            return null;
        }

        public void Clear()
        {
            Props.Clear();
        }

        public string ReadString(string paramname, string defvalue)
        { 
            ParamItem param = Find(paramname);
            if (param == null)
            {
                Props.Add(new ParamItem(paramname, defvalue));
                return defvalue;
            }
            else
            {
                return param.Value;
            }
        }

        public void WriteString(string paramname, string value)
        {
            ParamItem param = Find(paramname);
            if (param == null)
            {
                Props.Add(new ParamItem(paramname, value));
            }
            else
            {
                param.Value = value;
            }
        }

        public int ReadInt(string paramname, int defvalue)
        {
            try
            {
                return Int32.Parse(ReadString(paramname, defvalue.ToString()));
            }
            catch
            {
                return defvalue; 
            }
        }

        public void WriteInt(string paramname, string value)
        {
            WriteString(paramname, value.ToString());
        }
        public bool ReadBool(string paramname, bool defvalue)
        {
            try
            {
                return bool.Parse(ReadString(paramname, defvalue.ToString()));
            }
            catch
            {
                return defvalue;
            }
        }

        public void WriteBool(string paramname, bool value)
        {
            WriteString(paramname, value.ToString());
        }
        public double ReadDouble(string paramname, double defvalue)
        {
            try
            {
                return double.Parse(ReadString(paramname, defvalue.ToString()));
            }
            catch
            {
                return defvalue;
            }
        }

        public void WriteDouble(string paramname, double value)
        {
            WriteString(paramname, value.ToString());
        }

    }

    public class XmlIniFile
    {
        private IList _sections = new ArrayList();
        private bool _isWantSave = false;
        private string _xmlfilename = "";

        [XmlArrayItem(Type = typeof(XmlIniSection), ElementName = "Section")]
        [XmlArray]
        public IList Sections
        {
            get { return _sections; }
            set { _sections = value; }
        }

        [XmlIgnore]
        public bool IsWantSave
        {
            get {
                return _isWantSave;
            }
            set {
                _isWantSave = value;
            }
        }

        [XmlIgnore]
        public string XmlFilename
        {
            get
            {
                return _xmlfilename;
            }
            set
            {
                _xmlfilename = value;
            }
        }

        public XmlIniFile()
        { 
        
        }
        public XmlIniFile(string xmlfile)
        {
            XmlFilename = xmlfile;
            LoadFile(xmlfile);
        }
        public bool LoadFile(string xmlfile)
        {
            XmlIniFile obj = (XmlIniFile)XmlBeanTools.GetBeanFromXmlFile(typeof(XmlIniFile), xmlfile);
            if (obj != null)
            {
                this.Sections = obj.Sections;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SaveFile(string xmlfile) 
        {
            XmlBeanTools.SaveBeanToXmlFile(this, xmlfile);
        }
        public void SaveFile()
        {
            if (XmlFilename != "")
            {
                SaveFile(XmlFilename);
            }
        }

        public XmlIniSection FindSection(string sectionname)
        {
            foreach (XmlIniSection section in this.Sections)
            {
                if (section.Name == sectionname)
                {
                    return section;
                }
            }
            return null;
        }
        public XmlIniSection GetSection(string sectionname)
        {
            XmlIniSection section = FindSection(sectionname);
            if (section == null)
            {
                section = new XmlIniSection();
                section.Name = sectionname;
                this.Sections.Add(section);
                _isWantSave = true;
            }
            return section;
        }

        public string ReadString(string section,string paramname, string defvalue)
        {
            ParamItem param = GetSection(section).Find(paramname);
            if (param == null)
            {
                GetSection(section).Props.Add(new ParamItem(paramname, defvalue));
                _isWantSave = true;
                return defvalue;
            }
            else
            {
                return param.Value;
            }
        }

        public void WriteString(string section, string paramname, string value)
        {
            ParamItem param = GetSection(section).Find(paramname);
            if (param == null)
            {
                GetSection(section).Props.Add(new ParamItem(paramname, value));
            }
            else
            {
                param.Value = value;
            }
            _isWantSave = true;
        }

        public int ReadInt(string section, string paramname, int defvalue)
        {
            try
            {
                return Int32.Parse(ReadString(section, paramname, defvalue.ToString()));
            }
            catch
            {
                return defvalue;
            }
        }

        public void WriteInt(string section, string paramname, int value)
        {
            WriteString(section, paramname, value.ToString());
        }
        public bool ReadBool(string section, string paramname, bool defvalue)
        {
            try
            {
                return bool.Parse(ReadString(section, paramname, defvalue.ToString()));
            }
            catch
            {
                return defvalue;
            }
        }

        public void WriteBool(string section, string paramname, bool value)
        {
            WriteString(section, paramname, value.ToString());
        }
        public double ReadDouble(string section, string paramname, double defvalue)
        {
            try
            {
                return double.Parse(ReadString(section, paramname, defvalue.ToString()));
            }
            catch
            {
                return defvalue;
            }
        }

        public void WriteDouble(string section, string paramname, double value)
        {
            WriteString(section, paramname, value.ToString());
        }

        public void SmartSave()
        {
            if (IsWantSave)
                SaveFile();
        }
    }
}
