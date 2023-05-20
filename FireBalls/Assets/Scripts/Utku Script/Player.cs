using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private LayerMask interactionLayerMask;

    private Collider[] interactedColliders;
    [SerializeField] private GameObject visuals;

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

    private void Start()
    {
        PlayerInput.Instance.OnInteractKeyDown += PlayerInput_OnInteractKeyDown;
        PlayerInput.Instance.OnInteractKeyUp += PlayerInput_OnInteractKeyUp;
    }

    private void OnDisable()
    {
        PlayerInput.Instance.OnInteractKeyDown -= PlayerInput_OnInteractKeyDown;
        PlayerInput.Instance.OnInteractKeyUp -= PlayerInput_OnInteractKeyUp;
    }

    private void PlayerInput_OnInteractKeyDown(object sender, EventArgs e)
    {
        PlayerController.Instance.LockMovement();

        Vector3 boxSize = new Vector3(1.5f, 2f, 1.5f);

        interactedColliders = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity, interactionLayerMask);

        foreach (Collider interactedCollider in interactedColliders)
        {
            if (interactedCollider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void PlayerInput_OnInteractKeyUp(object sender, EventArgs e)
    {
        PlayerController.Instance.UnlockMovement();

        foreach (Collider interactedCollider in interactedColliders)
        {
            if (interactedCollider.TryGetComponent(out IInteractable interactable))
            {
                interactable.StopInteracting();
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 2, 3));
    }

    public void GetInCar()
    {
        PlayerController.Instance.DisableCharacterController();
        PlayerController.Instance.enabled = false;
        visuals.SetActive(false);
    }

    public void GetOutOfCar()
    {
        PlayerController.Instance.enabled = true;
        PlayerController.Instance.EnableCharacterController();
        visuals.SetActive(true);
    }
}
