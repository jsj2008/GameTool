using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace PublicUtilities
{
    public class WowToolConfig
    {
        private readonly static string XmlFileName = "Wow工具集.Xml";
        public readonly static WowToolConfig Instance = new WowToolConfig();

        private XmlIniFile XmlFile = new XmlIniFile(WowToolConfig.XmlFileName);

        public void Save()
        {
            Trace.WriteLine("Start write params to params file");
            try
            {
                this.XmlFile.SaveFile();
            }
            catch (System.Exception ex)
            {
                Trace.WriteLine(string.Format("Save params file error:{0}", ex.Message));
            }
        }

        /// <summary>
        /// Is completed
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsCompleted", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsCompleted", value);
            }
        }

        #region  FrmInsert Mode1

        /// <summary>
        /// Selected mode
        /// </summary>
        public int SelectedMode
        {
            get
            {
                return this.XmlFile.ReadInt("General", "SelectedMode", 1);
            }
            set
            {
                this.XmlFile.WriteInt("General", "SelectedMode", value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Mode1AddChars
        {
            get
            {
                return this.XmlFile.ReadString("Mode1", "AddChars", "");
            }
            set
            {
                this.XmlFile.WriteString("Mode1", "AddChars", value.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Mode1IsAddBefore
        {
            get
            {
                return this.XmlFile.ReadBool("Mode1", "IsAddBefore", false);
            }
            set
            {
                this.XmlFile.WriteBool("Mode1", "IsAddBefore", value);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Mode1AddBeforeChars
        {
            get
            {
                return this.XmlFile.ReadString("Mode1", "AddBeforeChars", "");
            }
            set
            {
                this.XmlFile.WriteString("Mode1", "AddBeforeChars", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Mode1IsAddAfter
        {
            get
            {
                return this.XmlFile.ReadBool("Mode1", "IsAddAfter", false);
            }
            set
            {
                this.XmlFile.WriteBool("Mode1", "IsAddAfter", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Mode1AddAfterChars
        {
            get
            {
                return this.XmlFile.ReadString("Mode1", "AddAfterChars", "");
            }
            set
            {
                this.XmlFile.WriteString("Mode1", "AddAfterChars", value);
            }
        }

        #endregion

        #region  FrmInsert Mode2

        /// <summary>
        /// 
        /// </summary>
        public string Mode2AddChars
        {
            get
            {
                return this.XmlFile.ReadString("Mode2", "AddChars", "");
            }
            set
            {
                this.XmlFile.WriteString("Mode2", "AddChars", value);
            }
        }

        #endregion

        #region  FrmInsert Mode3

        /// <summary>
        /// 
        /// </summary>
        public bool Mode3IsAdd
        {
            get
            {
                return this.XmlFile.ReadBool("Mode3", "IsAdd", false);
            }
            set
            {
                this.XmlFile.WriteBool("Mode3", "IsAdd", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Mode3IsDelete
        {
            get
            {
                return this.XmlFile.ReadBool("Mode3", "IsDelete", false);
            }
            set
            {
                this.XmlFile.WriteBool("Mode3", "IsDelete", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Mode3AddChars
        {
            get
            {
                return this.XmlFile.ReadString("Mode3", "AddChars", "");
            }
            set
            {
                this.XmlFile.WriteString("Mode3", "AddChars", value);
            }
        }


        /// <summary>
        ///
        /// </summary>
        public string Mode3Index
        {
            get
            {
                return this.XmlFile.ReadString("Mode3", "Index", "0");
            }
            set
            {
                this.XmlFile.WriteString("Mode3", "Index", value);
            }
        }

        #endregion

        #region  通用

        /// <summary>
        /// 数据格式
        /// </summary>
        public DataFormat DataFormat
        {
            get
            {
                string value = this.XmlFile.ReadString("General", "DataFormat", DataFormat.AccountMailPassword.ToString());
                return (DataFormat)Enum.Parse(typeof(DataFormat), value);
            }
            set
            {
                this.XmlFile.WriteString("General", "DataFormat", value.ToString());
            }
        }

        /// <summary>
        /// 目的分格字符
        /// </summary>
        public string TargetSplitChars
        {
            get
            {
                return this.XmlFile.ReadString("General", "TargetSplitChars", ":");
            }
            set
            {
                this.XmlFile.WriteString("General", "TargetSplitChars", value);
            }
        }


        /// <summary>
        /// 原文件分格字符
        /// </summary>
        public string RawSplitChars
        {
            get
            {
                return this.XmlFile.ReadString("General", "RawSplitChars", " ");
            }
            set
            {
                this.XmlFile.WriteString("General", "RawSplitChars", value);
            }
        }

        public bool IsCanAdd
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsCanAdd", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsCanAdd", value);
            }
        }

        /// <summary>
        /// 是否加在前面
        /// </summary>
        public bool IsAddBefore
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsAddBefore", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsAddBefore", value);
            }
        }

        /// <summary>
        /// 是否加在前面
        /// </summary>
        public bool IsAddAfter
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsAddAfter", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsAddAfter", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string AddIndex
        {
            get
            {
                return this.XmlFile.ReadString("General", "AddIndex", "1");
            }
            set
            {
                this.XmlFile.WriteString("General", "AddIndex", value);
            }
        }

        public bool IsCanDel
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsCanDel", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsCanDel", value);
            }
        }

        /// <summary>
        /// 是否删在前面
        /// </summary>
        public bool IsDelBefore
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsDelBefore", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsDelBefore", value);
            }
        }

        /// <summary>
        /// 是否删在前面
        /// </summary>
        public bool IsDelAfter
        {
            get
            {
                return this.XmlFile.ReadBool("General", "IsDelAfter", false);
            }
            set
            {
                this.XmlFile.WriteBool("General", "IsDelAfter", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string DelIndex
        {
            get
            {
                return this.XmlFile.ReadString("General", "DelIndex", "1");
            }
            set
            {
                this.XmlFile.WriteString("General", "DelIndex", value);
            }
        }

        #endregion

        #region  纯数字


        public string NumAdd3Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add3Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add3Chars", value);
            }
        }

        public string NumAdd4Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add4Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add4Chars", value);
            }
        }

        public string NumAdd5Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add5Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add5Chars", value);
            }
        }

        public string NumAdd6Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add6Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add6Chars", value);
            }
        }

        public string NumAdd7Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add7Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add7Chars", value);
            }
        }

        public string NumAdd8Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add8Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add8Chars", value);
            }
        }

        public string NumAdd9Chars
        {
            get
            {
                return this.XmlFile.ReadString("Num", "Add9Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Num", "Add9Chars", value);
            }
        }


        #endregion

        #region  纯字母

        public string CharAdd3Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add3Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add3Chars", value);
            }
        }

        public string CharAdd4Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add4Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add4Chars", value);
            }
        }

        public string CharAdd5Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add5Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add5Chars", value);
            }
        }

        public string CharAdd6Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add6Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add6Chars", value);
            }
        }

        public string CharAdd7Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add7Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add7Chars", value);
            }
        }

        public string CharAdd8Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add8Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add8Chars", value);
            }
        }

        public string CharAdd9Chars
        {
            get
            {
                return this.XmlFile.ReadString("Char", "Add9Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Char", "Add9Chars", value);
            }
        }

        #endregion

        #region  数字字母混合

        public string CompositeAdd3Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add3Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add3Chars", value);
            }
        }

        public string CompositeAdd4Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add4Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add4Chars", value);
            }
        }

        public string CompositeAdd5Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add5Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add5Chars", value);
            }
        }

        public string CompositeAdd6Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add6Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add6Chars", value);
            }
        }

        public string CompositeAdd7Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add7Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add7Chars", value);
            }
        }

        public string CompositeAdd8Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add8Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add8Chars", value);
            }
        }

        public string CompositeAdd9Chars
        {
            get
            {
                return this.XmlFile.ReadString("Composite", "Add9Chars", "");
            }
            set
            {
                this.XmlFile.WriteString("Composite", "Add9Chars", value);
            }
        }

        #endregion

        #region  特殊字符

        public string SpecialChars
        {
            get
            {
                return this.XmlFile.ReadString("Special", "SpecialChars", "");
            }
            set
            {
                this.XmlFile.WriteString("Special", "SpecialChars", value);
            }
        }
        #endregion

        #region GPU
        /// <summary>
        /// 
        /// </summary>
        /// 
        public bool GPU_IsSalt
        {
            get
            {
                return this.XmlFile.ReadBool("GPU", "IsSalt", false);
            }
            set
            {
                this.XmlFile.WriteBool("GPU", "IsSalt", value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        public string GPU_BatchCount
        {
            get
            {
                return this.XmlFile.ReadString("GPU", "BatchCount", "512");
            }
            set
            {
                this.XmlFile.WriteString("GPU", "BatchCount", value);
            }
        }


        /// <summary>
        /// Unit:S
        /// </summary>
        /// 
        public string GPU_Timeout
        {
            get
            {
                return this.XmlFile.ReadString("GPU", "Timeout", "600");
            }
            set
            {
                this.XmlFile.WriteString("GPU", "Timeout", value);
            }
        }

        public GPUAppType Gpu_AppType
        {
            get
            {
                string value = this.XmlFile.ReadString("GPU", "AppType", GPUAppType.Ighashgpu.ToString());
                return (GPUAppType)Enum.Parse(typeof(GPUAppType), value);
            }
            set
            {
                this.XmlFile.WriteString("GPU", "AppType", value.ToString());
            }
        }

        #endregion
    }
}
