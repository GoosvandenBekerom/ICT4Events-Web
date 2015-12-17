using System;
using System.IO;

namespace SharedModels.Debug
{
    public class Logger
    {
        static readonly TextWriter tw;
        static Logger()
        {
            tw = TextWriter.Synchronized(File.AppendText("Log.txt"));
        }

        private static readonly object _logObject = new object();

        /// <summary>
        /// Writes a message to the log
        /// </summary>
        /// <param name="logMessage">Message to log</param>
        public static void Write(string logMessage)
        {
            try
            {
                Log(logMessage, tw);
            }
            catch (IOException e)
            {
                tw.Close();
                Console.WriteLine(e.Message);
            }
        }

        private static void Log(string logMessage, TextWriter w)
        {
            lock (_logObject)
            {
                w.WriteLine("[{0}]: {1}", DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), logMessage);
                w.Flush();
            }
        }
    }
}
