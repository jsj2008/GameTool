using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

namespace PublicUtilities
{
    public class BoolEventArgs : EventArgs
    {
        public bool IsTrue
        {
            get;
            set;
        }
    }

    public abstract class HttpHelperBase : IDisposable
    {
        /// <summary>
        /// MS
        /// </summary>
        public static int ConnectTimeout = WowSetConfig.Instance.HttpTimeout * 1000;
        public const string MethodPost = "POST";
        public const string MethodGet = "GET";
        public const string HTTPERROR = "HttpError";
        public const string IMAGEEXTENSION = ".jpg";

        protected CookieContainer cookieContainer = new CookieContainer();
        protected WebHeaderCollection headerCollection = new WebHeaderCollection();
        protected string cookie = string.Empty;

        protected HttpWebRequest currentHttpRequest = null;
        public HtmlParseHelper HtmlParser = new HtmlParseHelper();

        public LogManagerBase LogManager
        {
            get;
            set;
        }

        static HttpHelperBase()
        {
            //WebRequest.ServicePoint.ConnectionLimit;
            int defaultLimit = ServicePointManager.DefaultConnectionLimit;
            ServicePointManager.DefaultConnectionLimit = 512;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            //ServicePointManager.Expect100Continue = true;
            //httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
        }

        public HttpHelperBase(LogManagerBase logManager)
        {
            this.LogManager = logManager;
        }

        protected string RedirectLocation
        {
            get
            {
                if ((null != this.headerCollection) && this.headerCollection.Count > 0)
                {
                    return this.headerCollection.Get("Location");
                }
                return string.Empty;
            }
        }

        //private const string testCookieFormat = @"BA-tassadar=US-5143631-JtsPnHsOZqnQ1coGp41vkRzsdg75WQOfiPy; {0} cookieLangId=en_US; __utmz=134253166.1276596977.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); login.key=36e74f1c39a5b32e07be475e96b5f074";
        private const string testCookieFormat = @"{0} __utma=134253166.1032902914.1283045384.1283050033.1283050135.3; __utmz=134253166.1283045384.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmb=134253166.1.10.1283050135; __utmc=134253166";

        protected virtual void SetHttpRequestPostHeader(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Method = HttpHelperBase.MethodPost;
                httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
                httpRequest.KeepAlive = true;
                httpRequest.AllowAutoRedirect = true;
                httpRequest.CookieContainer = this.cookieContainer;
                httpRequest.Accept = @"image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/QVOD, application/QVOD, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                httpRequest.UserAgent = @" Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; CIBA)";
                httpRequest.ContentType = @"application/x-www-form-urlencoded";
                httpRequest.ProtocolVersion = new Version("1.1");

