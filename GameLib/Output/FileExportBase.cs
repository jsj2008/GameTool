using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PublicUtilities
{
    public abstract class FileExportBase : IDisposable
    {
        protected string rawFilePath = string.Empty;
        protected bool IsAppended { get; set; }
        public LogManagerBase LogManager { get; set; }
        protected IDictionary<string, StreamWriter> streamWriterDic = new Dictionary<string, StreamWriter>();

        public FileExportBase(string filePath, bool isAppended,LogManagerBase logManager)
        {
            this.rawFilePath = filePath;
            this.IsAppended = isAppended;
            this.LogManager = logManager;
        }

        #region Methods

        protected StreamWriter GetStream(string key)
        {
            if ((this.streamWriterDic.Keys.Count > 0) && this.streamWriterDic.Keys.Contains(key))
            {
                return this.streamWriterDic[key];
            }
            return null;
        }

        /// <summary>
        /// File full path and file name ,but no extension name
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected string GetFileName(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                string fileName = Path.GetFileName(filePath);
                string extensionName = Path.GetExtension(filePath);
                fileName = fileName.Replace(extensionName, "");
                string path = string.Format("{0}\\{1}\\", Path.GetDirectoryName(filePath), fileName); ;

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return string.Format("{0}{1}", path, fileName);
            }

            return string.Empty;
        }

        protected string GetFilePath(string filePath, string symbol)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                return string.Format("{0}_{1}.txt", GetFileName(filePath), symbol);
            }
            return string.Empty;
        }

        /// <summary>
        /// If file existed and isAppend is true ,don't create file again
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <param name="isAppend"></param>
        protected void CreateFileStream(string filePath, string key, bool isAppend)
        {
            try
            {
                string resultFile = this.GetFilePath(filePath, key);
                if (string.IsNullOrEmpty(resultFile))
                {
                    LogManager.Error(string.Format("File output file path can't be empty:{0}", key));
                    return;
                }
                if (this.streamWriterDic.ContainsKey(key))
                {
                    LogManager.Error(string.Format("File output stream already existed:{0}", key));
                    return;
                }
                if (!isAppend && File.Exists(resultFile))
                {
                    File.Delete(resultFile);
                }
                StreamWriter sw = new StreamWriter(resultFile, true);
                this.streamWriterDic.Add(key, sw);
            }
            catch (System.Exception ex)
            {
                LogManager.Error(ex.Message);
            }
        }

        /// <summary>
        /// If file existed and isAppend is true ,don't create file again
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="key"></param>
        /// <param name="isAppend"></param>
        protected void CreateFileStream(string filePath, bool isAppend, params string[] keys)
        {
            foreach (string key in keys)
            {
                CreateFileStream(filePath, key, isAppend);
            }
        }

        object outObj = new object();
        public void Output(string content, StreamWriter sw)
        {
            lock (outObj)
            {
                if (!string.IsNullOrEmpty(content) && (null != sw))
                {
                    sw.WriteLine(content);
                    sw.Flush();
                }
                else
                {
                    if (sw == null)
                    {
                        LogManager.Error("Account output file cann't be null");
                    }
                }
            }
        }

        public void Output(string content, string streamKey)
        {
            lock (this)
            {
                StreamWriter sw = this.GetStream(streamKey);
                if (!string.IsNullOrEmpty(content) && (null != sw))
                {
                    sw.Write(content);
                    sw.Flush();
                }
                else
                {
                    if (sw == null)
                    {
                        LogManager.Error(string.Format("Account output file cann't be null,key:{0}", streamKey));
                    }
                }
            }
        }

        public virtual void Save()
        {
            foreach (StreamWriter item in this.streamWriterDic.Values)
            {
                using (item)
                {
                    item.Flush();
                }
            }
            this.streamWriterDic.Clear();
        }


        #endregion


        #region IDisposable Members

        public void Dispose()
        {
            this.Save();
        }

        #endregion IDisposable Members
    }
}
