using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BelongingsControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private TextMeshProUGUI PopulationText;

    public void ShowBelongings() {
        
        MoneyText.text = "소지금: " + GameData.Instance.playerInfo.money.ToString() + "G";
        PopulationText.text = "영웅: " + GameData.Instance.playerInfo.heros.Count.ToString() + "/10";
    }
}
