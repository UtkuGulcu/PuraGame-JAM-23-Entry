using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio Instance {get ; private set;}

    [SerializeField] private AudioSource gameLoop;
    [SerializeField] private AudioSource coin;
    [SerializeField] private AudioSource gumballMachineBuild;

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

    //public void PlayGameLoop()
    //{
    //    gameLoop.Play();
    //}

    public void PlayCoin()
    {
        coin.Play();
    }

    public void PlayGumballMachineBuild()
    {
        gumballMachineBuild.Play();
    }

}
