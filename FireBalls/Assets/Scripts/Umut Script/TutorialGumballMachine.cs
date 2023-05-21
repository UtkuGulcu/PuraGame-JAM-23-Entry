using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialGumballMachine : MonoBehaviour , IInteractable
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

    [SerializeField] private TMP_Text txtnext;
    [SerializeField] private TMP_Text txt;

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
        

        if(TutorialManager.Instance.Contoller == false)
        {
            TutorialManager.Instance.Contoller = true;
            txtnext.gameObject.SetActive(true);
            txt.gameObject.SetActive(false);
        }
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
            ResourceManager.Instance.IncreaseMoney(25);
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


    public void ShowInteract()
    {

    }

    public void HideInteract()
    {

    }
}
