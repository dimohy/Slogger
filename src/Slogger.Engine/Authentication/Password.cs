using Scrypt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Slogger.Engine.Authentication
{
    public static class Password
    {
        public static string Encode(string password)
        {
            var s = new ScryptEncoder();
            return s.Encode(password);
        }

        public static bool Compare(string password, string hashedPassword)
        {
            var s = new ScryptEncoder();
            return s.Compare(password, hashedPassword);
        }
    }
}
