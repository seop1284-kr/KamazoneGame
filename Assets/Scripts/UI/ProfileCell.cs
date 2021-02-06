using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 내가 소유하고있는 영웅 프로필 셀 (ReadyPopup)
public class ProfileCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI displayText;

    public static System.Action<ProfileCell> OnClickHero;
    
    public Character HeroInfo => heroInfo;
    
    Character heroInfo;
    public bool IsOn { get; set; }

    public void SetInfo(Character hero, int index = -1) {
        if (hero == null) {
            displayText.text = "";
        } else {
            heroInfo = hero;
            displayText.text = GameData.Instance.CharacterInfos[heroInfo.type].name;
        }
    }

    public void OnClick() {
        OnClickHero?.Invoke(this);
    }
}
