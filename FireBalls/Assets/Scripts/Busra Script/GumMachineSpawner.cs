using System.Collections.Generic;
using UnityEngine;

public class GumMachineSpawner : MonoBehaviour
{
    public GameObject gumMachinePrefab; 
    public Transform character; 

    public Transform[] spawnPoints; 

    public float baslangicDelay = 0f;
    public float DelayMin = 12f; 
    public float DelayMax = 15f;
    public int additionalSpawnCount = 5; 
    public int totalSpawnCount = 5;
    public int numClosestSpawnPoints = 5;

    private void Start()
    {
        Invoke("SpawnGumMachines", baslangicDelay);
    }

    private void SpawnGumMachines()
    {
        Transform[] enYakinSpawnNoktalari = EnYakinSpawnPoint();

        for (int i = 0; i < totalSpawnCount; i++)
        {
            Transform spawnPoint = enYakinSpawnNoktalari[i % enYakinSpawnNoktalari.Length];
            GameObject gumMachineInstance = Instantiate(gumMachinePrefab, spawnPoint.position, spawnPoint.rotation);
        }
        totalSpawnCount = additionalSpawnCount;

        float nextSpawnDelay = Random.Range(DelayMin, DelayMax);
        Invoke("SpawnGumMachines", nextSpawnDelay);
    }

  

    private Transform[] EnYakinSpawnPoint()
    {
        SortedList<float, Transform> sortedSpawnPoints = new SortedList<float, Transform>();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            float distance = Vector3.Distance(character.position, spawnPoints[i].position);
            sortedSpawnPoints.Add(distance, spawnPoints[i]);
        }

        Transform[] enYakinSpawnPoints = new Transform[numClosestSpawnPoints];
        for (int i = 0; i < numClosestSpawnPoints; i++)
        {
            enYakinSpawnPoints[i] = sortedSpawnPoints.Values[i];
        }

        return enYakinSpawnPoints;
    }

   }

