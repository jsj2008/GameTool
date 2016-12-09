using System;
using System.Collections.Generic;
using System.Text;
using PublicUtilities;

namespace XBOX
{
   public class GameServers
    {
        /// <summary>
        /// XBOX
        /// </summary>
        public static GameServer XBOXServer
        {
            get
            {
                GameServer rsServer = new GameServer()
                {
                    Header = CommentAttributeGetter.GetAttribute<GameServerType>(GameServerType.XBOXServer),
                    GameServerType = GameServerType.XBOXServer,
                    SelectAccountUrl = @"",
                    AccountDetailUrl = @"",
                    DomainUrl = @"http://www.xbox.com/",
                    LoginUrl = "https://live.xbox.com/Account/Signin?returnUrl=http%3A%2F%2Fsupport.xbox.com%2Fen-US%2Fxbox-live%2Fbrowse%3Fxr%3Dshellnav",
                    //LoginUrl = @"https://live.xbox.com/Account/Signin?returnUrl=http%3a%2f%2fwww.xbox.com%2fen-US%2f",   
                    //LoginPostActionUrl = @"https://login.live.com/ppsecure/post.srf?wa=wsignin1.0&rpsnv=11&ct=1338699810&rver=6.2.6289.0&wp=MBI&wreply=https:%2F%2Flive.xbox.com:443%2Fxweb%2Flive%2Fpassport%2FsetCookies.ashx%3Frru%3Dhttps%253a%252f%252flive.xbox.com%252fen-US%252fAccount%252fSignin%253freturnUrl%253dhttp%25253a%25252f%25252fwww.xbox.com%25252fen-US%25252f%25253flc%25253d1033&lc=1033&id=66262&cbcxt=0&bk=1338700220",
                    PasswordResetUrl = @"",
                    UnBattleCharacterUrl = @"",
                    BattleCharacterUrl = @""
                };
                return rsServer;
            }
        }
    }
}
