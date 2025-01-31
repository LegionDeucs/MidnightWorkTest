using System.Collections.Generic;
using UnityEngine;

public abstract class VisualAnimatorBaseAnimationType : MonoBehaviour
{
    [Header("Item animation")]
    [SerializeField] protected float appearDuration;
    [SerializeField] protected AnimationCurve progressionCurve;
    [SerializeField] protected AnimationCurve appearCurve;

    public float AppearDuration => appearDuration;

    public abstract void InstantAnimation();
    internal void SetAnimationDuration(float loopDuration, bool useAsRelative)
    {
        if (useAsRelative)
            appearDuration *= loopDuration;
        else 
            appearDuration = loopDuration;
    }

    //Work only for Linear Curves
    public float FindTimeFromValue(float targetValue, AnimationCurve curve)
    {
        if (curve.keys.Length == 0)
        {
            Debug.LogError("The AnimationCurve has no keys.");
            return -1f;
        }

        for (int i = 0; i < curve.length - 1; i++)
        {
            Keyframe key1 = curve.keys[i];
            Keyframe key2 = curve.keys[i + 1];

            if ((targetValue >= key1.value && targetValue <= key2.value) ||
                (targetValue <= key1.value && targetValue >= key2.value))
            {
                float t = (targetValue - key1.value) / (key2.value - key1.value);
                return Mathf.Lerp(key1.time, key2.time, t);
            }
        }

        Debug.LogWarning("Value not found in the curve.");
        return -1f; 
    }

    protected List<DebugAnimationComponent<T>> CreateAnimationData<T>(List<T> items)
    {
        List<DebugAnimationComponent<T>> debugAnimationComponents = new List<DebugAnimationComponent<T>>();

        float step = GetStep(items.Count);

        for (int i = 0; i < items.Count; i++)
        {
            float normalizedExecuteTime = i * step;// progressionCurve.Evaluate();
            debugAnimationComponents.Add(new DebugAnimationComponent<T>() { animationDebugComponent = items[i], normalizedAppearTime = FindTimeFromValue(normalizedExecuteTime, progressionCurve) });
        }
        return debugAnimationComponents;
    }

    private float GetStep(float itemCount) => 1 / itemCount;

    public abstract void PlayAnimationFrame(float debugProgression, float callAnimationDuration, float longestAppearAnimationDuration);

    public abstract void ResetAnimation();
    public abstract void ForceHide();

    public abstract void InitAnimation();

    
}

public class DebugAnimationComponent<T>
{
    public T animationDebugComponent;
    public float normalizedAppearTime;
}