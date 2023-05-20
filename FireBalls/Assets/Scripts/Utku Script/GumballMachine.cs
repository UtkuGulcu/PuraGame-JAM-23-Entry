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

    public IEnumerator DecreaseGumCoroutine;
    public IEnumerator StartRefillingGumCoroutine;

    [SerializeField] private int timeToDecreaseGum;
    [SerializeField] private float decreaseAmount;
    
    public float fillAmount;

    private void Start()
    {
        fillAmount = 100f;

        DecreaseGumCoroutine = DecreaseGum();
        StartCoroutine(DecreaseGumCoroutine);
    }

    private IEnumerator DecreaseGum()
    {
        yield return Helpers.GetWait(timeToDecreaseGum);
        fillAmount -= decreaseAmount;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);

        InvokeFillAmountChanged();

        if (fillAmount > 0)
        {
            DecreaseGumCoroutine = DecreaseGum();
            StartCoroutine(DecreaseGumCoroutine);
        }

    }
    
    public void StartInteracting()
    {
        StopCoroutine(DecreaseGumCoroutine);
        StartRefillingGumCoroutine = StartRefillingGum();
        StartCoroutine(StartRefillingGumCoroutine);
    }

    public void StopInteracting()
    {
        StopCoroutine(StartRefillingGumCoroutine);
        DecreaseGumCoroutine = DecreaseGum();
        StartCoroutine(DecreaseGumCoroutine);
    }

    private IEnumerator StartRefillingGum()
    {
        fillAmount += 25;
        fillAmount = Mathf.Clamp(fillAmount, 0, 100);

        yield return Helpers.GetWait(1);

        InvokeFillAmountChanged();

        if (fillAmount < 100)
        {
            StartRefillingGumCoroutine = StartRefillingGum();
            StartCoroutine(StartRefillingGumCoroutine);
        }
    }

    private void InvokeFillAmountChanged()
    {
        OnFillAmountChanged?.Invoke(this, new OnFillAmountChangedEventArgs
        {
            fillAmount = fillAmount
        });
    }
}
