using System.IO;

namespace Microsoft.CSharp.Basics.Solid.Code
{
    public class FileLoader
    {
        public string LoadFromFile(string fileName)
        {
            return  File.ReadAllText(fileName);
        }
    }
}