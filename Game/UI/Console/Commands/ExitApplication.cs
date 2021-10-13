using Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI.Console.Commands
{
    class ExitApplication : ICommand
    {
        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}
