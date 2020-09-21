using System;
using Microsoft.CSharp.Basics.Solid.Code.Raters;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class ConsoleLogger: ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}