using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace WowTools
{
    public enum TranslateMode
    {
        One,
        Two,
        Three
    }

    public class TranslateFactory : FactoryBase
    {
        private TranslateParams TranslateParams
        {
            get;
            set;
        }

        private TranslateBase CurrentTranslate
        {
            get;
            set;
        }

        public TranslateFactory(TranslateParams translateParams)
        {
            this.TranslateParams = translateParams;
            this.FilePath = this.TranslateParams.FilePath;
            this.LoadCurrentTranslate();
        }

        private string NewFilePath
        {
            get
            {
                string directoryName = Path.GetDirectoryName(this.TranslateParams.FilePath);
                string fileName = Path.GetFileName(this.TranslateParams.FilePath);
                fileName = fileName.Substring(0, fileName.Length - (fileName.Length - fileName.IndexOf('.'))) + "_已替换.txt";
                string newFile = directoryName + @"\" + fileName;
                return newFile;
            }
        }

        private void LoadCurrentTranslate()
        {
            switch (this.TranslateParams.CurrentMode)
            {
                case TranslateMode.One:
                    this.CurrentTranslate = new TranslateModeOne(this.TranslateParams.ModeOneParams);
                    break;
                case TranslateMode.Two:
                    this.CurrentTranslate = new TranslateModeTwo(this.TranslateParams.ModeTwoParams);
                    break;
                case TranslateMode.Three:
                    this.CurrentTranslate = new TranslateModeThree(this.TranslateParams.ModeThreeParams);
                    break;
                default:
                    this.CurrentTranslate = new TranslateModeOne(this.TranslateParams.ModeOneParams);
                    break;
            }
        }

        protected override void ProcessLine(string line)
        {
            if (string.IsNullOrEmpty(line)) return;
            
            line = this.CurrentTranslate.Start(line);
            this.WriteLine(line);
        }

        StreamWriter resultSW = null;
        private void WriteLine(string line)
        {
            if (null == resultSW)
            {
                if (File.Exists(this.NewFilePath))
                {
                    File.Delete(this.NewFilePath);
                }
                resultSW = new StreamWriter(new FileStream(this.NewFilePath, FileMode.CreateNew));
                resultSW.AutoFlush = true;

            }
            resultSW.WriteLine(line);          
        }

        public override void Stop()
        {
            base.Stop();
            if (null != resultSW )
            {
                using (resultSW)
                {
                    resultSW.Flush();
                }
                resultSW = null;
            }
        }
    }
}
