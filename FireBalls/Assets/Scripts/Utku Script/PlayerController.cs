using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    private Rigidbody rbPlayer;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rbPlayer.velocity = new Vector3(PlayerInput.Instance.GetInputVectorNormalized().x * movementSpeed * Time.fixedDeltaTime, 0,
            PlayerInput.Instance.GetInputVectorNormalized().z * movementSpeed * Time.fixedDeltaTime);

    }
}
