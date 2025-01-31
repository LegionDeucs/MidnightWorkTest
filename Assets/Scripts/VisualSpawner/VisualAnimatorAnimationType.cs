using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class VisualAnimatorAnimationType<TAnimatedComponent, TAnimationData> : VisualAnimatorBaseAnimationType where TAnimatedComponent : Component where TAnimationData : DefaultAnimationData<TAnimatedComponent>
{
    //[SerializeField] protected List<TAnimatedComponent> animatedParts;
    [SerializeField] protected List<TAnimationData> animationDatas;

    [Header("Tool")]
    [SerializeField] private Transform aimPoint;

    [Space]
    [SerializeField] private Transform parent;

    [TagField]
    [SerializeField] private string visualTag = "Visual";

    private List<DebugAnimationComponent<TAnimationData>> frameAnimatedComponentsData;

#if UNITY_EDITOR
    public TAnimatedComponent indexTarget;
    public int index;

    private void OnValidate()
    {
        if(animationDatas != null)
            index = animationDatas.Select(data => data.animatedComponent).ToList().IndexOf(indexTarget);
    }

    private void Fill(ref List<TAnimationData> fillParts)
    {
        fillParts = new List<TAnimationData>();

        List<Transform> transforms = parent.GetAllChildrenByTag(visualTag).ToHashSet().ToList();

        transforms.Sort((tr1, tr2) => (tr1.position - aimPoint.position).magnitude.CompareTo((tr2.position - aimPoint.position).magnitude));

        List<TAnimatedComponent> animatedComponents = transforms.Where(child => child.TryGetComponent<TAnimatedComponent>(out _)).Select(child => child.GetComponent<TAnimatedComponent>()).ToList();

        foreach (var animatedComponent in animatedComponents)
            fillParts.Add(CreateAnimatedComponentData(animatedComponent));

        UnityEditor.EditorUtility.SetDirty(this);
    }

    protected abstract TAnimationData CreateAnimatedComponentData(TAnimatedComponent animatedComponent);

    private void AddParts(ref List<TAnimationData> fillParts)
    {
        List<TAnimationData> tmp = new List<TAnimationData>();
        Fill(ref tmp);

        foreach (var part in tmp)
            if (!fillParts.Contains(part))
                fillParts.Add(part);


        UnityEditor.EditorUtility.SetDirty(this);
    }
    //Button
    public void FillShowParts()
    {
        Fill(ref animationDatas);
        ValidateShownParts();
    }

    //Button
    public void AddFillShowParts()
    {
        AddParts(ref animationDatas);
        ValidateShownParts();
    }

    //Button
    public void SortShownParts()
    {
        Sort(ref animationDatas);
    }

    //Button
    public void ValidateShownParts()
    {
        ValidateShownParts(ref animationDatas);
    }

    private void Sort(ref List<TAnimationData> sortObjects)
    {
        sortObjects.Sort((tr1, tr2) => (tr1.animatedComponent.transform.position - aimPoint.position).magnitude.CompareTo((tr2.animatedComponent.transform.position - aimPoint.position).magnitude));
    }

    private void ValidateShownParts(ref List<TAnimationData> animationDatas)
    {
        List<TAnimatedComponent> animComponents = new List<TAnimatedComponent>();
        for (int i = 0; i < animationDatas.Count; i++)
            animComponents.Add(animationDatas[i].animatedComponent);
        for (int i = 0; i < animationDatas.Count; i++)
            if (animationDatas[i].animatedComponent == null || animComponents.IndexOf(animationDatas[i].animatedComponent) !=i)
            {
                Debug.Log("Removing at " + i);
                animComponents.RemoveAt(i);
                animationDatas.RemoveAt(i--);

            }

        UnityEditor.EditorUtility.SetDirty(this);
    }
#endif

    public override void InitAnimation()
    {
        frameAnimatedComponentsData = CreateAnimationData(animationDatas);
    }
    public override void InstantAnimation()
    {
        for (int i = 0; i < animationDatas.Count; i++)
            PlayAnimationFrame(1, 0, appearDuration);
    }

    public override void ForceHide()
    {
        for (int i = 0; i < animationDatas.Count; i++)
        {
            PlayAnimationFrame(0, 0, appearDuration);
        }
    }

    public override void PlayAnimationFrame(float debugProgression, float callAnimationDuration, float longestAppearAnimationDuration)
    {
        if (frameAnimatedComponentsData == null)
        {
            Debug.LogError("Not initialized FrameData");
            return;
        }

        foreach (var animateComponent in frameAnimatedComponentsData)
            AnimateFrame(animateComponent.animationDebugComponent, 
                animateComponent.normalizedAppearTime,
                (longestAppearAnimationDuration + callAnimationDuration) * debugProgression,
                callAnimationDuration);
    }

    public override void ResetAnimation()
    {
        for (int i = 0; i < animationDatas.Count; i++)
            ResetAnimatedComponent(animationDatas[i]);
    }

    protected abstract void ResetAnimatedComponent(TAnimationData animatedComponent);

    protected abstract void AnimateFrame(TAnimationData itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime);
}

[System.Serializable]
public class DefaultAnimationData<TAnimatedComponent> where TAnimatedComponent : Component
{
    public TAnimatedComponent animatedComponent;
}