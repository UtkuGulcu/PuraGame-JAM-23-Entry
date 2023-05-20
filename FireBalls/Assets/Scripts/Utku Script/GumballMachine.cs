using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Time = UnityEngine.Time;

public class GumballMachine : MonoBehaviour
{
    //[SerializeField] private float depleteSpeed;
    [SerializeField] private int depleteTime;
    [SerializeField] private Image fillAmountBar;
    
    private float fillAmount;
    private float lerpTime;

    private void Start()
    {
        fillAmount = 100f;
        lerpTime = 0;
        StartCoroutine(StartDecreasingGum(fillAmount));
    }

    private void Update()
    {
        //fillAmount = Mathf.Lerp(fillAmount, 0, depleteSpeed * Time.deltaTime);
        fillAmountBar.fillAmount = fillAmount / 100;
        Debug.Log(fillAmount);
    }

    private IEnumerator StartDecreasingGum(float startAmount)
    {
        while (lerpTime <= depleteTime)
        {
            lerpTime += Time.deltaTime;
            fillAmount = Mathf.Lerp(startAmount, 0, lerpTime / depleteTime);
            yield return Helpers.GetWaitForEndOfFrame();
        }

        lerpTime = 0f;
    }
}
