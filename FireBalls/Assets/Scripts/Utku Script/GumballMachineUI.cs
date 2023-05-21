using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GumballMachineUI : MonoBehaviour
{
    [SerializeField] private GumballMachine gumballMachine;
    [SerializeField] private Image fillAmountBar;


    private void Start()
    {
        gumballMachine.OnFillAmountChanged += GumballMachineOnOnFillAmountChanged;
    }

    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }

    private void OnDisable()
    {
        gumballMachine.OnFillAmountChanged -= GumballMachineOnOnFillAmountChanged;
    }

    private void GumballMachineOnOnFillAmountChanged(object sender, GumballMachine.OnFillAmountChangedEventArgs e)
    {
        fillAmountBar.fillAmount = e.fillAmount / 100;
    }
}
