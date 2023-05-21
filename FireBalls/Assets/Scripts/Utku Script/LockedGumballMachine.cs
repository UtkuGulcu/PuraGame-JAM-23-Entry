using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedGumballMachine : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject gumballMachinePrefab;

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetGumballMachinePrice())
        {
            Instantiate(gumballMachinePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void StopInteracting()
    {
        
    }

    public void ShowInteract()
    {
        Debug.Log("Show buy gumball machine UI");
    }

    public void HideInteract()
    {
        Debug.Log("Hide buy gumball machine UI");
    }
}
