using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RiftTool
{
    public class GameRiftServers
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
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.USRift),
                    GameServerType = GameServerType.USRift,
                    SelectAccountUrl = @"",
                    AccountDetailUrl = @"https://rift.trionworlds.com/account/profile/manage-games.action",
                    DomainUrl = @"http://www.riftgame.com/en/",
                    LoginUrl = @"https://session.trionworlds.com/login?service=http%3A%2F%2Frift.trionworlds.com%2Fj_spring_cas_security_check",
                    LoginPostActionUrl = @"https://session.trionworlds.com/login?service=http%3A%2F%2Frift.trionworlds.com%2Fj_spring_cas_security_check",
                    PasswordResetUrl = @"",
                    UnBattleCharacterUrl = @"",
                    BattleCharacterUrl = @""
                };
                return usaServer;
            }
        }
    }
}
