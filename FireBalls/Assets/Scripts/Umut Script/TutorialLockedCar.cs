using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialLockedCar : MonoBehaviour, IInteractable
{
    private Car carScript;
    //[SerializeField] private TMP_Text Txt;
    [SerializeField] private TMP_Text arabaAcmaTxt;
    [SerializeField] private TMP_Text fabrikaAcmaTxt;

    private void Awake()
    {
        //carScript = GetComponent<Car>();
    }

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetCarPrice())
        {
            //carScript.enabled = true;
            
            Debug.Log("Car unlocked");
            //Txt.enabled = true;

            arabaAcmaTxt.gameObject.SetActive(false);
            fabrikaAcmaTxt.gameObject.SetActive(true);

            ResourceManager.Instance.DecreaseMoney(ResourceManager.Instance.GetCarPrice());
            ResourceManager.Instance.IncreaseMoney(ResourceManager.Instance.GetFactoryPrice());

            Destroy(this);
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
