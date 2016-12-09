using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace GuildWar
{
    public class GameServers
    {
        /// <summary>
        /// GuildWar
        /// </summary>
        public static GameServer GuildWarsServer
        {
            get
            {
                GameServer server = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.GuildWarsServer),
                    GameServerType = GameServerType.GuildWarsServer,
                    SelectAccountUrl = @"",
                    AccountDetailUrl = @"https://account.guildwars2.com/account",
                    DomainUrl = @"https://account.guildwars2.com",
                    LoginUrl = @"https://account.guildwars2.com/login?redirect_uri=https://www.guildwars2.com/en/",
                    LoginPostActionUrl = @"https://account.guildwars2.com/login",
                    //LoginPostActionUrl = @"https://account.guildwars2.com/login?redirect_uri=/account",
                    PasswordResetUrl = @"",
                    UnBattleCharacterUrl = @"",
                    BattleCharacterUrl = @""
                };
                return server;
            }
        }

         /// <summary>
        /// Archeage
        /// </summary>
        public static GameServer ArcheageServer
        {
            get
            {
                GameServer server = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.ArcheageServer),
                    GameServerType = GameServerType.ArcheageServer,
                    SelectAccountUrl = @"",
                    AccountDetailUrl = @"",
                    DomainUrl = @"",
                    LoginUrl = @"",
                    LoginPostActionUrl = @"https://account.archeage.com/user/login/form?login_error=true&forwardUrl=http://account.archeage.com/user/login/form?login_error=true&userId=ddd",
                    PasswordResetUrl = @"",
                    UnBattleCharacterUrl = @"",
                    BattleCharacterUrl = @""
                };
                return server;
            }
        }

        
    }
}
