using Microsoft.CSharp.Basics.Solid.Code.Interfaces;

namespace Microsoft.CSharp.Basics.Solid.UnitTests.Tests
{
    public class FakePolicySource: IPolicySource
    {
        public string PolicyString {get; set;} = "";

        public string GetPolicyFromSource()
        {
            return PolicyString;
        }
    }
}