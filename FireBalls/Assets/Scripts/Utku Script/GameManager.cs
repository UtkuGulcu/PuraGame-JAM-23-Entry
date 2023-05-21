using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 144;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    private void Update()
    {
        
    }
}
