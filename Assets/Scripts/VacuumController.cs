using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    [SerializeField] private ColliderTrigger interactionTrigger;
    [SerializeField] private Transform collectionPoint;
    [SerializeField] private Vector2 minMaxDistance;
    [SerializeField] private CollectingProcess minDistanceProcess;
    [SerializeField] private CollectingProcess maxDistanceProcess;

    [SerializeField] private float minDistanceForNormalScale;
    [SerializeField] private AnimationCurve scaleCurve;

    [SerializeField] private float distanceToCollect;

    private ColliderDictionary colliderDictionary;
    private ItemsEncyclopedia itemsEncyclopedia;
    private Dictionary<ICollectable, float> collectableList;


    private List<ICollectable> removalList;

    protected int currentCollectableItem;

    protected ItemConfig collectableTarget;

    public event System.Action<ICollectable> OnCollected;
    public event System.Action<ItemConfig> OnCollectableItemChanged;
    private void Awake()
    {
        collectableList = new Dictionary<ICollectable, float>();
        removalList = new List<ICollectable>();
    }

    private void Start()
    {
        colliderDictionary = ServiceLocations.ServiceLocator.Context.GetSingle<ColliderDictionary>();
        itemsEncyclopedia = ServiceLocations.ServiceLocator.Context.GetSingle<ItemsEncyclopedia>();
    }

    public void ChangeCollectableItemType()
    {
        currentCollectableItem = (int)Mathf.Repeat(currentCollectableItem + 1, itemsEncyclopedia.ItemCount);
        collectableTarget = itemsEncyclopedia.GetItemType(currentCollectableItem);
        OnCollectableItemChanged?.Invoke(collectableTarget);
    }

    public void StartCollecting()
    {
        interactionTrigger.OnTriggerEventEnter += OnInteractionTrigger_OnTriggerEventEnter;
        interactionTrigger.OnTriggerEventLeave += OnInteractionTrigger_OnTriggerEventLeave;
    }

    public void StopCollecting()
    {
        interactionTrigger.OnTriggerEventEnter -= OnInteractionTrigger_OnTriggerEventEnter;
        interactionTrigger.OnTriggerEventLeave -= OnInteractionTrigger_OnTriggerEventLeave;
    }

    private void OnInteractionTrigger_OnTriggerEventLeave(Collider obj)
    {
        var item = colliderDictionary.GetCollectable(obj);
        if (collectableList.ContainsKey(item))
        {
            item.StopCollecting();
            removalList.Add(item);
        }
    }

    private void OnInteractionTrigger_OnTriggerEventEnter(Collider obj)
    {
        var item = colliderDictionary.GetCollectable(obj);
        if (!itemsEncyclopedia.GetItemType(item.DataID.ID).Equals(collectableTarget))
            return;

        item.StartCollecting();
        collectableList.Add(item, Time.time);
    }

    public void DoFixedUpdate()
    {
        foreach (var items in collectableList)
            UpdateCollectables(items.Key, items.Value);

        for (int i = 0; i < removalList.Count; i++)
            collectableList.Remove(removalList[i]);

        removalList.Clear();
    }

    private bool UpdateCollectables(ICollectable collectable, float startCollectingTime)
    {
        if (collectable == null)
        {
            collectableList.Remove(null);
            return false;
        }

        Vector3 directionDistance = collectionPoint.position - collectable.transform.position;
        if(distanceToCollect>directionDistance.magnitude)
        {
            OnInteractionTrigger_OnTriggerEventLeave(collectable.MainCollider);
            OnCollected?.Invoke(collectable);
            return true;
        }

        float collectingTime = Time.time - startCollectingTime;

        float processProgression = Mathf.InverseLerp(minMaxDistance.x, minMaxDistance.y, directionDistance.magnitude);

        float drag = Mathf.Lerp(minDistanceProcess.GetDragFromTime(collectingTime), maxDistanceProcess.GetDragFromTime(collectingTime), processProgression);
        float forceMagnitude = Mathf.Lerp(minDistanceProcess.GetForceMagnitudeFromTime(collectingTime),
            maxDistanceProcess.GetForceMagnitudeFromTime(collectingTime), processProgression);

        Vector3 pullForce = directionDistance.normalized * forceMagnitude;


        float scale = Mathf.Lerp(1, scaleCurve.Evaluate(Mathf.InverseLerp(0, minDistanceForNormalScale, directionDistance.magnitude)),
            Mathf.Lerp(minDistanceProcess.GetTime(collectingTime), maxDistanceProcess.GetTime(collectingTime),processProgression));
        collectable.UpdateCollecting(drag, pullForce, scale);
        return false;
    }
}

[System.Serializable]
public class CollectingProcess
{
    public Vector2 minMaxTime;
    public Vector2 minMaxForceApplied;
    public Vector2 minMaxDrag;

    public float GetDragFromTime(float time) => Mathf.Lerp(minMaxDrag.x, minMaxDrag.y, GetTime(time));
    public float GetForceMagnitudeFromTime(float time) => Mathf.Lerp(minMaxForceApplied.x, minMaxForceApplied.y, GetTime(time));
    public float GetTime(float time) => Mathf.InverseLerp(minMaxTime.x, minMaxTime.y, time);
}