using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowQuestPointer : MonoBehaviour
{
    public static WindowQuestPointer Instance { get; private set; }
    


    [SerializeField] private Camera uiCamera;
    [SerializeField] private Sprite ArrowSprite;
    [SerializeField] private Sprite CrossSprite;
    
    private Vector3 Temp = new Vector3(0, 0, 500);
    private List<Vector3> GumballMachinePositions;
    private List<QuestPointer> questPointerList;
    

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

        questPointerList = new List<QuestPointer>();
        GumballMachinePositions = new List<Vector3>();
        

    }

    private void Start()
    {
        uiCamera.depth = 0;
        Camera.main.depth = 0;
    }

    private void Update()
    {
        foreach (QuestPointer questPointer in questPointerList)
        {
            questPointer.Update();
        }
    }

    

   public QuestPointer FindPointer(Vector3 Position)
    {
        QuestPointer FoundQuestPointer = null;

        foreach (QuestPointer questPointer in questPointerList)
        {
            if(Position == questPointer.TargetPosition)
            {
                FoundQuestPointer = questPointer;
            }
        }
        return FoundQuestPointer;
    }

   

    public QuestPointer CreatePointer(Vector3 targetPosition) 
    {
        GameObject pointerGameObject = Instantiate(transform.Find("PointerTemplate").gameObject);
        pointerGameObject.SetActive(true);
        pointerGameObject.transform.SetParent(transform, false);
        QuestPointer questPointer = new QuestPointer(targetPosition, pointerGameObject, ArrowSprite, CrossSprite,uiCamera);
        questPointerList.Add(questPointer);
        
        return questPointer;
    }

    public void DestroyPointer(Vector3 Position)
    {
        
        QuestPointer questPointer = FindPointer(Position);
        if (questPointer != null)
        {
            questPointerList.Remove(questPointer);
            questPointer.DestroySelf();
        }
    }

    
    public class QuestPointer
    {
        private Camera uiCamera;
        private GameObject pointerGameObject;
        private Sprite ArrowSprite;
        private Sprite CrossSprite;
        public Vector3 TargetPosition;
        private RectTransform pointerRectTransform;
        private Image pointerImage;

        public QuestPointer(Vector3 TargetPosition, GameObject pointerGameObject, Sprite ArrowSprite, Sprite CrossSprite, Camera uiCamera)
        {
            this.TargetPosition = TargetPosition;
            this.pointerGameObject = pointerGameObject;
            this.ArrowSprite = ArrowSprite;
            this.CrossSprite = CrossSprite;
            pointerRectTransform = pointerGameObject.GetComponent<RectTransform>();
            pointerImage = pointerGameObject.GetComponent<Image>();
            this.uiCamera = uiCamera;
        }

        public void Update()
        {


            float borderSize = 100f;

            Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(TargetPosition);
            Debug.Log(targetPositionScreenPoint);
            bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;
            

            //if (isOffScreen)
           //{
                RotatePointerTowardsTargetPosition();
                pointerImage.sprite = ArrowSprite;
                Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
                if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
                if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
                if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
                if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

                Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
                pointerRectTransform.position = pointerWorldPosition;
                pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);


            //}

            //else
            //{
            //    pointerImage.sprite = CrossSprite;
            //    Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            //    pointerRectTransform.position = pointerWorldPosition;
            //    pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
            //    pointerRectTransform.localEulerAngles = Vector3.zero;

            //}
        }

        private void RotatePointerTowardsTargetPosition()
        {
            Vector3 toPosition = TargetPosition;
            Vector3 fromPosition = Camera.main.transform.position;
            fromPosition.z = 0f;
            Vector3 dir = (toPosition - fromPosition).normalized;
            float angle = Helpers.GetAngleFromVectorFloat(dir);
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        }
        public void DestroySelf()
        {
            Destroy(pointerGameObject);
        }
    }



   

}
