using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowQuestPointer : MonoBehaviour
{
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private void Awake()
    {
        targetPosition = new Vector3(38,2,-1.5f);
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();

    }

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f; //KONTORL ET
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Helpers.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0,0,angle);

        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= 0 || targetPositionScreenPoint.x >= Screen.width || targetPositionScreenPoint.y <= 0 || targetPositionScreenPoint.y >= Screen.height;
        Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

    }
}
