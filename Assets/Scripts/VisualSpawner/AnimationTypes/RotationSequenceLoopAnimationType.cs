using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSequenceLoopAnimationType : VisualAnimatorAnimationType<Transform, RotationAnimationData>
{
    [SerializeField] private bool isInLocalCoordinates;

    [SerializeField] private List<Quaternion> targetRotation;

    protected override void AnimateFrame(RotationAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        float animationProgression = currentTime - appearTime * normalizedAppearTime;
        float progress = appearCurve.Evaluate(Mathf.InverseLerp(0, appearDuration, animationProgression));


        if (isInLocalCoordinates)
            itemAnimationData.animatedComponent.localRotation = VisualAnimations.GetRotationAnimationFrame(progress, itemAnimationData, true, targetRotation);
        else
            itemAnimationData.animatedComponent.rotation = VisualAnimations.GetRotationAnimationFrame(progress, itemAnimationData, false, targetRotation);
    }

    

    protected override RotationAnimationData CreateAnimatedComponentData(Transform animatedComponent)
    {
        return new RotationAnimationData() { animatedComponent = animatedComponent, localRotation = animatedComponent.localRotation, rotation = animatedComponent.rotation };
    }

    protected override void ResetAnimatedComponent(RotationAnimationData animatedComponent)
    {
        if (isInLocalCoordinates)
            animatedComponent.animatedComponent.localRotation = animatedComponent.localRotation;
        else
            animatedComponent.animatedComponent.rotation = animatedComponent.rotation;
    }
}
