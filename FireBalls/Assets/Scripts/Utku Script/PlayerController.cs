using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [HideInInspector] public bool isMoving;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float gravity;

    private Quaternion toRotation;
    private CharacterController CharacterController;
    private bool canMove;

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

        CharacterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        canMove = true;
    }


    private void Update()
    {
        if (!canMove)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        Vector3 inputVector = PlayerInput.Instance.GetInputVector();
        isMoving = inputVector != Vector3.zero;

        inputVector.y -= gravity * Time.deltaTime;

        CharacterController.Move(inputVector * (Time.deltaTime * movementSpeed));
    }

    private void HandleRotation()
    {
        if (isMoving)
        {
            Vector3 inputVector = PlayerInput.Instance.GetInputVector();
            toRotation = Quaternion.LookRotation(inputVector, Vector3.up);

        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }

    public void LockMovement()
    {
        canMove = false;
        CharacterController.Move(Vector3.zero);
        isMoving = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void DisableCharacterController()
    {
        CharacterController.detectCollisions = false;
        CharacterController.enabled = false;
    }

    public void EnableCharacterController()
    {
        CharacterController.detectCollisions = true;
        CharacterController.enabled = true;
    }
}
