using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumMachineSpawner : MonoBehaviour
{
    public GameObject gumMachinePrefab;

    public Transform[] spawnPoints;

    private void Start()
    {
        SpawnGumMachines();
    }

    private void SpawnGumMachines()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i];

            GameObject gumMachineInstance = Instantiate(gumMachinePrefab, spawnPoint.position, spawnPoint.rotation);


        }
    }



}
