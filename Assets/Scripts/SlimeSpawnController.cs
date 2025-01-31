using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawnController : MonoBehaviour
{
    [SerializeField] private ItemProducer slimeItemProducer;

    private void Start()
    {
        slimeItemProducer.StartProducingItems();
    }
}
