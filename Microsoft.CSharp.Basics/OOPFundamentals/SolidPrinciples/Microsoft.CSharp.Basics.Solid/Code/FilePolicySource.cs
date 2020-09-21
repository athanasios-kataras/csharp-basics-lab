using System.IO;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class FilePolicySource
    {
        public string GetPolicyFromSource()
        {
            return File.ReadAllText("policy.json");
        }
    }
}