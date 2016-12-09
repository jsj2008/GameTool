using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PublicUtilities;
using System.Net;
using System.Web.Security;
using System.IO;
using System.IO.Compression;

namespace GameGift
{
    public class HttpHelper
    {
        public static string Get(ItemBase user)
        {
            string md5Pwd = FormsAuthentication.HashPasswordForStoringInConfigFile(user.Password, "MD5");
            string url = string.Format(@"http://zt.xoyo.com/mj/cdkey/cdkey.php?callback=jQuery&game=1&refer=&passport={0}&password={1}&submit=%E9%A2%86%E5%8F%96%E6%96%B0%E6%89%8B%E5%8D%A1&_=",
                user.User, md5Pwd);
            //+new Date().getTime();
            string result = string.Empty;
            HttpWebRequest httpRequest = null;
            try
            {
                httpRequest = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse httpResponse = httpRequest.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader sr = new StreamReader(GetGzipStream(httpResponse), Encoding.UTF8))
                    {
                        ///jQuery(({"code":"363654D889384c87","type":2,"passport":"jftail"}))
                        result = sr.ReadToEnd();

                        if (string.IsNullOrEmpty(result) || TextHelper.IsContains(result, "jQuery(-4)"))
                        {
                            return result;
                        }

                        string[] values = result.Split(',');
                        string[] codes = values[0].Split(':');
                        result = codes[1].Replace('\"', ' ');
                        return result;
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
            finally
            {
                if (null != httpRequest)
                {
                    httpRequest.Abort();
                }
            }

            return result;
        }

        protected static Stream GetGzipStream(HttpWebResponse webReponse)
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
}