                //httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");
                httpRequest.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                this.SetCookieToHttpRequestHeader(httpRequest);
                this.SetProxy(httpRequest);
                httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }
        }

        protected virtual void SetProxy(HttpWebRequest httpRequest)
        {
        }

        protected virtual void SetCookieToHttpRequestHeader(HttpWebRequest httpRequest)
        {
            string tempCookie = string.Empty;
            if (!string.IsNullOrEmpty(this.cookie))
            {
                if (this.cookie.Contains("; Path=/login; Secure; HttpOnly"))
                {
                    tempCookie = string.Format(testCookieFormat, this.cookie.Replace("; Path=/login; Secure; HttpOnly", ";"));
                    //this.cookie = string.Format(testCookieFormat, this.cookie.Replace("; Path=/login; Secure; HttpOnly", "")) + "; __utmc=134253166";
                }
                //LogManager.Info("SetCookieToHttpRequestHeader  cookie:" + tempCookie);
                if (!string.IsNullOrEmpty(tempCookie))
                {
                    httpRequest.Headers.Add(HttpRequestHeader.Cookie, tempCookie);
                }
            }
        }

        protected virtual void SetHttpRequestGetHeader(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Method = HttpHelperBase.MethodGet;
                httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
                httpRequest.CookieContainer = this.cookieContainer;
                httpRequest.Accept = @"image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/QVOD, application/QVOD, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                httpRequest.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; CIBA)";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-US");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                httpRequest.ProtocolVersion = new Version("1.1");
                httpRequest.KeepAlive = true;
                httpRequest.AllowAutoRedirect = true;
                httpRequest.MaximumAutomaticRedirections = 50;
                this.SetCookieToHttpRequestHeader(httpRequest);
                this.SetProxy(httpRequest);
                httpRequest.Credentials = CredentialCache.DefaultNetworkCredentials;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            }
        }

        /// <summary>
        /// Use for send multipart form-data
        /// </summary>
        /// <param name="httpRequest"></param>
        protected virtual string SetHttpRequestPostMultiPartFormDataHeader(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Method = HttpHelperBase.MethodPost;
                httpRequest.Timeout = HttpHelperBase.ConnectTimeout;
                string[] props = { "urls", "src", "product", "combo", "v", "vk", "mid" };
                string[] values ={"881ca4270f86f5f0e3e67b4b6af583e9|8aaece1c52454e9710dab6c3c51a3612|c4543fb77aad8fa201042ee8dc35e359|3ba2761279b1fa38534253deb71be10d	nUE0pUZ6Yl91pl5vLKE0oTHhozI0Y2kiM2yhY2IhY2kiM2yhYaugoQ9lMJL9nUE0pUZ6Yl93q3phq29loTEiMaqupzAlLJM0YzAioF9uL2AiqJ50YlMupUN9q2SgWzAlCKElqJHzpzu0oJj9qUW1MD==",
                              "addrbar","wd","urlproc","1","821e69f4","e2b47e3cc8674849a6b7e42e9fce5193"};

                const string BOUNDARY = "-----------------------------7d83e2d7a141e"; // 分隔符

                StringBuilder sb = new StringBuilder();
                sb = sb.Append("\r\n");
                sb = sb.Append("--");
                sb = sb.Append(BOUNDARY);
                sb = sb.Append("\r\n");
                sb = sb.Append("\r\n");
                sb = sb.Append("Content-Disposition: form-data; name=\"" + props[0] + "\"\r\n\r\n");
                sb = sb.Append(values[0]);
                sb = sb.Append("\r\n");
                sb = sb.Append("\r\n");

                // 发送每个字段:
                for (int i = 1; i < props.Length; i++)
                {
                    sb = sb.Append("--");
                    sb = sb.Append(BOUNDARY);
                    sb = sb.Append("\r\n");
                    sb = sb.Append("Content-Disposition: form-data; name=\"" + props[i] + "\"\r\n\r\n");
                    sb = sb.Append(values[i]);
                    sb = sb.Append("\r\n");
                }

                sb = sb.Append("--");
                sb = sb.Append(BOUNDARY);
                sb = sb.Append("--");
                sb = sb.Append("\r\n");
                sb = sb.Append("\r\n");

                httpRequest.ContentType = "Content-Type: multipart/form-data; boundary=" + BOUNDARY;
                httpRequest.UserAgent = "Post_Multipart";
                // httpRequest.Headers.Add(HttpRequestHeader.Host, "qurl.f.360.cn");
                httpRequest.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");

                string postData = sb.ToString();
                return postData;
            }
            return string.Empty;
        }

        protected void WriteHttpMultiPartFormData(string url)
        {
            string uri = string.Format(@"{0}login/en/check_outchain.php", url);
            HttpWebRequest httpRequest = WebRequest.Create(uri) as HttpWebRequest;

            if (null != httpRequest)
            {
                try
                {
                    string postData = this.SetHttpRequestPostMultiPartFormDataHeader(httpRequest);
                    if (!string.IsNullOrEmpty(postData))
                    {
                        httpRequest.ContentLength = postData.Length;
                        using (StreamWriter sw = new StreamWriter(httpRequest.GetRequestStream(), Encoding.ASCII))
                        {
                            sw.Write(postData);
                            sw.Flush();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (null != LogManager)
                        LogManager.Error(string.Format("WriteHttpMultiPartFormData() error:{0}", ex.Message));
                }
                finally
                {
                    this.DisposeHttpRequest(httpRequest);
                }
            }
        }

        /// <summary>
        /// New query without catptcha (isn't the same sessionid)need cookie be empty;
        /// </summary>
        protected void ClearCookie()
        {
            this.cookie = string.Empty;
            this.cookieContainer = new CookieContainer();
        }

        protected IList<string> cookieList = new List<string>();
        private void AddCookieToList(string cookie)
        {
            if (string.IsNullOrEmpty(cookie) || this.cookieList.Contains(cookie))
            {
                return;
            }
            cookieList.Add(cookie);
        }

        protected virtual void SaveCookie(HttpWebResponse httpResponse)
        {
            string repCookie = httpResponse.Headers.Get("Set-Cookie");
            if (!string.IsNullOrEmpty(repCookie))
            {
                this.cookie = repCookie;
            }

            /* save cookies */
            foreach (Cookie item in httpResponse.Cookies)
            {
                this.cookieContainer.Add(item);
            }
        }
        protected virtual void SaveCookie(HttpWebRequest httpRequest, HttpWebResponse httpResponse)
        {
            string resCookie = httpRequest.Headers.Get("Cookie");
            AddCookieToList(resCookie);
            string repCookie = httpResponse.Headers.Get("Set-Cookie");
            if (!string.IsNullOrEmpty(repCookie))
            {
                AddCookieToList(repCookie);
                this.cookie = repCookie;
            }

            //this.headerCollection = new WebHeaderCollection();
            ///* save headers */
            //for (int i = 0; i < httpResponse.Headers.Count; i++)
            //{
            //    this.headerCollection.Add(httpResponse.Headers.AllKeys[i], httpResponse.Headers.Get(i));
            //}

            //JSESSIONID=7AFA35022C74EBFF8063FA6084D1AE41.blade34_01_login
            //httpResponse.Cookies["JSESSIONID"].Value = string.Empty;

            /* save cookies */
            foreach (Cookie item in httpResponse.Cookies)
            {
                this.cookieContainer.Add(item);
            }
            // BugFix_CookieDomain(this.cookieContainer);
        }

        private void BugFix_CookieDomain(CookieContainer cookieContainer)
        {
            System.Type _ContainerType = typeof(CookieContainer);
            Hashtable table = (Hashtable)_ContainerType.InvokeMember("m_domainTable",
                                       System.Reflection.BindingFlags.NonPublic |
                                       System.Reflection.BindingFlags.GetField |
                                       System.Reflection.BindingFlags.Instance,
                                       null,
                                       cookieContainer,
                                       new object[] { });
            ArrayList keys = new ArrayList(table.Keys);
            foreach (string keyObj in keys)
            {
                string key = (keyObj as string);
                if (key[0] == '.')
                {
                    string newKey = key.Remove(0, 1);
                    table[newKey] = table[keyObj];
                }
            }
        }

        /// <summary>
        /// Write ASCII content to HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postContent"></param>
        /// <returns></returns>
        protected HttpWebRequest WriteToHttpWebRequest(string url, string postContent)
        {
            HttpWebRequest httpRequest = GetHttpWebRequest(url, true);
            try
            {
                if (null != httpRequest)
                {
                    Thread.Sleep(500);
                    if (null != LogManager)
                        LogManager.Info(string.Format("WriteToHttpWebRequest() : post {0} to {1}", postContent, url));
                    // TODO:use the same session id
                    char[] postDataBytes = postContent.ToCharArray();
                    httpRequest.ContentLength = postDataBytes.Length;
                    using (StreamWriter sw = new StreamWriter(httpRequest.GetRequestStream(), Encoding.ASCII))
                    //using (StreamWriter sw = new StreamWriter(httpRequest.GetRequestStream(), Encoding.UTF8))
                    {
                        sw.Write(postDataBytes, 0, postDataBytes.Length);
                        sw.Flush();
                    }
                }
            }
            catch (WebException we)
            {
                if (null != LogManager)
                    LogManager.Error(string.Format("HttpHelperBase.WriteToHttpWebRequest() WebException:{0}", we.Message));
                return null;
            }
            catch (Exception ex)
            {
                if (null != LogManager)
                {
                    LogManager.Error(string.Format("-> 向网站提交数据{0}时出错 ", postContent));
                    LogManager.Error(string.Format("HttpHelperBase.WriteToHttpWebRequest() Exception:{0}", ex.Message));
                }
            }
            return httpRequest;
        }

        /// <summary>
        /// Get httpwebrequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isPost"></param>
        /// <returns></returns>
        private int lastServicePoint = 0;
        protected HttpWebRequest GetHttpWebRequest(string url, bool isPost)
        {
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            try
            {
                this.currentHttpRequest = httpRequest;
                if (null != httpRequest)
                {
                    if (isPost)
                    {
                        this.SetHttpRequestPostHeader(httpRequest);
                    }
                    else
                    {
                        this.SetHttpRequestGetHeader(httpRequest);
                    }

                    if ((lastServicePoint != httpRequest.ServicePoint.CurrentConnections) &&
                        (httpRequest.ServicePoint.CurrentConnections > 5))
                    {
                        if (null != LogManager)
                            LogManager.Info(string.Format("Current ServicePoint:{0}", httpRequest.ServicePoint.CurrentConnections));
                    }
                    return httpRequest;
                }
            }
            catch (Exception ex)
            {
                if (null != LogManager)
                    LogManager.Error(string.Format("HttpHelperBase.GetHttpWebRequest() error:{0}", ex.Message));
            }
            return null;
        }

        /// <summary>
        /// Read content from posting data to url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isPost"></param>
        /// <returns></returns>
        protected string ReadUrlContent(string url, string postData)
        {
            HttpWebRequest httpRequest = this.WriteToHttpWebRequest(url, postData);
            try
            {
                if (null != httpRequest)
                {
                    return this.ReadFromHttpWebResponse(httpRequest);
                }
            }
            catch (Exception ex)
            {
                if (null != LogManager)
                    LogManager.Error(string.Format("HttpHelperBase.GetHttpWebRequest() error:{0}", ex.Message));
            }
            finally
            {
                this.DisposeHttpRequest(httpRequest);
            }
            return string.Empty;
        }

        /// <summary>
        /// Get url content by Get method
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string ReadFromUrl(string url)
        {
            return ReadFromUrl(url, false);
        }

        protected string GetResponseUri(string url, ref string html)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            string content = string.Empty;
            HttpWebRequest httpRequest = this.GetHttpWebRequest(url, false);

            if (null != httpRequest)
            {
                try
                {
                    //Thread.Sleep(500);
                    HttpWebResponse httpResponse = null;
                    try
                    {
                        httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                    }
                    catch (WebException we)
                    {
                        if (null != LogManager)
                            LogManager.Error(string.Format("ReadFromHttpWebResponse error:{0}", we.Message));
                        httpResponse = we.Response as HttpWebResponse;
                    }

                    // TODO:use the same session id
                    if (null != httpResponse)
                    {
                        using (httpResponse)
                        {
                            using (StreamReader sr = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                html = sr.ReadToEnd();
                            }
                            this.SaveCookie(httpRequest, httpResponse);
                            if (httpResponse.StatusCode == HttpStatusCode.OK &&
                                new Uri(url) != httpResponse.ResponseUri)
                            {
                                content = httpResponse.ResponseUri.AbsoluteUri;
                            }
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    if (null != LogManager)
                        LogManager.Error(string.Format("HttpHelperBase.ReadFromResponseContent() error:{0}", ex.Message));
                    content = HttpHelperBase.HTTPERROR;
                }
                finally
                {
                    this.DisposeHttpRequest(httpRequest);
                }
            }

            return content;
        }

        protected string ReadFromUrl(string url, bool isPost)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            string content = HttpHelperBase.HTTPERROR;
            HttpWebRequest httpRequest = this.GetHttpWebRequest(url, isPost);

            if (null != httpRequest)
            {
                try
                {
                    //LogManager.Info(string.Format("ReadFromResponseContent() from :{0}", url));
                    content = this.ReadFromHttpWebResponse(httpRequest);
                }
                catch (System.Exception ex)
                {
                    if (null != LogManager)
                        LogManager.Error(string.Format("HttpHelperBase.ReadFromResponseContent() error:{0}", ex.Message));
                    content = HttpHelperBase.HTTPERROR;
                }
                finally
                {
                    this.DisposeHttpRequest(httpRequest);
                }
            }

            return content;
        }

        protected void DisposeHttpRequest(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                try
                {
                    httpRequest.Abort();
                    httpRequest.ServicePoint.BindIPEndPointDelegate = null;
                    httpRequest = null;
                    this.currentHttpRequest = null;
                }
                catch (Exception ex)
                {
                    if (null != LogManager)
                        LogManager.Error(string.Format("HttpRequest.Abort failed:{0}", ex.Message));
                }
            }
        }

        /// <summary>
        /// Get UTF8 content from HttpWebResponse
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns></returns>
        protected string ReadFromHttpWebResponse(HttpWebRequest httpRequest)
        {
            string content = HttpHelperBase.HTTPERROR;
            try
            {
                if (null != httpRequest)
                {
                    //Thread.Sleep(500);
                    HttpWebResponse httpResponse = null;
                    try
                    {
                        httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                    }
                    catch (WebException we)
                    {
                        if (null != LogManager)
                            LogManager.Error(string.Format("ReadFromHttpWebResponse error:{0}", we.Message));
                        httpResponse = we.Response as HttpWebResponse;
                    }

                    // TODO:use the same session id
                    if (null != httpResponse)
                    {
                        using (httpResponse)
                        {
                            this.SaveCookie(httpRequest, httpResponse);
                            using (StreamReader sr = new StreamReader(GetGzipStream(httpResponse), Encoding.UTF8))
                            {
                                content = sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (null != LogManager)
                    LogManager.Error(string.Format("HttpHelperBase.ReadFromHttpWebResponse() error:{0}", ex.Message));
                content = HttpHelperBase.HTTPERROR;
            }

            return content;
        }

        protected bool IsContains(string rawContent, params string[] subStrings)
        {
            foreach (string item in subStrings)
            {
                if (rawContent.IndexOf(item.Trim(), StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        protected bool IsEquals(string rawContent, params string[] subStrings)
        {
            foreach (string item in subStrings)
            {
                if (rawContent.Equals(item.Trim(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        protected Stream GetGzipStream(HttpWebResponse webReponse)
        {
            if (webReponse.ContentEncoding.Equals("gzip", StringComparison.CurrentCultureIgnoreCase))
            {
                return new GZipStream(webReponse.GetResponseStream(), CompressionMode.Decompress);
            }
            else if (webReponse.ContentEncoding.Equals("deflate", StringComparison.CurrentCultureIgnoreCase))
            {
                return new DeflateStream(webReponse.GetResponseStream(), CompressionMode.Decompress);
            }
            else
            {
                return webReponse.GetResponseStream();
            }
        }

        #region IDisposable Members

        protected bool isDisposing = false;
        protected virtual void OnDispose()
        {
            lock (this)
            {
                if (null != this.currentHttpRequest)
                {
                    this.currentHttpRequest.Abort();
                    this.currentHttpRequest = null;
                }
                isDisposing = true;
            }
        }

        public void Dispose()
        {
            OnDispose();
        }

        #endregion
    }

    public abstract class GameHttperBase : HttpHelperBase
    {
        /// <summary>
        /// 获取用户信息POST格式
        /// </summary>
        protected string PostContentFormat
        { get; set; }
        /// <summary>
        /// 获取验证码POST格式
        /// </summary>
        protected string PostContentWithCaptchaFormat
        { get; set; }
        /// <summary>
        /// 获取角色POST格式
        /// </summary>
        protected string PostCharacterContentFormat
        { get; set; }

        protected DetectionParamsItem DetectionParamsItem
        { get; set; }

        public GameHttperBase(DetectionParamsItem paramsItem, LogManagerBase logManager)
            : base(logManager)
        {
            this.DetectionParamsItem = paramsItem;
        }

        public virtual void ForceToSave()
        {
        }
        
        #region Captcha

        public const string CAPTCHA = "captcha.jpg";

        private object lockObject = new object();

        protected bool PostDataAndCheckCaptcha(string url, string webSiteUrl, string postContent, ref  string postCaptchaContent, ref string content)
        {
            HttpWebRequest httpRequest = this.WriteToHttpWebRequest(url, postContent);
            if (httpRequest == null)
            {
                content = HttpHelperBase.HTTPERROR;
                return false;
            }
            bool isContainCaptcha = false;
            //string captchaCode = string.Empty;
            try
            {
                content = this.ReadFromHttpWebResponse(httpRequest);
                isContainCaptcha = this.IsContainCaptchaCode(content);
                //captchaCode = this.GetCaptchaCode(content, webSiteUrl);
                //if (!string.IsNullOrEmpty(captchaCode))
                //{
                //    postCaptchaContent = string.Format(postCaptchaContent, captchaCode);
                //    if (null != LogManager)
                //        LogManager.Info(string.Format("GetCaptcha() contains verify code post data:{0}", postCaptchaContent));
                //}
            }
            finally
            {
                this.DisposeHttpRequest(httpRequest);
            }

            return isContainCaptcha;
        }

        protected bool IsContainCaptchaCode(string htmlContent)
        {
            //only need captcha need get session id back from cookie
            return htmlContent.Contains(GameHttperBase.CAPTCHA);
        }

        protected string GetCaptchaCode(string htmlContent, string webSiteUrl)
        {
            //only need captcha need get session id back from cookie
            if (htmlContent.Contains(GameHttperBase.CAPTCHA))
            {
                string captchaUrl = this.GetCaptchaUrl(htmlContent);
                if (!string.IsNullOrEmpty(captchaUrl))
                {
                    return this.GetCaptchaCodeFormUrl(string.Format("{0}{1}", webSiteUrl, captchaUrl));
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Get captcha code from url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string GetCaptchaCodeFormUrl(string url)
        {
            string captchaImagePath = this.SaveCaptchaImage(url);
            string code = CaptchaHelper.GetCaptchaFromFile(captchaImagePath);
            if (null != LogManager)
                LogManager.Info(string.Format("HttpHelperWowBase.GetCaptchaCodeFormUrl() ,Captcha:{0},File:{1}", code, captchaImagePath));
            if (DetectionParamsItem.IsDeleteCaptcha)
            {
                try
                {
                    File.Delete(captchaImagePath);
                }
                catch (System.Exception ex)
                {
                    if (null != LogManager)
                        LogManager.Info(string.Format("Delete captcha file failed:{1}", ex.Message));
                }
            }
            return code;
        }

        /// <summary>
        /// save captcha from url and return the file path
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string SaveCaptchaImage(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                if (null != LogManager)
                    LogManager.Error("captcha url can't be null or empty");
            }
            //https://us.battle.net/login/captcha.jpg?random=-1235
            HttpWebRequest httpRequest = this.GetHttpWebRequest(url, false);
            if (null != httpRequest)
            {
                try
                {
                    Thread.Sleep(500);
                    using (HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse)
                    {
                        using (Stream stream = httpResponse.GetResponseStream())
                        {
                            Bitmap bitmap = new Bitmap(stream);
                            string imagePath = string.Format("{0}{1}{2}", GetCurrentAppCaptchaPath(), GetImageNameFromUrl(url), IMAGEEXTENSION);
                            bitmap.Save(imagePath);
                            if (null != LogManager)
                                LogManager.Info(string.Format("Cpatha url:{0} save to :{1}", url, imagePath));
                            return imagePath;
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (null != LogManager)
                        LogManager.Error("HttpHelperWowBase.SaveCaptchaImage() error:" + ex.Message);
                    return HttpHelperBase.HTTPERROR;
                }
                finally
                {
                    this.DisposeHttpRequest(httpRequest);
                }
            }
            if (null != LogManager)
                LogManager.ErrorWithCallback("获取验证码时出错，可能网站不可访问！");
            return string.Empty;
        }

        protected virtual string GetCaptchaUrl(string htmlContent)
        {
            // <p><img src="/login/captcha.jpg?random=8055856583243930139" alt=""/></p>
            // string outerHtml = HtmlParser.GetOuterTextFromHtml("<div class=\"captcha\">", @"<img", @"/>", 1, htmlContent);
            string outerHtml = HtmlParser.GetOuterTextFromHtml("<img src=\"/login/captcha.jpg", @"<img", @"/>", 1, htmlContent);
            string captchaImage = HtmlParser.GetOutterPropertyFromHtml(outerHtml, "src");
            return captchaImage;
        }

        private string GetCurrentAppCaptchaPath()
        {
            const string captchaFolderName = "TempCaptcha";
            string appPath = Path.GetDirectoryName(Application.ExecutablePath);
            appPath = string.Format(@"{0}\{1}\", appPath, captchaFolderName);

            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }
            return appPath;
        }

        private string GetImageNameFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                if (null != LogManager)
                    LogManager.Error("captcha url can't be null or empty");
            }
            string randomId = string.Empty;
            if (!string.IsNullOrEmpty(url) && (url.IndexOf("=") >= 0))
            {
                randomId = url.Substring(url.IndexOf("=") + 1);
            }

            if (string.IsNullOrEmpty(randomId))
            {
                if (null != LogManager)
                    LogManager.Error("captcha random id can't be null or empty, and App create a GUID");
                randomId = Guid.NewGuid().ToString();
            }

            return randomId;
        }

        #endregion Captcha

        protected override void SetProxy(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                ProxyItem proxy = this.DetectionParamsItem.GetAvaiableProxy();
                if (proxy != null)
                {
                    httpRequest.Proxy = new WebProxy(proxy.IP, proxy.Port);
                }
                else
                {
                    httpRequest.Proxy = WebProxy.GetDefaultProxy();
                }
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            this.headerCollection.Clear();
        }
    }
}