using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public static partial class Extensions
{
    public static IEnumerator DoFade(this CanvasGroup canvasGroup, float duration, float to, AnimationCurve curve, float from = -1)
    {
        if (from >= 0)
            canvasGroup.alpha = from;

        float t = 0;
        while (t < duration)
        {
            yield return null;
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(from, to, curve.Evaluate(Mathf.InverseLerp(0, duration, t)));
        }
    }
}
