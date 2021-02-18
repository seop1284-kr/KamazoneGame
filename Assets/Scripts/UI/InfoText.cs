using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// MapScene에서 왼쪽 상단에 나타나는 플레이어 현재정보
public class InfoText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI displayText;
    private string infoText;
    public string Info {
        get { return infoText; }
        set { infoText = value; displayText.text = Info; }
    }

}
