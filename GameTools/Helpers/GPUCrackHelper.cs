using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PublicUtilities;

namespace WowTools
{
    public class GPUCrackHelper
    {
        #region Api

        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [DllImport("shell32.dll ")]
        static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile,
            string lpParameters, string lpDirectory, ShowCommands nShowCmd);

        #endregion

        public static bool IsRunning = false;
        private GPUParams CurretnParams
        {
            get;
            set;
        }

        private Process currentProcess = null;

        private IDictionary<int, string> tempFiles = new Dictionary<int, string>();

        public GPUCrackHelper(GPUParams gpuParams)
        {
            this.CurretnParams = gpuParams;
        }

        #region CMD

        public void Start()
        {
            if (null == this.CurretnParams) return;
            if (!GPUCrackHelper.IsRunning)
            {
                Thread t = new Thread(new ThreadStart(delegate()
                {
                    GPUCrackHelper.IsRunning = true;
                    if (this.CurretnParams.IsSalt)
                    {
                        this.CreateTempDataFiles();
                        if (this.tempFiles.Count > 0)
                        {
                            foreach (string filePath in this.tempFiles.Values)
                            {
                                if (!GPUCrackHelper.IsRunning)
                                    break;
                                StartCmdProcess(filePath);
                            }
                        }
                    }
                    else
                    {
                        StartCmdProcess(this.CurretnParams.DataPath);
                    }

                    this.DeleteTempDir();
                    GPUCrackHelper.IsRunning = false;
                }));

                t.Start();
            }
            else
            {
                MessageBox.Show("GPU破解程序正在运行，请等执行完再试！");
            }
        }

        public void Stop()
        {
            GPUCrackHelper.IsRunning = false;
            if (null != this.currentProcess)
            {
                //this.currentProcess.Close();
                this.currentProcess.Kill();
            }
        }

        private void StartCmdProcess(string filePath)
        {
            using (currentProcess = new Process())
            {
                currentProcess.StartInfo.FileName = this.CurretnParams.AppPath;
                currentProcess.StartInfo.CreateNoWindow = false;
                currentProcess.StartInfo.Arguments = filePath;
                currentProcess.Start();

                currentProcess.WaitForExit();
            }
            currentProcess = null;
        }

        #endregion

        #region Data reader

        private int count = 0;
        private void CreateTempDataFiles()
        {
            if ((null != this.CurretnParams) && File.Exists(this.CurretnParams.DataPath))
            {
                using (StreamReader sr = new StreamReader(this.CurretnParams.DataPath, Encoding.UTF8))
                {
                    string line = string.Empty;
                    StringBuilder sb = new StringBuilder();
                    count = 0;

                    this.tempFiles.Clear();
                    while (null != (line = sr.ReadLine()))
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            sb.AppendLine(line);
                            count++;
                        }

                        if ((count % this.CurretnParams.BatchCount) == 0)
                        {
                            this.CreatTempFile((count / this.CurretnParams.BatchCount), sb.ToString());
                            sb = new StringBuilder();
                        }
                    }

                    if (sb.Length > 0)
                    {
                        this.CreatTempFile((count / this.CurretnParams.BatchCount) + 1, sb.ToString());
                    }
                }
            }
        }

        private void CreatTempFile(int index, string lines)
        {
            string dir = this.CreateTempDir(this.CurretnParams.DataPath, index);
            if (!string.IsNullOrEmpty(dir))
            {
                string filePath = string.Format("{0}\\{1}.txt", dir, index);
                using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        sw.Write(lines);
                        sw.Flush();
                    }
                }

                //string batFile = string.Format("{0}\\{1}",dir,Path.GetFileName(this.CurretnParams.AppPath));
                //File.Copy(this.CurretnParams.AppPath, batFile);
                this.tempFiles.Add(index, filePath);
            }
        }

        private string tempPath = string.Empty;
        private void DeleteTempDir()
        {
            if (!string.IsNullOrEmpty(this.tempPath) && Directory.Exists(this.tempPath))
            {
                Directory.Delete(this.tempPath, true);
            }
        }

        private string CreateTempDir(string rawFilePath, int index)
        {
            if (string.IsNullOrEmpty(rawFilePath)) return string.Empty;

            string dir = Path.GetDirectoryName(rawFilePath);
            this.tempPath = string.Format("{0}\\Temp\\", dir);
            string newDir = string.Format("{0}{1}", this.tempPath, index);
            if (Directory.Exists(newDir))
            {
                Directory.Delete(newDir, true);
            }
            Directory.CreateDirectory(newDir);
            return newDir;
        }

        #endregion
    }

    public class BatParams
    {
        public string BatPath
        { get; set; }

        public string Param
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Md5(Md5($Pass).$Salt) get from raw data file
    /// </summary>
    public class Md5MD5PassSaltHepler
    {
        private const string SQLINSERTHEADER = "INSERT INTO aa VALUES('";
        private const string SQLCOLUMNSPLIT = "', '";
        private const string SQLINSERTEND = "');";
        private const string REPLACESTRING = ":";

        private string sourceFilePath = string.Empty;
        private string targetFilePath = string.Empty;
        private StreamWriter sw = null;

        public Md5MD5PassSaltHepler()
        {
        }

        public void Start(string rawFile)
        {
            if (string.IsNullOrEmpty(rawFile) || !File.Exists(rawFile))
            {
                return;
            }

            using (StreamReader sr = new StreamReader(rawFile))
            {
                this.sourceFilePath = rawFile;
                string line = string.Empty;
                while ((null != (line = sr.ReadLine())) && !sr.EndOfStream)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        line = RawParse(line);
                        if (!string.IsNullOrEmpty(line))
                        {
                            this.SaveTo(line);
                        }
                    }
                }

                if(null != sw)
                {
                    using (sw)
                    {
                        sw.Flush();
                        sw.Close();
                    }
                }               

                sr.Close();
            }
        }

        private void SaveTo(string line)
        {
            if (null == sw)
            {
                this.targetFilePath = string.Format("{0}\\{1}_New.txt", FileHelper.GetFilePath(this.sourceFilePath), FileHelper.GetFileName(this.sourceFilePath));
                if (File.Exists(this.targetFilePath))
                {
                    File.Delete(this.targetFilePath);
                }

                FileStream fs = File.Create(this.targetFilePath);
                if (null != fs)
                {
                    sw = new StreamWriter(fs, Encoding.UTF8);
                }
            }

            sw.WriteLine(line);
        }

        private string RawParse(string rawLine)
        {
            //INSERT INTO aa VALUES('SoulBeat', '91eed37c8ab99e6a8227db7e64da278e', '<g]', 'schu.rke@gmx.de');
            rawLine = rawLine.Replace(SQLINSERTHEADER, "");
            rawLine = rawLine.Replace(SQLCOLUMNSPLIT, REPLACESTRING);
            rawLine = rawLine.Replace(SQLINSERTEND, "");
            return rawLine;
        }
    }
}
