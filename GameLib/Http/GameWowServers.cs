using System;
using System.Collections.Generic;
using System.Text;

namespace PublicUtilities
{
    public class GameWowServers
    {
        /// <summary>
        /// 美服
        /// </summary>
        public static GameServer USServer
        {
            get
            {
                GameServer usaServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.USBattle),
                    GameServerType = GameServerType.USBattle,
                    //SelectAccountUrl = @"https://www.worldofwarcraft.com/account/change-account.html",
                    SelectAccountUrl = @"https://us.battle.net/account/management/index.xml?lnk=1",
                    AccountDetailUrl = @"https://us.battle.net/account/management/wow/dashboard.html?region=US&accountName=",
                    DomainUrl = @"https://us.battle.net",
                    LoginPostActionUrl = @"https://us.battle.net/login/en/login.xml?app=wam&ref=https%3A%2F%2Fwww.worldofwarcraft.com%2Faccount%2F&cr=true",
                    PasswordResetUrl = @"https://us.battle.net/account/support/password-reset.html",
                    UnBattleCharacterUrl = @"https://www.worldofwarcraft.com/account/character-customization.html",
                    BattleCharacterUrl = @"http://us.battle.net/wow/en/forum/",
                    BattleGSelectCharUrl = @"http://us.battle.net/wow/en/pref/character",
                    BattleGetGUrl = @"http://us.battle.net/wow/en/vault/character/auction/alliance/",
                    BattleGetGUrlMoney = @"http://us.battle.net/wow/en/vault/character/auction/alliance/money"
                };
                return usaServer;
            }
        }

        /// <summary>
        /// 欧服
        /// </summary>
        public static GameServer ENServer
        {
            get
            {
                GameServer euServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.EUBattle),
                    GameServerType = GameServerType.EUBattle,
                    SelectAccountUrl = @"https://eu.battle.net/account/management/index.xml?lnk=1",
                    AccountDetailUrl = @"https://en.battle.net/account/management/wow/dashboard.html?region=US&accountName=",
                    DomainUrl = @"https://eu.battle.net",
                    LoginPostActionUrl = @"https://eu.battle.net/login/en/login.xml?ref=https%3A%2F%2Fwww.wow-europe.com%2Faccount%2F&app=wam&rhtml=true",
                    PasswordResetUrl = @"https://eu.battle.net/account/support/password-reset.html",
                    UnBattleCharacterUrl = @"https://www.worldofwarcraft.com/account/character-customization.html",
                    BattleCharacterUrl = @"http://eu.battle.net/wow/en/forum/",
                    BattleGSelectCharUrl = @"http://eu.battle.net/wow/en/pref/character",
                    BattleGetGUrl = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/",
                    BattleGetGUrlMoney = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/money"
                    //https://eu.battle.net/wow/en/vault/character/auction/horde/index 
                };

                return euServer;
            }
        }

        /// <summary>
        /// 东南亚服
        /// </summary>
        public static GameServer SEAServer
        {
            get
            {
                GameServer seaServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.SEABattle),
                    GameServerType = GameServerType.SEABattle,
                    SelectAccountUrl = @"https://sea.battle.net/account/management/index.xml?lnk=1",
                    ///TODO:set the region to SEA
                    AccountDetailUrl = @"https://sea.battle.net/account/management/wow/dashboard.html?region=US&accountName=",
                    DomainUrl = @"https://sea.battle.net",
                    LoginPostActionUrl = @"https://sea.battle.net/login/en/?ref=https%3A%2F%2Fsea.battle.net%2Faccount%2Fmanagement%2Findex.xml&app=bam&cr=true",
                    PasswordResetUrl = @"https://sea.battle.net/account/support/password-reset.html",
                    UnBattleCharacterUrl = @"https://www.worldofwarcraft.com/account/character-customization.html",
                    BattleCharacterUrl = @"https://eu.battle.net/wow/en/forum",
                    BattleGSelectCharUrl = @"https://eu.battle.net/wow/en/pref/character",
                    BattleGetGUrl = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/",
                    BattleGetGUrlMoney = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/money"
                };
                return seaServer;
            }
        }

        /// <summary>
        /// 韩服
        /// </summary>
        public static GameServer KRServer
        {
            get
            {
                GameServer krServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.KRBattle),
                    GameServerType = GameServerType.KRBattle,
                    SelectAccountUrl = @"https://kr.battle.net/account/management/index.xml?lnk=1",
                    ///TODO:set the region to KR
                    AccountDetailUrl = @"https://kr.battle.net/account/management/wow/dashboard.html?region=US&accountName=",
                    DomainUrl = @"https://kr.battle.net",
                    LoginPostActionUrl = @"https://kr.battle.net/login/en/?ref=https%3A%2F%2Fkr.battle.net%2Faccount%2Fmanagement%2Findex.xml&app=bam&cr=true",
                    PasswordResetUrl = @"https://kr.battle.net/account/support/password-reset.html",
                    UnBattleCharacterUrl = @"https://www.worldofwarcraft.com/account/character-customization.html",
                    BattleCharacterUrl = @"https://eu.battle.net/wow/en/forum",
                    BattleGSelectCharUrl = @"https://eu.battle.net/wow/en/pref/character",
                    BattleGetGUrl = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/",
                    BattleGetGUrlMoney = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/money"
                };
                return krServer;
            }
        }

        /// <summary>
        /// 台服
        /// </summary>
        public static GameServer TWServer
        {
            get
            {
                GameServer twServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.TWBattle),
                    GameServerType = GameServerType.TWBattle,
                    SelectAccountUrl = @"https://tw.battle.net/account/management/index.xml",
                    ///TODO:set the region to TW
                    AccountDetailUrl = @"https://tw.battle.net/account/management/wow/dashboard.html?region=US&accountName=",
                    DomainUrl = @"https://tw.battle.net",
                    LoginPostActionUrl = @"https://tw.battle.net/login/en/?ref=https%3A%2F%2Ftw.battle.net%2Faccount%2Fmanagement%2Findex.xml&app=bam&cr=true",
                    PasswordResetUrl = @"https://tw.battle.net/account/support/password-reset.html",
                    UnBattleCharacterUrl = @"https://www.worldofwarcraft.com/account/character-customization.html",
                    BattleCharacterUrl = @"https://eu.battle.net/wow/en/forum",
                    BattleGSelectCharUrl = @"https://eu.battle.net/wow/en/pref/character",
                    BattleGetGUrl = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/",
                    BattleGetGUrlMoney = @"http://eu.battle.net/wow/en/vault/character/auction/alliance/money"
                };
                return twServer;
            }
        }
    }
}
