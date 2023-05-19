using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] humanPrefabs;
    public int MaksSpawn = 10;
    public int DeathPoint = 10;
    private List<GameObject> objects = new List<GameObject>();


    void Start()
    {
        for (int i = 0; i < MaksSpawn; i++)
        {
            int randEnemy = Random.Range(0, humanPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);


            GameObject obj = Instantiate(humanPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            objects.Add(obj);

        }

    }

    // Update is called once per frame
    void Update()
    {
        // Olu?turulan tüm objeleri kontrol et
        for (int i = objects.Count - 1; i >= 0; i--)
        {
            GameObject obj = objects[i];
            if (obj == null)
            {
                // E?er obje yok olduysa, deathpoint de?erini art?r ve listeden ç?kar
                DeathPoint++;
                objects.RemoveAt(i);
            }
        }




        if (DeathPoint > 0)
        {
            int randEnemy = Random.Range(0, humanPrefabs.Length);
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);

            GameObject obj = Instantiate(humanPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            objects.Add(obj);
            DeathPoint--;
        }
    }
}
