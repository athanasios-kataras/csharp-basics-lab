namespace Microsoft.CSharp.Basics.Solid
{
    using System.IO;

    public class Repository
    {
        public string GetPolicy()
        {
            return File.ReadAllText("policy.json");
        }
    }
}