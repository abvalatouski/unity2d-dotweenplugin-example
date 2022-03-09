using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

// See:
// https://github.com/Demigiant/dotween/blob/develop/_DOTween.Assembly/DOTween/Plugins/FloatPlugin.cs
public class FloatNoOptionsIncrementalTweenerPlugin
    : ABSTweenPlugin<float, float, IncrementalTweenerOptions<float>>
{
    private readonly FloatPlugin mockPlugin;
    private readonly FloatOptions mockOptions;
    private readonly IncrementalTweenerState<float> state;

    public FloatNoOptionsIncrementalTweenerPlugin(IncrementalTweenerState<float> state)
    {
        mockPlugin = new FloatPlugin();
        mockOptions = new FloatOptions();
        this.state = state;
    }

    public override float ConvertToStartValue(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t,
        float value)
    {
        InitializeStateIfNeeded(t);
        return value;
    }

    public override void EvaluateAndApply(
        IncrementalTweenerOptions<float> options,
        Tween t,
        bool isRelative,
        DOGetter<float> getter,
        DOSetter<float> setter,
        float elapsed,
        float startValue,
        float changeValue,
        float duration,
        bool usingInversePosition,
        UpdateNotice updateNotice)
    {
        mockPlugin.EvaluateAndApply(
            mockOptions,
            t,
            isRelative,
            getter,
            setter,
            elapsed,
            startValue,
            changeValue,
            duration,
            usingInversePosition,
            updateNotice);
    }

    public override float GetSpeedBasedDuration(
        IncrementalTweenerOptions<float> options,
        float unitsXSecond,
        float changeValue)
    {
        return Mathf.Abs(changeValue / unitsXSecond);
    }

    public override void Reset(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t)
    {
    }

    public override void SetChangeValue(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t)
    {
        t.changeValue = t.endValue - t.startValue;
    }

    public override void SetFrom(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t,
        bool isRelative)
    {
        float previousEndValue = t.endValue;
        t.endValue = t.getter();
        t.startValue = isRelative ? t.endValue + previousEndValue : previousEndValue;
        t.setter(t.startValue);
    }

    public override void SetFrom(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t,
        float fromValue,
        bool setImmediately,
        bool isRelative)
    {
        if (isRelative)
        {
            float currentValue = t.getter();
            t.endValue += currentValue;
            fromValue += currentValue;
        }

        t.startValue = fromValue;
        if (setImmediately)
        {
            t.setter(fromValue);
        }
    }

    public override void SetRelativeEndValue(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t)
    {
        t.endValue += t.startValue;
    }

    private void InitializeStateIfNeeded(
        TweenerCore<float, float, IncrementalTweenerOptions<float>> t)
    {
        t.plugOptions.State ??= state;
    }
}
