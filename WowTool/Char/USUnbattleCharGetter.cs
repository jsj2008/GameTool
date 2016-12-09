using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PublicUtilities;

namespace WebDetection
{
    /// <summary>
    /// 非战角色获取
    /// </summary>
    public class USUnbattleCharGetter
    {
        private List<RealmItem> realmList = new List<RealmItem>();
        private List<UnBattleCharItem> characterList = new List<UnBattleCharItem>();
        private int availableLeve = 70;
        HtmlParseHelper HtmlParser = new HtmlParseHelper();

        public USUnbattleCharGetter(int level)
        {
            availableLeve = level;
        }

        private void GetRealmsAndCharacter()
        {
            string html = GetText();
            if (string.IsNullOrEmpty(html))
            {
                return;
            }

            this.GetRealms(ref html);
            this.GetCharacter(ref html);
        }

        public static string GetText()
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "文本文件(*.txt)|*.txt|所有文件(*.*)|*.*";
            openDlg.Multiselect = false;
            openDlg.ShowDialog();
            if (File.Exists(openDlg.FileName))
            {
                using (StreamReader sr = new StreamReader(openDlg.FileName))
                {
                    string html = sr.ReadToEnd();
                    return html;
                }
            }
            return string.Empty;
        }

        public List<RealmItem> GetRealms(ref string html)
        {
            realmList.Clear();

            string optionStart = "<option";
            IList<string> optionList = new List<string>();
            int i = 1;
            while (html.IndexOf(optionStart) != -1)
            {
                string item = HtmlParser.GetOuterTextFromHtml(optionStart, "</option>", 1, html);
                if (!string.IsNullOrEmpty(item))
                {
                    string num = HtmlParser.GetOutterPropertyFromHtml(item, "value");
                    string name = HtmlParser.GetInnerTextFromHtml(item);

                    if (!string.IsNullOrEmpty(num) && (num != "0") && TextHelper.IsNumber(num) && !string.IsNullOrEmpty(name))
                    {
                        realmList.Add(new RealmItem() { Index = i++, Num = TextHelper.StringToInt(num), Name = name });
                    }

                    html = html.Replace(item, "");
                }
            }
            return realmList;
        }

        public List<UnBattleCharItem> GetCharacter(ref string html)
        {
            characterList.Clear();
            string charStart = "<form method=\"post\" name=\"form";

            while (html.IndexOf(charStart) != -1)
            {
                string item = HtmlParser.GetOuterTextFromHtml(charStart, "</form>", 1, html);
                if (!string.IsNullOrEmpty(item))
                {
                    html = html.Replace(item, "");
                    if (!TextHelper.IsContains(item, "selectedCharacterId"))
                    {
                        continue;
                    }
                    string character = TextHelper.TrimHtml(GetChar(ref item));
                    string lvl = TextHelper.TrimHtml(GetLvl(ref item));
                    string race_Class = TextHelper.TrimHtml(GetRace_Class(ref item));
                    string realm = TextHelper.TrimHtml(GetRealm(ref item));
                    string eligibility = TextHelper.TrimHtml(GetEligibility(ref item));
                    string options = TextHelper.TrimHtml(GetOptions(ref item));

                    if (!string.IsNullOrEmpty(character))
                    {
                        if (!string.IsNullOrEmpty(lvl) && TextHelper.IsNumber(lvl))
                        {
                            int level = TextHelper.StringToInt(lvl);
                            UnBattleCharItem ci = new UnBattleCharItem()
                            {
                                Character = character,
                                Level = level,
                                Race_Class = race_Class,
                                Realm = realm,
                                Eligibility = eligibility,
                                Options = options
                            };
                            if (level >= availableLeve)
                            {
                                characterList.Add(ci);
                            }
                        }
                    }
                }
            }
            return characterList;
        }

