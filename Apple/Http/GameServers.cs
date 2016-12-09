using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace Apple
{
   public class GameServers
    {
        /// <summary>
        /// 苹果
        /// </summary>
        public static GameServer AppleServer
        {
            get
            {
                GameServer rsServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.AppleServer),
                    GameServerType = GameServerType.AppleServer,
                    SelectAccountUrl = @"",
                    AccountDetailUrl = @"https://p33-buy.itunes.apple.com/WebObjects/MZFinance.woa/wa/authenticate",
                    DomainUrl = @"https://appleid.apple.com",
                    LoginUrl="https://appleid.apple.com/cgi-bin/WebObjects/MyAppleId.woa",
                    //LoginUrl = @"https://appleid.apple.com/cgi-bin/WebObjects/MyAppleId.woa/101/wa/directToSignIn?localang=zh_Cn",
                    //DomainUrl = @"http://www.apple.com/",
                    //LoginUrl = @"https://secure1.store.apple.com/us/order/list",
                    //LoginPostActionUrl = @"https://secure1.store.apple.com/us/order/sentryx/sign_in",
                    PasswordResetUrl = @"",
                    UnBattleCharacterUrl = @"",
                    BattleCharacterUrl = @""
                };
                return rsServer;
            }
        }
    }
}
