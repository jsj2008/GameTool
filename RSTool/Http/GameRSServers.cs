using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace RSTool
{
   public class GameRSServers
    {
        /// <summary>
        /// 江湖
        /// </summary>
        public static GameServer RSServer
        {
            get
            {
                GameServer rsServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.RSServer),
                    GameServerType = GameServerType.RSServer,
                    SelectAccountUrl = @"",
                    AccountDetailUrl = @"https://secure.runescape.com/m=weblogin/loginform.ws?mod=www&ssl=0&dest=",
                    DomainUrl = @"https://secure.runescape.com/",
                    LoginUrl = @"https://secure.runescape.com/m=weblogin/loginform.ws?mod=www&ssl=0&dest=",
                    LoginPostActionUrl=@"https://secure.runescape.com/m=weblogin/login.ws",
                    PasswordResetUrl = @"",
                    UnBattleCharacterUrl = @"",
                    BattleCharacterUrl = @""
                };
                return rsServer;
            }
        }
    }
}
