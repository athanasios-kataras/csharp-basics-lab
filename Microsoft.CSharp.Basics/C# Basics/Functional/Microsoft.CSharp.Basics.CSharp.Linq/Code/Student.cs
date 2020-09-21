using System.Collections.Generic;

namespace Microsoft.CSharp.Basics.CSharp.Linq.Code
{
    public class Student
    {
        public string First { get; internal set; }
        public string Last { get; internal set; }
        public int ID { get; internal set; }
        public List<int> Scores { get; internal set; }
    }
}