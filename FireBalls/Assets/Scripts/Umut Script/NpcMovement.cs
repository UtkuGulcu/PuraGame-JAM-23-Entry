using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public Transform[] waypoints; // Kaldýrýmýn yol noktalarý
    public float speed = 5f; // Karakterin hareket hýzý

    private int currentWaypointIndex = 0; // Þu anki hedef nokta index'i

    private void Start()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        // Hedef noktaya doðru hareket et
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        // Eðer karakter hedef noktaya ulaþtýysa bir sonraki hedef noktaya geç
        if (transform.position == targetPosition)
        {
            currentWaypointIndex++;

            // Eðer tüm hedef noktalarý tamamlandýysa hareketi durdur
            if (currentWaypointIndex >= waypoints.Length)
            {
                Debug.Log("Hedefe ulaþýldý!");
                return;
            }
        }

        // Kaldýrýmýn yüzeyinde kalmasýný saðlamak için y düzlemi sabit tutulur
        Vector3 newPosition = new Vector3(transform.position.x, waypoints[currentWaypointIndex].position.y, transform.position.z);
        transform.position = newPosition;

        // Hedef noktaya ulaþýlmadýysa döngüyü devam ettir
        Invoke("MoveToWaypoint", 0.1f);
    }
}
