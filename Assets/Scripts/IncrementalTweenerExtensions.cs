using DG.Tweening;
using DG.Tweening.Core;

public static class IncrementalTweenerExtensions
{
    public static TweenerCore<float, float, IncrementalTweenerOptions<float>> IncrementallyTweenCopyTo(
        this float startValue,
        float endValue,
        float duration)
    {
        var state = new IncrementalTweenerState<float>((x, y) => x - y, startValue);
        var plugin = new FloatNoOptionsIncrementalTweenerPlugin(state);
        return DOTween.To(
            plugin,
            () => state.CurrentValue,
            value => state.CurrentValue = value,
            endValue,
            duration);
    }

    public static TweenerCore<T, T, IncrementalTweenerOptions<T>> OnUpdate<T>(
        this TweenerCore<T, T, IncrementalTweenerOptions<T>> tweener,
        TweenCallback<IncrementalTweenerState<T>> handler)
    {
        return tweener.OnUpdate(() =>
        {
            handler(tweener.plugOptions.State);
        });
    }
}
