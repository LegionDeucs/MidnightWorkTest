using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFromAnimationType : VisualAnimatorAnimationType<Transform, EulerRotationAnimationData>
{
    [SerializeField] private bool isInLocalCoordinates;

    [SerializeField] private Vector3 startRotation;

    protected override void AnimateFrame(EulerRotationAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        Vector3 targetRotation = (isInLocalCoordinates ? itemAnimationData.localEuler : itemAnimationData.euler); ;
        Vector3 animatedRotation = startRotation.Add(targetRotation);

        float animationProgression = currentTime - appearTime * normalizedAppearTime;
        float progress = Mathf.InverseLerp(0, appearDuration, animationProgression);

        if (isInLocalCoordinates)
            itemAnimationData.animatedComponent.localEulerAngles = Vector3.Lerp(animatedRotation, targetRotation, appearCurve.Evaluate(progress));
        else
            itemAnimationData.animatedComponent.eulerAngles = Vector3.Lerp(animatedRotation, targetRotation, appearCurve.Evaluate(progress));
    }

    protected override EulerRotationAnimationData CreateAnimatedComponentData(Transform animatedComponent)
    {
        return new EulerRotationAnimationData() { animatedComponent = animatedComponent, localEuler = animatedComponent.localEulerAngles, euler = animatedComponent.eulerAngles };
    }

    protected override void ResetAnimatedComponent(EulerRotationAnimationData animatedComponent)
    {
        if (isInLocalCoordinates)
            animatedComponent.animatedComponent.localEulerAngles = animatedComponent.localEuler;
        else
            animatedComponent.animatedComponent.eulerAngles = animatedComponent.euler;
    }
}

[System.Serializable]
public class EulerRotationAnimationData : DefaultAnimationData<Transform>
{
    public Vector3 localEuler;
    public Vector3 euler;
}