        private string GetChar(ref string html)
        {
            string td = HtmlParser.GetOuterTextFromHtml("<td NOWRAP", "</b></span></td>", 1, html);
            if (string.IsNullOrEmpty(td)) return string.Empty;
            html = html.Replace(td, "");

            string character = HtmlParser.GetOuterTextFromHtml("<b><input type=\"hidden\" name=\"movingCharacterGuid\"", "</b>", 1, td);
            if (!string.IsNullOrEmpty(character))
            {
                html = html.Replace(character, "");
                return HtmlParser.GetInnerTextFromHtml(character);
            }
            return string.Empty;
        }
        private string GetLvl(ref string html)
        {
            //string lvl = HtmlParser.GetOuterTextFromHtml("<td NOWRAP class = \"serverStatus2\"", "</b>", 1, html);
            string lvl = HtmlParser.GetOuterTextFromHtml("<td NOWRAP", "</b></span></td>", 1, html);
            if (!string.IsNullOrEmpty(lvl))
            {
                html = html.Replace(lvl, "");
                return HtmlParser.GetInnerTextFromHtml(lvl);
            }
            return string.Empty;
        }
        private string GetRace_Class(ref string html)
        {
            //string item = HtmlParser.GetOuterTextFromHtml("<td NOWRAP class = \"serverStatus2\"", "</td>", 1, html);
            string item = HtmlParser.GetOuterTextFromHtml("<td NOWRAP", "</small></span></td>", 1, html);
            if (!string.IsNullOrEmpty(item))
            {
                html = html.Replace(item, "");
            }
            string race = HtmlParser.GetOuterTextFromHtml("<img src = \"images/race", ">", 1, item);
            if (!string.IsNullOrEmpty(race))
            {
                item = item.Replace(race, "");
                race = HtmlParser.GetOutterPropertyFromHtml(race, "onMouseover");
                race = race.Replace("ddrivetip('", "");
                race = race.Replace("')", "");
            }

            string cls = HtmlParser.GetOuterTextFromHtml("<img src = \"images/class", ">", 1, item);
            if (!string.IsNullOrEmpty(cls))
            {
                item = item.Replace(cls, "");
                cls = HtmlParser.GetOutterPropertyFromHtml(cls, "onMouseover");
                cls = race.Replace("ddrivetip('", "");
                cls = race.Replace("')", "");
            }
            if (!string.IsNullOrEmpty(race) && !string.IsNullOrEmpty(cls))
            {
                return string.Format("{0}/{1}", race, cls);
            }
            return string.Empty;
        }
        private string GetRealm(ref string html)
        {
            //string realm = HtmlParser.GetOuterTextFromHtml("<td NOWRAP class = \"serverStatus2\" align=\"left\">", "</td>", 1, html);
            string realm = HtmlParser.GetOuterTextFromHtml("<td NOWRAP", "</td>", 1, html);
            if (!string.IsNullOrEmpty(realm))
            {
                html = html.Replace(realm, "");
                realm = HtmlParser.GetOuterTextFromHtml("<b>", "</b>", 1, realm);
                return HtmlParser.GetInnerTextFromHtml(realm);
            }
            return string.Empty;
        }
        private string GetEligibility(ref string html)
        {
            //string eligibility = HtmlParser.GetOuterTextFromHtml("<td NOWRAP class = \"serverStatus2b\" align=\"center\">", "</small>", 1, html);
            string eligibility = HtmlParser.GetOuterTextFromHtml("<td NOWRAP", "</small></span></td>", 1, html);
            if (!string.IsNullOrEmpty(eligibility))
            {
                html = html.Replace(eligibility, "");
                eligibility = HtmlParser.GetOuterTextFromHtml("<small>", "</small>", 1, eligibility);
                return HtmlParser.GetInnerTextFromHtml(eligibility);
            }
            return string.Empty;
        }
        private string GetOptions(ref string html)
        {
            const string customizeIcon = "/account/images/pcc/button-customize.gif";
            const string customize = "Customize";
            //string options = HtmlParser.GetOuterTextFromHtml("<td NOWRAP class = \"serverStatus2b\" align=\"center\">", "</b>", 1, html);
            string options = HtmlParser.GetOuterTextFromHtml("<td NOWRAP", "</td>", 1, html);
            if (!string.IsNullOrEmpty(options))
            {
                html = html.Replace(options, "");
                string result = HtmlParser.GetOutterPropertyFromHtml(options, "src");
                if (string.IsNullOrEmpty(result))
                {
                    options = HtmlParser.GetOuterTextFromHtml("<b>", "</b>", 1, options);
                    return HtmlParser.GetInnerTextFromHtml(options);
                }

                if (TextHelper.IsContains(result, customizeIcon))
                {
                    return customize;
                }
                return result;
            }
            return string.Empty;
        }
    }
}
