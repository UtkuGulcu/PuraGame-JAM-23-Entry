using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialLockGumballMachine : MonoBehaviour
{
    [SerializeField] private GameObject gumballMachinePrefab;
    [SerializeField] private TMP_Text makineCalistirma;
    [SerializeField] private TMP_Text arabaAcma;

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetGumballMachinePrice())
        {
            Instantiate(gumballMachinePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            makineCalistirma.gameObject.SetActive(false);
            arabaAcma.gameObject.SetActive(true);
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
