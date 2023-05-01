using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    abstract class UnloadAction : Action
    {
        protected void Create(string path, byte[] bytes)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            System.IO.File.WriteAllBytes(path, bytes);
        }

        protected void Delete(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
