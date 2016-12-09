using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using PublicUtilities;

namespace WebDetection
{
    public partial class USBattleLoginHttper : WowHttperLoginBase
    {
        private static List<RealmItem> realmList = new List<RealmItem>();
        private List<UnBattleCharItem> unbattleCharList = new List<UnBattleCharItem>();

        protected bool IsCanGetUnbattleChar
        {
            get
            {
                return (null != this.DetectionParamsItem) &&
                       (this.DetectionParamsItem.IsGetUnbattleChar) &&
                       !string.IsNullOrEmpty(this.DetectionParamsItem.CurrentGameServer.UnBattleCharacterUrl);
            }
        }

        protected bool IsCanGetBattleChar
        {
            get
            {
                return (null != this.DetectionParamsItem) &&
                       (this.DetectionParamsItem.IsGetBattleChar) &&
                       !string.IsNullOrEmpty(this.DetectionParamsItem.CurrentGameServer.BattleCharacterUrl);
            }
        }

        public override void GetCharacters(UserAccountItem userItem)
        {
            if ((null != userItem))
            {
                if (this.IsCanGetBattleChar)
                {
                    this.GetBattleCharacter(userItem);
                }
                if (this.IsCanGetUnbattleChar)
                {
                    this.GetUnbattleCharacter(userItem);
                }
            }
        }
        #region Get Unbattle char
        private void GetUnbattleRealm()
        {
            if (USBattleLoginHttper.realmList.Count == 0)
            {
                if (IsCanGetUnbattleChar)
                {
                    string url = this.DetectionParamsItem.CurrentGameServer.UnBattleCharacterUrl;
                    string html = this.ReadFromUrl(url);
                    if (!string.IsNullOrEmpty(html))
                    {
                        USUnbattleCharGetter cg = new USUnbattleCharGetter(70);
                        USBattleLoginHttper.realmList = cg.GetRealms(ref html);
                    }
                }
            }
        }

        private void GetUnbattleCharacter(UserAccountItem userItem)
        {
            if (userItem.IsCanGetUnbattleCharacter && this.IsCanGetUnbattleChar)
            {
                this.GetUnbattleRealm();
                this.unbattleCharList.Clear();
                this.curretnAccountItem = userItem;
                //WowLogManager.Instance.InfoWithCallback(userItem.UserDetail);
                foreach (RealmItem item in realmList)
                {
                    List<UnBattleCharItem> tempList = GetUnbattleCharacter(item.Num);
                    if ((null != tempList) && (tempList.Count > 0))
                    {
                        this.unbattleCharList.AddRange(tempList);
                        BattleOutptMgt.Instance.LevelDetailCount += tempList.Count;
                        foreach (UnBattleCharItem ci in tempList)
                        {
                            if ((ci.Level >= this.DetectionParamsItem.AvailableCharacterLevel) && this.DetectionParamsItem.IsStopByLevel)
                            {
                                continue;
                            }
                        }
                    }
                    string s = string.Format("当前正在查用户: {0 }的第 {1} 个领域: {2}，有{3} 角色", userItem.EMail, item.Index, item.Name,
                                            (null == tempList) ? 0 : tempList.Count);
                    WowLogManager.Instance.InfoWithCallback(s);
                }

                this.SaveUnbattleChar();
            }
        }

        private List<UnBattleCharItem> GetUnbattleCharacter(int selectedCharacterNum)
        {
            if (this.IsCanGetUnbattleChar)
            {
                string url = this.DetectionParamsItem.CurrentGameServer.UnBattleCharacterUrl;
                string postContent = string.Format(this.PostCharacterContentFormat, selectedCharacterNum);
                string content = string.Empty;

                HttpWebRequest httpRequest = this.WriteToHttpWebRequest(url, postContent);
                if (httpRequest == null)
                {
                    content = HttpHelperBase.HTTPERROR;
                }
                else
                {
                    content = this.ReadFromHttpWebResponse(httpRequest);
                    if (!string.IsNullOrEmpty(content) && !this.IsContains(content, "You have no characters on", "Please choose a different realm"))
                    {
                        USUnbattleCharGetter cg = new USUnbattleCharGetter(this.DetectionParamsItem.AvailableCharacterLevel);
                        return cg.GetCharacter(ref content);
                    }
                }
            }
            return null;
        }

