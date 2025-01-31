using UnityEngine;

public class LinearMoveInAnimationType : VisualAnimatorAnimationType<Transform, MoveInAnimationData>
{
    [SerializeField] private bool isInLocalCoordinates;
    [SerializeField] private Vector3 moveFromOffset;

    protected override MoveInAnimationData CreateAnimatedComponentData(Transform animatedComponent)
    {
        return new MoveInAnimationData() { animatedComponent = animatedComponent, localPosition = animatedComponent.localPosition, worldPosition = animatedComponent.position };
    }

    protected override void AnimateFrame(MoveInAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        Vector3 startPosition = (isInLocalCoordinates ? itemAnimationData.localPosition : itemAnimationData.worldPosition) + moveFromOffset;

        float animationProgression = currentTime - appearTime * normalizedAppearTime;

        float progress = Mathf.InverseLerp(0, appearDuration, animationProgression);

        if(isInLocalCoordinates)
            itemAnimationData.animatedComponent.localPosition = Vector3.Lerp(startPosition, itemAnimationData.localPosition, appearCurve.Evaluate(progress));
        else
            itemAnimationData.animatedComponent.position = Vector3.Lerp(startPosition, itemAnimationData.worldPosition, appearCurve.Evaluate(progress));
    }

    protected override void ResetAnimatedComponent(MoveInAnimationData animatedComponent)
    {
        if (isInLocalCoordinates)
            animatedComponent.animatedComponent.localPosition = animatedComponent.localPosition;
        else
            animatedComponent.animatedComponent.position = animatedComponent.worldPosition;
    }
}

[System.Serializable]
public class MoveInAnimationData : DefaultAnimationData<Transform>
{
    public Vector3 worldPosition;
    public Vector3 localPosition;
}