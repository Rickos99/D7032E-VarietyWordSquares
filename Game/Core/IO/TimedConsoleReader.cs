using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Core.IO
{
    class TimedConsoleReader
    {
        private static Thread inputThread;
        private static AutoResetEvent getInput, gotInput;
        private static string input;

        static TimedConsoleReader()
        {
            getInput = new AutoResetEvent(false);
            gotInput = new AutoResetEvent(false);
            inputThread = new Thread(Reader);
            inputThread.IsBackground = true;
            inputThread.Start();
        }

        private static void Reader()
        {
            while (true)
            {
                getInput.WaitOne();
                input = Console.ReadLine();
                gotInput.Set();
            }
        }

        /// <summary>
        /// Get user input from <see cref="Console"/> with a timeout of <paramref name="timeOutMillisecs"/>
        /// </summary>
        /// <remarks>
        /// If no input is entered before timeout, a <see cref="TimeoutException"/> will be thrown.
        /// </remarks>
        /// <param name="timeOutMillisecs">Number of milliseconds the user has before timeout</param>
        /// <returns>Input the user entered</returns>
        /// <exception cref="TimeoutException"></exception>
        public static string ReadLine(int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                return input;
            else
                throw new TimeoutException("User did not provide input within the timelimit.");
        }

        /// <summary>
        /// Get user input from <see cref="Console"/> with a timeout of <paramref name="timeOutMillisecs"/>
        /// </summary>
        /// <param name="line">Input the user enterd</param>
        /// <param name="timeOutMillisecs">Number of milliseconds the user has before timeout</param>
        /// <returns>True if any input was entered; Otherwise false</returns>
        public static bool TryReadLine(out string line, int timeOutMillisecs = Timeout.Infinite)
        {
            getInput.Set();
            bool success = gotInput.WaitOne(timeOutMillisecs);
            if (success)
                line = input;
            else
                line = null;
            return success;
        }
    }
}
