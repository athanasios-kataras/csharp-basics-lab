using System.Collections.Generic;
using Microsoft.CSharp.Basics.Solid.Code.Interfaces;

namespace Microsoft.CSharp.Basics.Solid.UnitTests.Tests
{
    public class FakeLogger : ILogger
    {
        public List<string> LoggedMessages { get; } = new List<string>();
        public void Log(string message)
        {
            LoggedMessages.Add(message);
        }
    }
}