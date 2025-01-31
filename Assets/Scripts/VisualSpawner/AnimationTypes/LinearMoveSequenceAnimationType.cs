using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearMoveSequenceAnimationType : VisualAnimatorAnimationType<Transform, MoveInAnimationData>
{
    [SerializeField] private bool isInLocalCoordinates;

    [SerializeField] private List<Vector3> targetPositions;

    protected override void AnimateFrame(MoveInAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        float animationProgression = currentTime - appearTime * normalizedAppearTime;
        float progress = appearCurve.Evaluate(Mathf.InverseLerp(0, appearDuration, animationProgression));


        if (isInLocalCoordinates)
            itemAnimationData.animatedComponent.localPosition = VisualAnimations.GetPositionAnimationFrame(progress, itemAnimationData.localPosition, targetPositions);
        else
            itemAnimationData.animatedComponent.position = VisualAnimations.GetPositionAnimationFrame(progress, itemAnimationData.worldPosition, targetPositions);
    }


    protected override MoveInAnimationData CreateAnimatedComponentData(Transform animatedComponent)
    {
        return new MoveInAnimationData() { animatedComponent = animatedComponent, localPosition = animatedComponent.localPosition, worldPosition = animatedComponent.position};
    }

    protected override void ResetAnimatedComponent(MoveInAnimationData animatedComponent)
    {
        if (isInLocalCoordinates)
            animatedComponent.animatedComponent.localPosition = animatedComponent.localPosition;
        else
            animatedComponent.animatedComponent.position = animatedComponent.worldPosition;
    }
}
