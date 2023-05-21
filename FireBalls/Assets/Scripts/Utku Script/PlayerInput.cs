using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get; private set; }

    public event EventHandler OnInteractKeyDown;
    public event EventHandler OnInteractKeyUp;

    private Vector3 inputVector;
    private bool movementLocked;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (!movementLocked)
        {
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");

            inputVector = Camera.main.transform.right * horizontalInput + Camera.main.transform.forward * verticalInput;
            inputVector.y = 0;
        }
        else
        {
            inputVector = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            movementLocked = true;
            OnInteractKeyDown?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            movementLocked = false;
            OnInteractKeyUp?.Invoke(this, EventArgs.Empty);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ResourceManager.Instance.IncreaseMoney(5000);
        }
    }

    public Vector3 GetInputVector()
    {
        return inputVector.normalized;
    }

    public void LockMovement()
    {
        movementLocked = true;
    }

    public void UnlockMovement()
    {
        movementLocked = false;
    }
}
