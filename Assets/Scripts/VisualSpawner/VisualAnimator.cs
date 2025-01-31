using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class VisualAnimator : MonoBehaviour
{
    [SerializeField] private List<VisualAnimatorBaseAnimationType> visualAnimations;
    [SerializeField] private float spawnDuration;

    public event System.Action OnPlayFinished;

    internal void SetAnimationDuration(float loopDuration, bool useAsRelative)
    {
        for (int i = 0; i < visualAnimations.Count; i++)
            visualAnimations[i].SetAnimationDuration(loopDuration, useAsRelative);
    }

    public void Init()
    {
        foreach (VisualAnimatorBaseAnimationType animation in visualAnimations)
            animation.InitAnimation();
    }

    internal float GetAnimationDuration() => spawnDuration + visualAnimations.Max(vAnim => vAnim.AppearDuration);

    public void ForceHide()
    {
        for (int i = 0; i < visualAnimations.Count; i++)
        {
            visualAnimations[i].ForceHide();
        }
    }

    public void ForceShow()
    {
        for (int i = 0; i < visualAnimations.Count; i++)
        {
            visualAnimations[i].InstantAnimation();
        }
    }

    public void PlayAnimation()
    {
        if (playRoutine != null)
            StopCoroutine(playRoutine);

        playRoutine = StartCoroutine(PlayAnimationRoutine());
    }
    private IEnumerator PlayAnimationRoutine()
    {
        if(visualAnimations.Count == 0)
            yield break;

        float maxAppearAnimationDuration = visualAnimations.Max(animation => animation.AppearDuration);
        float wholeAnimationDuration = maxAppearAnimationDuration + spawnDuration;
        float animationProgress = 0;

        while (animationProgress < wholeAnimationDuration)
        {
            animationProgress += Time.deltaTime;
            foreach (var animation in visualAnimations)
            {
                animation.PlayAnimationFrame(Mathf.InverseLerp(0, wholeAnimationDuration, animationProgress), spawnDuration, maxAppearAnimationDuration);
            }
            yield return null;
        }
        OnPlayFinished?.Invoke();
        playRoutine = null;
    }
}