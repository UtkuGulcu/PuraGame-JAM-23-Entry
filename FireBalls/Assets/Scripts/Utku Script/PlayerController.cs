using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [HideInInspector] public bool isMoving;
    
    [SerializeField] private float movementSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody rbPlayer;
    private Quaternion toRotation;

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

        rbPlayer = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        Vector3 inputVector = PlayerInput.Instance.GetInputVector();
        rbPlayer.velocity = new Vector3(inputVector.x * movementSpeed * Time.deltaTime, rbPlayer.velocity.y,
            inputVector.z * movementSpeed * Time.deltaTime);
    }

    private void Update()
    {
        Vector3 inputVector = PlayerInput.Instance.GetInputVector();

        isMoving = inputVector != Vector3.zero;

        if (isMoving)
        {
            toRotation = Quaternion.LookRotation(inputVector, Vector3.up);
            
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
    }
}
