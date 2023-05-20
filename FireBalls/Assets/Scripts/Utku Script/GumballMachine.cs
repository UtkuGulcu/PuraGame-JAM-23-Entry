using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GumballMachine : MonoBehaviour, IInteractable
{
    public class OnFillAmountChangedEventArgs : EventArgs
    {
        public float fillAmount;
    }


    public event EventHandler<OnFillAmountChangedEventArgs> OnFillAmountChanged;

    private IEnumerator DecreaseGumCoroutine;
    private IEnumerator StartRefillingGumCoroutine;
    private IEnumerator WaitForDecreaseCoroutine;
    private IEnumerator IncreaseMoneyCoroutine;

    //[SerializeField] private int timeToDecreaseGum;
    //[SerializeField] private float decreaseAmount;

    [SerializeField] private float decreaseSpeed;
    
    [HideInInspector] public float fillAmount;

    private void Start()
    {
        fillAmount = 100f;

        DecreaseGumCoroutine = DecreaseGum();
        StartCoroutine(DecreaseGumCoroutine);

        IncreaseMoneyCoroutine = IncreaseMoney();
        StartCoroutine(IncreaseMoneyCoroutine);
    }

    //private IEnumerator DecreaseGum()
    //{
    //    yield return Helpers.GetWait(timeToDecreaseGum);
    //    fillAmount -= decreaseAmount;
    //    fillAmount = Mathf.Clamp(fillAmount, 0, 100);

    //    InvokeFillAmountChanged();

    //    if (fillAmount > 0)
    //    {
    //        DecreaseGumCoroutine = DecreaseGum();
    //        StartCoroutine(DecreaseGumCoroutine);
    //    }

    //}

    private IEnumerator DecreaseGum()
    {
        yield return Helpers.GetWaitForEndOfFrame();
        fillAmount -= Time.deltaTime * decreaseSpeed;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);

        InvokeFillAmountChanged();

        if (fillAmount > 0)
        {
            DecreaseGumCoroutine = DecreaseGum();
            StartCoroutine(DecreaseGumCoroutine);
        }

    }

    public void Interact()
    {
        StopCoroutine(DecreaseGumCoroutine);
        StartRefillingGumCoroutine = StartRefillingGum();
        StartCoroutine(StartRefillingGumCoroutine);
    }

    public void StopInteracting()
    {
        StopCoroutine(StartRefillingGumCoroutine);

        WaitForDecreaseCoroutine = WaitForDecrease();
        StartCoroutine(WaitForDecreaseCoroutine);
    }

    private IEnumerator StartRefillingGum()
    {
        fillAmount += 5;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);

        yield return Helpers.GetWait(0.2f);

        InvokeFillAmountChanged();

        if (fillAmount < 100)
        {
            StartRefillingGumCoroutine = StartRefillingGum();
            StartCoroutine(StartRefillingGumCoroutine);
        }
    }

    private IEnumerator WaitForDecrease()
    {
        yield return Helpers.GetWait(5);
        
        DecreaseGumCoroutine = DecreaseGum();
        StartCoroutine(DecreaseGumCoroutine);
    }

    private IEnumerator IncreaseMoney()
    {
        yield return Helpers.GetWait(5);

        if (fillAmount > 0)
        {
            ResourceManager.Instance.IncreaseMoney(100);
            Debug.Log(ResourceManager.Instance.GetMoney());
        }

        IncreaseMoneyCoroutine = IncreaseMoney();
        StartCoroutine(IncreaseMoneyCoroutine);
    }

    

    private void InvokeFillAmountChanged()
    {
        OnFillAmountChanged?.Invoke(this, new OnFillAmountChangedEventArgs
        {
            fillAmount = fillAmount
        });
    }


    public void SendPositionToWindowQuestPointer(Vector3 position)
    {
        WindowQuestPointer.Instance.AddGumballMachineToList(position);
    }

    private bool isConditionMet = false;

    private void Update()
    {
        if (!isConditionMet && fillAmount < 50)
        {
            isConditionMet = true; //Þeker Makinasý doldurulduktan sonra bu bool false dönmeli
            Vector3 position = transform.position;
            WindowQuestPointer.Instance.CreatePointer(position);
        }
    }




}
