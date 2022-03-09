using DG.Tweening.Plugins.Options;

public struct IncrementalTweenerOptions<T> : IPlugOptions
{
    internal IncrementalTweenerState<T> State { get; set; }

    public void Reset()
    {
    }
}
