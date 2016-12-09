using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using PublicUtilities;

namespace TestProject
{
    public partial class Frmcaptcha : Form
    {
        private IList<string> fileList = new List<string>();
        private string currentFile = string.Empty;

        public Frmcaptcha()
        {
            InitializeComponent();
            this.lbFilePath.Text = string.Empty;
            this.lbcaptcha.Text = string.Empty;
        }

        private void btnopen_Click(object sender, EventArgs e)
        {
            GetCaptchaImageFromWeb();
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Multiselect = true;
            openDlg.ShowDialog();
            if (openDlg.FileNames.Length > 0)
            {
                fileList.Clear();
                foreach (string item in openDlg.FileNames)
                {
                    this.fileList.Add(item);
                }
                this.currentFile = this.fileList[0];
                GetCaptchaFromFile(currentFile);
            }
        }

        private void GetCaptchaFromFile(string file)
        {
            this.imagecaptcha.ImageLocation = file;
            this.lbFilePath.Text = file;
            this.lbcaptcha.Text = CaptchaHelper.GetCaptchaFromFile(file);
        }

        private void GetCaptchaImageFromWeb()
        {
            HttpWebRequest httpRequest = WebRequest.Create("https://us.battle.net/login/captcha.jpg?random=-1235") as HttpWebRequest;
            if (null != httpRequest)
            {
                WebProxy proxy = new WebProxy("127.0.0.1", 8580);
                httpRequest.Proxy = proxy;
                using (Stream stream = httpRequest.GetResponse().GetResponseStream())
                {
                    using (Bitmap bitmap = new Bitmap(stream))
                    {
                        bitmap.Save(@"d:\a.jpg");
                    }
                }
            }
        }

        //private string LoadVerifyCode(string filePath)
        //{
        //    byte[] chars = new byte[8];
        //    NativeMethods.OpenImage_battlenet(filePath, ref chars[0], ref chars[1], ref chars[2], ref chars[3],
        //        ref chars[4], ref chars[5], ref chars[6], ref chars[7]);
        //    char[] codes = ASCIIEncoding.Default.GetChars(chars);
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(codes);
        //    return sb.ToString();
        //}

        //private char[] LoadVerifyCode(string filePath)
        //{
        //    try
        //    {
        //        byte[] codes = new byte[8];
        //        int resultCode = NativeMethods.OpenImage_battlenet(filePath);
        //        codes[0] = NativeMethods.Get_battlenet(1);
        //        codes[1] = NativeMethods.Get_battlenet(2);
        //        codes[2] = NativeMethods.Get_battlenet(3);
        //        codes[3] = NativeMethods.Get_battlenet(4);
        //        codes[4] = NativeMethods.Get_battlenet(5);
        //        codes[5] = NativeMethods.Get_battlenet(6);
        //        codes[6] = NativeMethods.Get_battlenet(7);
        //        codes[7] = NativeMethods.Get_battlenet(8);

        //        char[] chars = ASCIIEncoding.Default.GetChars(codes);
        //        return chars;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("LoadVerifyCode() method error:" + ex.Message);
        //    }
        //    return new char[8];
        //}

        private void btnnext_Click(object sender, EventArgs e)
        {
            int index = this.fileList.IndexOf(this.currentFile);
            string filePath = string.Empty;
            if (index < 0 || index >= (this.fileList.Count - 1))
            {
                this.currentFile = this.fileList[0];
            }
            else
            {
                this.currentFile = this.fileList[++index];
            }

            this.GetCaptchaFromFile(this.currentFile);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //OpenFileDialog openDlg = new OpenFileDialog();
            //openDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            //openDlg.Multiselect = false;
            //openDlg.ShowDialog();
            //if (File.Exists(openDlg.FileName))
            //{
            //    string sourceFile = openDlg.FileName;
            //    string destFile = @"e:\test.gzip";
            //    string decompressFile = @"e:\test.txt";
            //    int len = GZipHelper.Compress(sourceFile, destFile);
            //    GZipHelper.Decompress(destFile, decompressFile);
            //}

            //CharacterGetter cg = new CharacterGetter();
            //cg.GetRealmsAndCharacter();

            //BattleCharGetter.Test();
            RunCmd();
        }

        private void RunCmd()
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";

            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;

            p.Start();
            string cmd = " dir d:\\";
            p.StandardInput.WriteLine(cmd);

            p.StandardInput.WriteLine("exit");
            string content = p.StandardOutput.ReadToEnd();
            //string errmsg = p.StandardError.ReadToEnd()

            p.WaitForExit();
            p.Close();
        }
    }
}