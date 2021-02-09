using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultPopup : PopupBase {
    [SerializeField] private TextMeshProUGUI resultText;

    public override void Enter(object param) {
        bool isWin = (bool) param;
        
        resultText.text = isWin? "WIN" : "LOSE";
    }

    public void OnClickClose() {
        PopupManager.Instance.Close();
    }
}
