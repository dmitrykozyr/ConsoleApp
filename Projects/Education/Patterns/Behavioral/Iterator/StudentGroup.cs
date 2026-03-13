using System.Collections;

namespace Education.Patterns.Behavioral.Iterator;

public class StudentGroup : IEnumerable<string>
{
    private List<string> _students = new()
   {
    "Алексей",
    "Мария",
    "Иван"
   };

    public IEnumerator<string> GetEnumerator()
    {
        foreach (var student in _students)
        {
            yield return student;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
