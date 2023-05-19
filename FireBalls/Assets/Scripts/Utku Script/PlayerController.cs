using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [HideInInspector] public bool isMoving;
    
    [SerializeField] private float movementSpeed;

    private Rigidbody rbPlayer;

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
        isMoving = PlayerInput.Instance.GetInputVector() != Vector3.zero;
    }
}
