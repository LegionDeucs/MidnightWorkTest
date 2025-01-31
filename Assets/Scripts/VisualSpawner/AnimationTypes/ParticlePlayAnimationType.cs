using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/Grid/Grid cell logic/Particle/Plat Particle")]
public class ParticlePlayAnimationType : VisualAnimatorAnimationType<ParticleSystem, ParticleAnimationType>
{
    protected override void AnimateFrame(ParticleAnimationType itemAnimationData, float normalizedAppearTime, float currentTime, float appearTime)
    {
        float animationProgression = currentTime - appearTime * normalizedAppearTime;
        float progress = appearCurve.Evaluate(Mathf.InverseLerp(0, appearDuration, animationProgression));

        if (progress > 0 && !itemAnimationData.animatedComponent.isPlaying)
            itemAnimationData.animatedComponent.Play();
    }

    protected override ParticleAnimationType CreateAnimatedComponentData(ParticleSystem animatedComponent)
    {
        return new ParticleAnimationType() {animatedComponent = animatedComponent};
    }

    protected override void ResetAnimatedComponent(ParticleAnimationType animatedComponent)
    {
        
    }
}

[System.Serializable]
public class ParticleAnimationType : DefaultAnimationData<ParticleSystem>
{
    
}