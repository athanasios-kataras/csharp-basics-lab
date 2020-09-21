using Microsoft.Workshop.Net.OOPFundamentals.GoF___Structural;

namespace Microsoft.CSharp.Basics.Patterns.FileTreeExtender
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteGoFCompositeExample();
        }
        #region GoF - Composite
        public static void ExecuteGoFCompositeExample()
        {
            var client = new FileSystemClient();
            client.ShowStructure();
        }
        #endregion
    }

}
