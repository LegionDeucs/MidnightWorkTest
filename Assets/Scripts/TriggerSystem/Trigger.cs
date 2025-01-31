using Unity.VisualScripting;
using UnityEngine;

public abstract class Trigger<TInteractionType> : MonoBehaviour
{
    public abstract event System.Action<TInteractionType> OnTriggerEventEnter;
    public abstract event System.Action<TInteractionType> OnTriggerEventLeave;
}
