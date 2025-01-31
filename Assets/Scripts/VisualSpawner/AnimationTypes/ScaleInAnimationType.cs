using UnityEngine;

public class ScaleInAnimationType : VisualAnimatorAnimationType<Transform, ScaleAnimationData>
{
    protected override void AnimateFrame(ScaleAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        Vector3 startScale = itemAnimationData.defaultScale;

        float animationProgression = currentTime - appearTime * normalizedAppearTime;

        float progress = Mathf.InverseLerp(0, appearDuration, animationProgression);
        itemAnimationData.animatedComponent.localScale = startScale * appearCurve.Evaluate(progress);
    }

    protected override void ResetAnimatedComponent(ScaleAnimationData animatedComponent)
    {
        animatedComponent.animatedComponent.localScale = animatedComponent.defaultScale;
    }

    protected override ScaleAnimationData CreateAnimatedComponentData(Transform item)
    {
        return new ScaleAnimationData() { animatedComponent = item, defaultScale = item.localScale };
    }
}

[System.Serializable]
public class ScaleAnimationData : DefaultAnimationData<Transform>
{
    public Vector3 defaultScale;
}