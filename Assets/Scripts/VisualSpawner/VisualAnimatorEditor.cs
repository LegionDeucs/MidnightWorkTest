using UnityEngine;
using System;
using System.Linq;

#if UNITY_EDITOR
public partial class VisualAnimator
{
    [Range(0, 1)]
    [SerializeField] public float animationDebugSlider;
    private Coroutine playRoutine;

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

        if (visualAnimations == null || visualAnimations.Count == 0)
            return;
        float maxAppearAnimationDuration = visualAnimations.Max(animation => animation.AppearDuration);

        foreach (var animation in visualAnimations)
        {
            animation.InitAnimation();
            animation.PlayAnimationFrame(animationDebugSlider, spawnDuration, maxAppearAnimationDuration);
        }
    }

    [ContextMenu("Reset")]
    public void ResetDebug()
    {
        animationDebugSlider = -1;

        foreach (var animation in visualAnimations)
        {
            animation.ResetAnimation();
        }
    }
}
#endif
