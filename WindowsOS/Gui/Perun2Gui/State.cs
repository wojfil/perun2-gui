using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perun2Gui
{
    class State
    {
        public State()
        {
            LocationPathString = String.Empty;
            FilePathString = String.Empty;
            FileNameString = String.Empty;
            BackupPathString = String.Empty;
        }

        public bool HasLocation()
        {
            return ! String.IsNullOrWhiteSpace(this.LocationPathString);
        }

        public bool HasFile()
        {
            return ! String.IsNullOrWhiteSpace(this.FilePathString);
        }

        public bool HasBackup()
        {
            return ! String.IsNullOrWhiteSpace(this.BackupPathString);
        }

        //public bool HasLocation;
        //public bool HasFile;
        //public bool HasBackup;
        public string LocationPathString;
        public string FilePathString;
        public string FileNameString;
        public string BackupPathString;

        
    }
}
