using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Factory : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        if (ResourceManager.Instance.GetMoney() >= ResourceManager.Instance.GetFactoryPrice())
        {
            Debug.Log("Game won");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
        Debug.Log("Show buy factory UI");
    }

    public void HideInteract()
    {
        Debug.Log("Hide buy factory UI");
    }
}
