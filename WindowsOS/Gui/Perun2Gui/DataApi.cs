/*
    This file is part of Perun2.
    Perun2 is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    Perun2 is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with Perun2. If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using System.IO;

namespace Perun2Gui
{
    public static class DataApi
    {
        public static bool GetCurrentVersion(out string version)
        {
            try
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(Paths.GetInstance().GetExePath());
                version = info.ProductVersion;
                return true;
            }
            catch (Exception)
            {
                version = "unknown";
                return false;
            }
        }

        public static bool GetLatestVersion(out string version)
        {
            try
            {
                Connection.GetInstance().Init();
                var request = (HttpWebRequest)WebRequest.Create(Constants.VERSION_API);
                request.Method = "GET";

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var encoding = Encoding.GetEncoding(response.CharacterSet);

                    using (var responseStream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(responseStream, encoding))
                        {
                            string data = reader.ReadToEnd();
                            if (data.IsValidVersion())
                            {
                                version = data;
                                return true;
                            }
                            else
                            {
                                version = "unknown";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception) { }

            version = "unknown";
            return false;
        }
    }
}
