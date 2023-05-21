using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFactory : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StopInteracting()
    {
        throw new System.NotImplementedException();
    }

    public void ShowInteract()
    {
        throw new System.NotImplementedException();
    }

    public void HideInteract()
    {
        throw new System.NotImplementedException();
    }
}
