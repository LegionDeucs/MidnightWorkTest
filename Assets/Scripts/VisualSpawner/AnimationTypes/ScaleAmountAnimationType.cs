using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAmountAnimationType : VisualAnimatorAnimationType<Transform, ScaleAnimationData>
{
    [SerializeField] private bool isMultiplicative;
    [SerializeField] private Vector3 amount;

    protected override void AnimateFrame(ScaleAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        float animationProgression = currentTime - appearTime * normalizedAppearTime;
        float progress = Mathf.InverseLerp(0, appearDuration, animationProgression);
        Vector3 targetScale = isMultiplicative ? itemAnimationData.defaultScale.Multiply(amount) : itemAnimationData.defaultScale + amount;
        Vector3 scale = Vector3.Lerp(itemAnimationData.defaultScale, targetScale, appearCurve.Evaluate(progress));

        itemAnimationData.animatedComponent.localScale = scale;
    }

    protected override ScaleAnimationData CreateAnimatedComponentData(Transform animatedComponent)
    {
        return new ScaleAnimationData() { animatedComponent = animatedComponent, defaultScale = animatedComponent.localScale };
    }

    protected override void ResetAnimatedComponent(ScaleAnimationData animatedComponent)
    {
        animatedComponent.animatedComponent.localScale = animatedComponent.defaultScale;
    }
}