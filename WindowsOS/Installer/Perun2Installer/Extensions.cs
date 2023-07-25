using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer
{
    public static class Extensions
    {
        public static string Quoted(this string s)
        {
            return "\"" + s + "\"";
        }

        public static string EscapeQuote(this string s)
        {
            if (s.Length >= 2 && s.StartsWith("\"") && s.EndsWith("\""))
            {
                return s.Substring(1, s.Length - 2);
            }

            return s;
        }
    }
}
