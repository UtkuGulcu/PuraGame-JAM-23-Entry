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

            inputVector = new Vector3(horizontalInput, 0, verticalInput);
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
    }

    public Vector3 GetInputVector()
    {
        return inputVector.normalized;
    }
}
