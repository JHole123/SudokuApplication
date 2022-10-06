

namespace SudokuApplication;

public class Ref<T> where T : struct
{
    public Ref(T value)
    {
        Value = value;
    }
    public T Value { get; set; }
}