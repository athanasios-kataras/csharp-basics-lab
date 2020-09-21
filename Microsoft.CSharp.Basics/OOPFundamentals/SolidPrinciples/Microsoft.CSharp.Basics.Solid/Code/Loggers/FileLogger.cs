using System.IO;
using Microsoft.CSharp.Basics.Solid.Code.Interfaces;

namespace Microsoft.CSharp.Basics.Solid.Code.Loggers
{
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            File.AppendAllText("log.txt", message);
        }
    }
}