        private void SaveUnbattleChar()
        {
            if ((null != this.curretnAccountItem))
            {
                if ((null != unbattleCharList) && (unbattleCharList.Count > 0))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(curretnAccountItem.UserDetail);
                    int count = 0;

                    unbattleCharList.Sort(new CharItemComparer<UnBattleCharItem>());
                    foreach (UnBattleCharItem ci in unbattleCharList)
                    {
                        sb.AppendLine(string.Format("\t{0}、{1}", ++count, ci.ToString()));
                    }
                    unbattleCharList.Clear();
                    BattleOutptMgt.Instance.OuputCharacter(sb.ToString(), CharacterType.Battle, this.DetectionParamsItem);
                }
                else if (this.curretnAccountItem.IsCanGetDetail)
                {
                    string s = string.Format("{0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.NoCharacter));
                    StringBuilder sb = new StringBuilder();
                    sb.Append(curretnAccountItem.UserDetail);
                    sb.Append(",{ " + s + " }");
                    BattleOutptMgt.Instance.OuputCharacter(sb.ToString(), CharacterType.Battle, this.DetectionParamsItem);
                }
            }
        }

        #endregion

        #region Get battle char

        protected List<BattleCharItem> battleCharList = new List<BattleCharItem>();
        protected virtual void GetBattleCharacter(UserAccountItem userItem)
        {
            if (this.IsCanGetBattleChar)
            {
                this.battleCharList.Clear();
                this.curretnAccountItem = userItem;
                //Get all battle characters
                USBattleCharGetter bg = new USBattleCharGetter(0, this.DetectionParamsItem, this.LogManager, this.cookie, this.cookieContainer);
                this.battleCharList = bg.GetCharacter();
                this.SaveBattleChar();
            }
        }

        #endregion

        protected void SaveBattleChar()
        {
            if ((null != this.curretnAccountItem))
            {
                if ((null != battleCharList) && (battleCharList.Count > 0))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(curretnAccountItem.UserDetail);
                    int count = 0;
                    BattleOutptMgt.Instance.LevelDetailCount += battleCharList.Count;

                    battleCharList.Sort(new CharItemComparer<BattleCharItem>());
                    foreach (BattleCharItem ci in battleCharList)
                    {
                        sb.AppendLine(string.Format("\t{0}、{1}", (++count).ToString().PadRight(2, ' '), ci.ToString()));
                    }
                    battleCharList.Clear();
                    BattleOutptMgt.Instance.OuputCharacter(sb.ToString(), CharacterType.Battle, this.DetectionParamsItem);
                }
                else if (this.curretnAccountItem.IsCanGetDetail)
                {
                    string s = string.Format("{0}", CommentAttributeGetter.GetAttribute<WowLoginStates>(WowLoginStates.NoCharacter));
                    StringBuilder sb = new StringBuilder();
                    sb.Append(curretnAccountItem.UserDetail);
                    sb.Append(",{ " + s + " }");
                    BattleOutptMgt.Instance.OuputCharacter(sb.ToString(), CharacterType.Battle, this.DetectionParamsItem);
                }
            }
        }

        public override void ForceToSave()
        {
            if ((null != this.curretnAccountItem))
            {
                if (((null != battleCharList) && (battleCharList.Count > 0)) ||
                    ((null != unbattleCharList) && (unbattleCharList.Count > 0)))
                {
                    BattleOutptMgt.Instance.Output(curretnAccountItem, this.DetectionParamsItem);
                    SaveUnbattleChar();
                    SaveBattleChar();

                    WowLogManager.Instance.InfoWithCallback(this.curretnAccountItem.UserDetail);
                }
            }
        }
    }

    public class CharItemComparer<T> : Comparer<T> where T : CharItem
    {
        public override int Compare(T x, T y)
        {
            if ((null != x) && (null != y))
            {
                return x.Realm.CompareTo(y.Realm);// | (y.Level - x.Level);
            }
            return 0;
        }
    }
}
