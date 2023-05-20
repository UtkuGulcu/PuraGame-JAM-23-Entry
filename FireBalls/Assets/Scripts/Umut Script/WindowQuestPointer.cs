using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowQuestPointer : MonoBehaviour
{
    public static WindowQuestPointer Instance { get; private set; }
    


    [SerializeField] private Camera uiCamera;
    [SerializeField] private Sprite arrowSprite;
    [SerializeField] private Sprite crossSprite;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;
    private Image pointerImage;
    private Vector3 Temp = new Vector3(0, 0, 500);
    

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


        
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        pointerImage = transform.Find("Pointer").GetComponent<Image>();

    }

    private void Update()
    {
        targetPosition = Temp;
        if (Temp.z != 500)
        {
            Vector3 toPosition = targetPosition;
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f; //KONTORL ET
            Vector3 dir = (toPosition - fromPosition).normalized;
            float angle = Helpers.GetAngleFromVectorFloat(dir);
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

            float borderSize = 100f;

            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
            bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
            Debug.Log(isOffScreen + " " + targetPositionScreenPoint);

            if (isOffScreen)
            {
                RotatePointerTowardsTargetPosition();
                pointerImage.sprite = arrowSprite;
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
                if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
                if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
                if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);


            }

            else
            {
                pointerImage.sprite = crossSprite;
                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
                pointerRectTransform.localEulerAngles = Vector3.zero;

            }

        }
    }

    private void RotatePointerTowardsTargetPosition()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = Camera.main.transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = Helpers.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle); 
    }

   

    public void Hide() //Doldurduðumuz þeker makinasýný iþaretlemeyi býraktýr.
    {
        gameObject.SetActive(false);

    }

    public void Show(Vector3 targetPosition) // Azalmýþ þeker makinasýnýn konumunu parametre olarak göndericez.
    {
        gameObject.SetActive(true);

    }

    public void ReceivePosition(Vector3 position)
    {
        Temp = position;
    }

}
