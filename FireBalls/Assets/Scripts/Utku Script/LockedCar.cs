using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedCar : MonoBehaviour, IInteractable
{
    private Car carScript;
    [SerializeField] private TMP_Text arabaAcmaTxt;
    [SerializeField] private TMP_Text fabrikaAcmaTxt;

    private void Awake()
    {
        carScript = GetComponent<Car>();
    }

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetCarPrice())
        {
            carScript.enabled = true;
            Destroy(this);
            Debug.Log("Car unlocked");
            arabaAcmaTxt.gameObject.SetActive(false);
            fabrikaAcmaTxt.gameObject.SetActive(true);
            ResourceManager.Instance.IncreaseMoney(10000);
        }
        else
        {
            Debug.Log($"Not enough money. Your money is: {ResourceManager.Instance.GetMoney()}");
        }
    }

    

    public void ShowInteract()
    {
        Debug.Log("Show buy car UI");
    }

    public void HideInteract()
    {
        Debug.Log("Hide buy car UI");
    }


    public void StopInteracting()
    {

    }
}
