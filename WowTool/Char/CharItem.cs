
namespace WebDetection
{
    /// <summary>
    /// 非战领域
    /// </summary>
    public class RealmItem
    {
        public int Index { get; set; }
        public int Num { get; set; }
        public string Name { get; set; }
    }

    public class CharItem
    {
        public int Level { get; set; }
        public string Realm { get; set; }
    }

    /// <summary>
    /// 非战角色信息
    /// </summary>
    public class UnBattleCharItem : CharItem
    {
        public string Character { get; set; }
        public string Race_Class { get; set; }
        public string Eligibility { get; set; }
        public string Options { get; set; }

        public override string ToString()
        {
            return string.Format("角色:{0}; 等级:{1};种族:{2};领域{3};资格:{4};其他选项:{5}",
                this.Character, this.Level, this.Race_Class, this.Realm, this.Eligibility, this.Options);
        }
    }

    /// <summary>
    /// 战网角色信息
    /// </summary>
    public class BattleCharItem : CharItem
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string LevelStr { get; set; }
        public string Href { get; set; }
        public int Gold { get; set; }
        public int Sliver { get; set; }
        public int Copper { get; set; }
        public string Money { get; set; }

        public override string ToString()
        {
            return string.Format("序号:{0}; 名称:{1}; 等级:{2}; 领域:{3}; 金币:{4}; 银币:{5}; 铜币:{6}, Money:{7}",
                                               this.Index.ToString().PadRight(2, ' '), this.Name.PadRight(40, ' '),
                                               this.LevelStr.PadRight(20, ' '), this.Realm.PadRight(20, ' '),
                                               this.Gold.ToString().PadRight(6, ' '),
                                               this.Sliver.ToString().PadRight(6, ' '), this.Copper.ToString().PadRight(6, ' '),
                                               this.Money);
        }
    }

}
