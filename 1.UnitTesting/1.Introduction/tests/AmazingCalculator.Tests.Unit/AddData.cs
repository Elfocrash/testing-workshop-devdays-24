using System.Collections;

namespace AmazingCalculator.Tests.Unit;

public class AddData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return [1, 2, 3];
        yield return [5, -5, 0];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
