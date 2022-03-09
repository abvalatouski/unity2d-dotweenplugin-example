using System;

public class IncrementalTweenerState<T>
{
    private readonly Func<T, T, T> subtractor;
    private T previousValue;
    private T currentValue;
    private T delta;

    public IncrementalTweenerState(Func<T, T, T> subtractor, T startValue)
    {
        this.subtractor = subtractor;
        previousValue = startValue;
        currentValue = startValue;
        delta = subtractor(currentValue, previousValue);
    }

    public T PreviousValue
    {
        get => previousValue;
    }

    public T CurrentValue
    {
        get => currentValue;
        internal set
        {
            previousValue = currentValue;
            currentValue = value;
            delta = subtractor(currentValue, previousValue);
        }
    }

    public T Delta
    {
        get => delta;
    }
}
