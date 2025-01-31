using UnityEngine;

public class RendererFadeAnimationType : VisualAnimatorAnimationType<Renderer, RendererAnimationData>
{
    protected override RendererAnimationData CreateAnimatedComponentData(Renderer animatedComponent)
    {
        return new RendererAnimationData() { animatedComponent = animatedComponent, defaultColor = animatedComponent.material.color };
    }

    protected override void AnimateFrame(RendererAnimationData item, float appearTime, float currentTime, float callAnimationTime)
    {
        Color hideColor = item.animatedComponent.material.color.WithAlpha(0);
        float animationProgression = currentTime - appearTime;
        float progress = Mathf.InverseLerp(0, appearDuration, animationProgression);

        Color animateStepColor = Color.Lerp(item.defaultColor, hideColor, appearCurve.Evaluate(progress));
        item.animatedComponent.material.color = animateStepColor;
    }

    protected override void ResetAnimatedComponent(RendererAnimationData animatedComponent)
    {
        animatedComponent.animatedComponent.material.color = animatedComponent.defaultColor;
    }
}

[System.Serializable]
public class RendererAnimationData :DefaultAnimationData<Renderer>
{
    public Color defaultColor;
}