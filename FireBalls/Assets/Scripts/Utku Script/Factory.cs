using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetFactoryPrice())
        {
            Debug.Log("Game won");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void StopInteracting()
    {

    }
}
