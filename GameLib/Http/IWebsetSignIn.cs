using System;
using System.Collections.Generic;
using System.Text;

namespace PublicUtilities
{
    public interface IWebsetSignIn
    {
        void SignIn(GameServer server, string user, string password);
        void Stop();
    }
}
