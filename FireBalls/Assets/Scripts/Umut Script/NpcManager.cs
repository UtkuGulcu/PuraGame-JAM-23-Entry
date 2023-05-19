using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcManager : MonoBehaviour
{
    public static NpcManager Instance { get; private set; }
    [SerializeField] private Transform[] waypointarray;

    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;
        }

        else
        {
            Destroy(this);
        }
    }
    public Vector3 GetShortestWayPoint(Vector3 npcPosition)
    {
        Transform shortestwaypoint = waypointarray[0];
        foreach (Transform waypoint in waypointarray)
        {
            if (Vector3.Distance(npcPosition,waypoint.position)<Vector3.Distance(npcPosition,shortestwaypoint.position))
            {
                shortestwaypoint = waypoint;

            }
        }


        return shortestwaypoint.position;
    }

    public Vector3 GetRandomWayPoint(Vector3 npcPosition)
    {
        int randomIndex = Random.Range(0, waypointarray.Length-1);

        while (waypointarray[randomIndex].position == npcPosition)
        {
            randomIndex = Random.Range(0, waypointarray.Length - 1);
        }
        return waypointarray[randomIndex].position;
        

    }

}
