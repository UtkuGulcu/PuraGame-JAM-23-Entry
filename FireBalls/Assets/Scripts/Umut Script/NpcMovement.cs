using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMovement : MonoBehaviour
{
    public Transform[] waypoints; // Kald�r�m�n yol noktalar�
    public float speed = 5f; // Karakterin hareket h�z�

    private int currentWaypointIndex = 0; // �u anki hedef nokta index'i

    private void Start()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        // Hedef noktaya do�ru hareket et
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        // E�er karakter hedef noktaya ula�t�ysa bir sonraki hedef noktaya ge�
        if (transform.position == targetPosition)
        {
            currentWaypointIndex++;

            // E�er t�m hedef noktalar� tamamland�ysa hareketi durdur
            if (currentWaypointIndex >= waypoints.Length)
            {
                Debug.Log("Hedefe ula��ld�!");
                return;
            }
        }

        // Kald�r�m�n y�zeyinde kalmas�n� sa�lamak i�in y d�zlemi sabit tutulur
        Vector3 newPosition = new Vector3(transform.position.x, waypoints[currentWaypointIndex].position.y, transform.position.z);
        transform.position = newPosition;

        // Hedef noktaya ula��lmad�ysa d�ng�y� devam ettir
        Invoke("MoveToWaypoint", 0.1f);
    }
}
