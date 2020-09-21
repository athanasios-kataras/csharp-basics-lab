using System.Collections.Generic;

namespace Microsoft.CSharp.Basics.CSharp.Linq.Code
{
    public class StudentCollection
    {
        List<Student> students = new List<Student> { 
            new Student { First = "Svetlana", Last = "Omelchenko", ID = 111, Scores = new List<int> { 97, 72, 81, 60 } }, 
            new Student { First = "Claire", Last = "O'Donnell", ID = 112, Scores = new List<int> { 75, 84, 91, 39 } }, 
            new Student { First = "Sven", Last = "Mortensen", ID = 113, Scores = new List<int> { 99, 89, 91, 95 } }, 
            new Student { First = "Cesar", Last = "Garcia", ID = 114, Scores = new List<int> { 72, 81, 65, 84 } }, 
            new Student { First = "Debra", Last = "Garcia", ID = 115, Scores = new List<int> { 97, 89, 85, 82 } } };
    }
}