using System;

namespace Microsoft.CSharp.Basics.Patterns.FileTreeExtender
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteVisitorExample();
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
