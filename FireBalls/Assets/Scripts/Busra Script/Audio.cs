using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance {get ; private set;}

    private AudioSource footstepSound;
    private AudioSource carSound;
    private AudioSource scoreSound;
    private void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        carSound = GetComponent<AudioSource>();
        scoreSound = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }
}
