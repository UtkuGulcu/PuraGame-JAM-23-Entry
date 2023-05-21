using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType
    {
        Money
    }

    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceType, int> Resources = new Dictionary<ResourceType, int>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Resources[ResourceType.Money] = 0;
    }

    public int GetMoney()
    {
        return Resources[ResourceType.Money];
    }

    public void IncreaseMoney(int amount)
    {
        Resources[ResourceType.Money] += amount;
    }

    public void DecreaseMoney(int amount)
    {
        Resources[ResourceType.Money] -= amount;
    }
}