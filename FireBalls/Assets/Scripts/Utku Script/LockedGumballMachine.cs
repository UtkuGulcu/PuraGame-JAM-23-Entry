using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedGumballMachine : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetGumballMachinePrice())
        {
            
        }
    }

    public void StopInteracting()
    {
        
    }
}
