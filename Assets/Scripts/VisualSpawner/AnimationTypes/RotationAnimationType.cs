using UnityEngine;

public class RotationAnimationType : VisualAnimatorAnimationType<Transform, RotationAnimationData>
{
    [SerializeField] private bool isInLocalCoordinates;

    [SerializeField] private Quaternion targetRotation;

    protected override RotationAnimationData CreateAnimatedComponentData(Transform animatedComponent)
    {
        return new RotationAnimationData() { animatedComponent = animatedComponent, localRotation = animatedComponent.localRotation, rotation = animatedComponent.rotation };
    }

    protected override void AnimateFrame(RotationAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        Quaternion defaultRotation = (isInLocalCoordinates ? itemAnimationData.localRotation : itemAnimationData.rotation);

        Quaternion animatedRotation = defaultRotation * targetRotation;

        float animationProgression = currentTime - appearTime * normalizedAppearTime;
        float progress = Mathf.InverseLerp(0, appearDuration, animationProgression);

        if(isInLocalCoordinates)
            itemAnimationData.animatedComponent.localRotation = Quaternion.Lerp(defaultRotation, animatedRotation, appearCurve.Evaluate(progress));
        else
            itemAnimationData.animatedComponent.rotation = Quaternion.Lerp(defaultRotation, animatedRotation, appearCurve.Evaluate(progress));
    }

    protected override void ResetAnimatedComponent(RotationAnimationData animatedComponent)
    {
        if(isInLocalCoordinates)
            animatedComponent.animatedComponent.localRotation = animatedComponent.localRotation;
        else
            animatedComponent.animatedComponent.rotation = animatedComponent.rotation;
    }
}

[System.Serializable]
public class RotationAnimationData : DefaultAnimationData<Transform>
{
    public Quaternion localRotation;
    public Quaternion rotation;
}