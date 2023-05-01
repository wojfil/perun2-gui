using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perun2Installer.Actions
{
    abstract class Action
    {
        protected bool Finished = false;

        public bool IsFinished()
        {
            return Finished;
        }

        public void MarkAsFinished()
        {
            Finished = true;
        }

        public abstract bool Do();
        public abstract void Undo();

    }
}
