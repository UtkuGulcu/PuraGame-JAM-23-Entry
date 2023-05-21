using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCar : MonoBehaviour, IInteractable
{
    private Car carScript;

    private void Awake()
    {
        carScript = GetComponent<Car>();
    }

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetCarPrice())
        {
            carScript.enabled = true;
            this.enabled = false;
        }
        else
        {
            Debug.Log($"Not enough money. Your money is: {ResourceManager.Instance.GetMoney()}");
        }
    }

    public void StopInteracting()
    {

    }
}
