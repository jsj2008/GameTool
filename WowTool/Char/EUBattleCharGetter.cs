using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using PublicUtilities;
using LitJson;

namespace WebDetection
{
    public class EUBattleCharGetter : USBattleCharGetter
    {
        public EUBattleCharGetter(int level, DetectionParamsItem paramsItem, LogManagerBase logManager,
            string cookieStr, CookieContainer cookieContainer)
            : base(level, paramsItem, logManager, cookieStr, cookieContainer)
        {
        }


        protected override void SetHttpRequestHeader(HttpWebRequest httpRequest, bool isPost, HttpHeaderType headerType)
        {
            base.SetHttpRequestHeader(httpRequest, isPost, headerType);
            //if (isPost)
            //{
            //    httpRequest.Accept = @"*/*";
            //    httpRequest.Referer = this.DetectionParamsItem.GameServer.BattleCharacterUrl;
            //}

            switch (headerType)
            {
                case HttpHeaderType.Json:
                    httpRequest.UserAgent = @"Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; .NET4.0E)";
                    httpRequest.Accept = @"application/json, text/javascript, */*; q=0.01";
                    //httpRequest.Referer = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/";
                    httpRequest.Referer = this.DetectionParamsItem.CurrentGameServer.BattleGetGUrl;
                    httpRequest.ContentLength = 0;
                    httpRequest.ContentType = string.Empty;
                    // httpRequest.ContentType = "application/json;charset=utf-8";
                    // httpRequest.AllowAutoRedirect = false;
                    // httpRequest.Headers.Add(HttpRequestHeader.ContentLength, "0");
                    httpRequest.Headers.Add(HttpRequestHeader.Pragma, "no-cache");
                    break;
                case HttpHeaderType.XmlHttpRequest:
                    httpRequest.ContentType = "application/x-www-form-urlencoded";
                    httpRequest.Accept = "*/*";
                    httpRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                    break;
                default:
                    break;

            }
        }

        protected override void GetGValue(BattleCharItem item)
        {
            if (null == item) return;
            //index=0&xtoken=74c15f4a-347a-4c8e-b7aa-26704ac45ad9
            const string PostGFormat = "index={0}{1}";
            //lastMailId=0&xtoken=24d9b826-251b-4e42-ad80-dc775ad6ccee
            const string firstPostGFormate = "lastMailId=0{0}";

            string gUrl = this.DetectionParamsItem.CurrentGameServer.BattleGSelectCharUrl;
            string postData = string.Empty;
            if (item.Index <= 0)
            {
                postData = string.Format(firstPostGFormate, this.GetGxstoken());
            }
            else
            {
                postData = string.Format(PostGFormat, item.Index, this.GetGxstoken());
            }
            string html = string.Empty;
            if (item.Index > 0)
                html = this.ReadUrlContent(gUrl, postData);
            //html = this.ReadFromUrl(item.Href);
            if (!isQueryed)
            {
                html = this.ReadFromUrl(this.DetectionParamsItem.CurrentGameServer.BattleGetGUrl);
                isQueryed = true;
            }
            int i = 0;
            do
            {
                this.httpHeaderType = HttpHeaderType.Json;
                html = this.ReadFromUrl(this.DetectionParamsItem.CurrentGameServer.BattleGetGUrlMoney, true);
                this.httpHeaderType = HttpHeaderType.Normal;

                string moneyDiv = "\"money\"";
                if (TextHelper.IsContains(html, moneyDiv))
                {
                    JsonData jd = JsonMapper.ToObject(html);
                    int money = (int)jd["money"];
                    item.Money = money.ToString();
                    item.Gold = money / 10000;
                    item.Sliver = (money - item.Gold * 10000) / 100;
                    item.Copper = money - item.Gold * 10000 - item.Sliver * 100;
                    return;
                }
                this.LogManager.Error(string.Format("Char:{0} get G 值失败，开始重试:{1}", item.Name, postData));
            } while (i++ < 1);

            item.Money = "取不到G值或者出错";
        }
    }
}
