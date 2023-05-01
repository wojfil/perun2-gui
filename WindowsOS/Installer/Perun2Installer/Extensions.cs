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
    }
}
