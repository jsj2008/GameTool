using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;

namespace TestProject
{
    public class HtmlRequest
    {
        /// <summary>
        /// MS
        /// </summary>
        public static int ConnectTimeout = 50 * 1000;
        public static char[] BlankChars = { ' ', '\t', '\n', '\r' };
        public const string MethodPost = "POST";
        public const string MethodGet = "GET";
        protected const string HTTPERROR = "HttpError";

        private static object LockedObject = new object();
        protected CookieContainer cookieContainer = new CookieContainer();
        protected string cookie = string.Empty;

        private const string testCookieFormat = @"{0} __utma=134253166.1032902914.1283045384.1283050033.1283050135.3; __utmz=134253166.1283045384.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmb=134253166.1.10.1283050135; __utmc=134253166";

        protected virtual void SetHttpRequestPostHeader(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Method = HtmlRequest.MethodPost;
                httpRequest.Timeout = HtmlRequest.ConnectTimeout;
                httpRequest.KeepAlive = true;
                httpRequest.AllowAutoRedirect = true;
                httpRequest.CookieContainer = this.cookieContainer;

                httpRequest.Accept = @"image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/QVOD, application/QVOD, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                httpRequest.UserAgent = @" Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; CIBA)";
                httpRequest.ContentType = @"application/x-www-form-urlencoded";

                httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");
                httpRequest.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                this.SetCookieToHttpRequestHeader(httpRequest);
                this.AddProxy(httpRequest);
            }
        }

        private static bool? isHasProxy = null;
        protected void AddProxy(HttpWebRequest httpRequest)
        {
            if (isHasProxy.HasValue && !isHasProxy.Value) return;

            if (null != httpRequest)
            {
                string proxyIp = "192.168.80.222";
                if (!isHasProxy.HasValue)
                {
                    using (Ping ping = new Ping())
                    {
                        try
                        {
                            PingReply pr = ping.Send(proxyIp);
                            isHasProxy = pr.Status == IPStatus.Success;
                        }
                        catch (System.Exception ex)
                        {
                            Trace.WriteLine(ex.Message);
                        }
                    }
                }
                if (isHasProxy.HasValue && isHasProxy.Value)
                {
                    httpRequest.Proxy = new WebProxy(proxyIp, 3128);
                }
            }
        }

        private void SetCookieToHttpRequestHeader(HttpWebRequest httpRequest)
        {
            string tempCookie = string.Empty;
            if (!string.IsNullOrEmpty(this.cookie))
            {
                if (this.cookie.Contains("; Path=/login; Secure; HttpOnly"))
                {
                    tempCookie = string.Format(testCookieFormat, this.cookie.Replace("; Path=/login; Secure; HttpOnly", ";"));
                }
                httpRequest.Headers.Add(HttpRequestHeader.Cookie, tempCookie);
            }
        }

        protected virtual void SetHttpRequestGetHeader(HttpWebRequest httpRequest)
        {
            if (null != httpRequest)
            {
                httpRequest.Method = HtmlRequest.MethodGet;
                httpRequest.Timeout = HtmlRequest.ConnectTimeout;
                httpRequest.CookieContainer = this.cookieContainer;
                httpRequest.Accept = @"image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/QVOD, application/QVOD, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                httpRequest.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.2; .NET4.0C; .NET4.0E; CIBA)";
                httpRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");
                httpRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
                httpRequest.KeepAlive = true;
                httpRequest.AllowAutoRedirect = true;
                this.SetCookieToHttpRequestHeader(httpRequest);
                this.AddProxy(httpRequest);
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

        protected void SaveCookie(HttpWebResponse httpResponse)
        {
            this.cookie = httpResponse.Headers.Get("Set-Cookie");
            foreach (Cookie item in httpResponse.Cookies)
            {
                this.cookieContainer.Add(item);
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
                    // TODO:use the same session id
                    char[] postDataBytes = postContent.ToCharArray();
                    httpRequest.ContentLength = postDataBytes.Length;
                    using (StreamWriter sw = new StreamWriter(httpRequest.GetRequestStream(), Encoding.ASCII))
                    {
                        sw.Write(postDataBytes, 0, postDataBytes.Length);
                        sw.Flush();
                    }
                }
            }
            catch (WebException we)
            {
                Trace.WriteLine(string.Format("HtmlRequest.WriteToHttpWebRequest() WebException:{0}", we.Message));
                return null;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("-> 向网站提交数据{0}时出错 ", postContent));
                Trace.WriteLine(string.Format("HtmlRequest.WriteToHttpWebRequest() Exception:{0}", ex.Message));
            }
            return httpRequest;
        }

        /// <summary>
        /// Get httpwebrequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="isPost"></param>
        /// <returns></returns>
        protected HttpWebRequest GetHttpWebRequest(string url, bool isPost)
        {
            HttpWebRequest httpRequest = WebRequest.Create(url) as HttpWebRequest;
            try
            {
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
                    return httpRequest;
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(string.Format("HtmlRequest.GetHttpWebRequest() error:{0}", ex.Message));
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
                Trace.WriteLine(string.Format("HtmlRequest.GetHttpWebRequest() error:{0}", ex.Message));
            }
            finally
            {
                this.DisposeHttpRequest(httpRequest);
            }
            return string.Empty;
        }

        protected string ReadFromResponseContent(string url, bool isPost)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            string content = HtmlRequest.HTTPERROR;

            HttpWebRequest httpRequest = this.GetHttpWebRequest(url, isPost);

            if (null != httpRequest)
            {
                try
                {
                    Trace.WriteLine(string.Format("ReadFromResponseContent() from :{0}", url));
                    content = this.ReadFromHttpWebResponse(httpRequest);
                    return content;
                }
                catch (System.Exception ex)
                {
                    Trace.WriteLine(string.Format("HtmlRequest.ReadFromResponseContent() error:{0}", ex.Message));
                    content = HtmlRequest.HTTPERROR;
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
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(string.Format("HttpRequest.Abort failed:{0}", ex.Message));
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
            string content = HtmlRequest.HTTPERROR;
            try
            {
                if (null != httpRequest)
                {
                    HttpWebResponse httpResponse = null;
                    try
                    {
                        httpResponse = httpRequest.GetResponse() as HttpWebResponse;
                    }
                    catch (WebException we)
                    {
                        httpResponse = we.Response as HttpWebResponse;
                    }

                    // TODO:use the same session id
                    if (null != httpResponse)
                    {
                        using (httpResponse)
                        {
                            //Trace.WriteLine(string.Format("HtmlRequest.ReadFromHttpWebResponse() httpReponse.StatusCode={0}", httpResponse.StatusCode));
                            this.SaveCookie(httpResponse);
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
                Trace.WriteLine(string.Format("HtmlRequest.ReadFromHttpWebResponse() error:{0}", ex.Message));
                content = HtmlRequest.HTTPERROR;
            }

            return content;
        }

        protected bool IsContains(string rawContent, params string[] subStrings)
        {
            lock (LockedObject)
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
        }

        protected static Stream GetGzipStream(HttpWebResponse webReponse)
        {
            lock (LockedObject)
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
        }


        #region Sync / Async get html

        /// <summary>
        /// Get url content by Get method
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string ReadFromResponseContent(string url)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Trace.WriteLine(string.Format("--- Start ayncRead --"));
            try
            {
                string s = ReadFromResponseContent(url, false);
                // Trace.WriteLine(s);
                return s;
            }
            finally
            {
                sw.Stop();
                long times = sw.ElapsedMilliseconds;
                Trace.WriteLine(string.Format("-- Sync spends:{0}----", times));
            }
        }

        Stopwatch asyncSW = null;
        public string AsyncRead(string url)
        {
            asyncSW = new Stopwatch();
            asyncSW.Start();
            Trace.WriteLine(string.Format("/** Start AsyncRead **/"));
            HttpWebRequest httpRequest = this.GetHttpWebRequest(url, false);
            if (null != httpRequest)
            {
                IAsyncResult ar = httpRequest.BeginGetResponse(new AsyncCallback(AsyncHtmlCallback), httpRequest);
                //ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, null, null, 10, true);
            }
            return string.Empty;
        }

        private void AsyncHtmlCallback(IAsyncResult ar)
        {
            Trace.WriteLine(string.Format("{0}", ar.AsyncState));
            using (HttpWebResponse httpResponse = (ar.AsyncState as HttpWebRequest).EndGetResponse(ar) as HttpWebResponse)
            {
                using (StreamReader sr = new StreamReader(GetGzipStream(httpResponse), Encoding.UTF8))
                {
                    string str = sr.ReadToEnd();
                    //Trace.WriteLine(str);
                }
            }
            asyncSW.Stop();
            long times = asyncSW.ElapsedMilliseconds;
            Trace.WriteLine(string.Format(",** Aync spends:{0} **/", times));
        }
        #endregion
    }
}
