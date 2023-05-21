using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialLockedCar : MonoBehaviour, IInteractable
{
    private Car carScript;
    [SerializeField] private TMP_Text Txt;

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
            Txt.enabled = true;
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
