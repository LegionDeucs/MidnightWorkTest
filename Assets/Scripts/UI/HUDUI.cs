using ServiceLocations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDUI : MonoBehaviour, IService
{
    [SerializeField] private ResourceUI resourceUI;

    public ResourceUI ResourceUI => resourceUI;
}
