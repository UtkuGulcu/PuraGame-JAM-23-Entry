using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int carPrice;
    [SerializeField] private int factoryPrice;
    [SerializeField] private int gumballMachine;

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
        GameManager.Instance.UpdateMoney();
        
    }

    public void DecreaseMoney(int amount)
    {
        Resources[ResourceType.Money] -= amount;
        GameManager.Instance.UpdateMoney();
    }

    public int GetCarPrice()
    {
        return carPrice;
    }

    public int GetFactoryPrice()
    {
        return factoryPrice;
    }

    public int GetGumballMachinePrice()
    {
        return gumballMachine;
    }
}
