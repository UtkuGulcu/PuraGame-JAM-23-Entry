using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private LayerMask interactionLayerMask;

    private Collider[] interactedColliders;

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

    private void PlayerInput_OnInteractKeyDown(object sender, EventArgs e)
    {
        Vector3 boxSize = new Vector3(2.5f, 2f, 2.5f);

        interactedColliders = Physics.OverlapBox(transform.position, boxSize, Quaternion.identity, interactionLayerMask);

        foreach (Collider interactedCollider in interactedColliders)
        {
            if (interactedCollider.TryGetComponent(out IInteractable interactable))
            {
                interactable.StartInteracting();
            }
        }
    }

    private void PlayerInput_OnInteractKeyUp(object sender, EventArgs e)
    {
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
        Gizmos.DrawWireCube(transform.position, new Vector3(5, 5, 5));
    }
}
