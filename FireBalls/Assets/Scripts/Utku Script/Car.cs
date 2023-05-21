using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class Car : MonoBehaviour, IInteractable
{
    [SerializeField] private Rigidbody rbSphere;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float reverseSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float gravity;
    [SerializeField] private Transform getOffPoint;

    private bool isPlayerInCar;
    private float verticalInput;
    private float horizontalInput;

    private void Start()
    {
        rbSphere.transform.parent = null;
        BoxCollider colliderCar = GetComponent<BoxCollider>();
        SphereCollider colliderMotorSphere = rbSphere.gameObject.GetComponent<SphereCollider>();
        Physics.IgnoreCollision(colliderMotorSphere, colliderCar);
    }

    private void Update()
    {

        if (isPlayerInCar)
        {
            //verticalInput = PlayerInput.Instance.GetInputVector().z;
            //horizontalInput = PlayerInput.Instance.GetInputVector().x;

            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");

            verticalInput *= verticalInput > 0 ? forwardSpeed : reverseSpeed;
            verticalInput *= Time.deltaTime;

            float newRotation = horizontalInput * turnSpeed * Time.deltaTime * MathF.Abs(rbSphere.velocity.magnitude);

            if (verticalInput < 0)
            {
                newRotation *= -1;
            }

            transform.Rotate(0, newRotation, 0);
        }

        transform.position = rbSphere.transform.position;
        
    }

    private void LateUpdate()
    {
        if (isPlayerInCar)
        {
            Player.Instance.transform.position = transform.position;
            Player.Instance.transform.rotation = transform.rotation;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerInCar)
        {
            Vector3 force = transform.forward * verticalInput;
            force *= Time.deltaTime;
            rbSphere.AddForce(force, ForceMode.Acceleration);
            rbSphere.AddForce(Vector3.down * (gravity * Time.deltaTime));
        }
    }

    public void Interact()
    {
        isPlayerInCar = !isPlayerInCar;

        if (isPlayerInCar)
        {
            GetPlayerInVehicle();
        }
        else
        {
            GetPlayerOutOfVehicle();
        }
    }

    private void GetPlayerInVehicle()
    {
        Player.Instance.GetInCar();
    }

    private void GetPlayerOutOfVehicle()
    {
        Player.Instance.transform.position = getOffPoint.position;
        Player.Instance.GetOutOfCar();
    }


    public void StopInteracting()
    {
        
    }

    public void ShowInteract()
    {
        
    }

    public void HideInteract()
    {
        
    }
}
