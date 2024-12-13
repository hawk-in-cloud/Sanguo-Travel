using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerStatUpgradeDisaplay : MonoBehaviour
{
    public TMP_Text valueText, costText;
    public GameObject button;

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        valueText.text = $"数值 : {oldValue:F1} => {newValue:F1}";
        costText.text = $"消耗 : {cost}";

        if (cost <= CoinContronller.Instance.currentCoin)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }

    public void ShowMaxLevel()
    {
        valueText.text = $"数值 : 無制限";
        costText.text = $"消耗 : 無制限";
        button.SetActive(false);

    }
}
