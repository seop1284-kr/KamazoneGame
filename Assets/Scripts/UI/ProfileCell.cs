using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroInfo {
    public int index;
    public int type;
    public string name;
    public int hp;
    public int curHp;
    public int str;
    public int lv;
    public bool isOn;

    public HeroInfo(Hero hero, int idx) {
        index = idx;
        type = hero.type;
        name = GameData.Instance.monsters[hero.type].name;
        hp = hero.hp;
        curHp = hero.curHp;
        str = hero.str;
        lv = hero.lv;
        isOn = false;
    }
}

public class ProfileCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI displayText;

    public static System.Action<HeroInfo> OnClickHero;
    
    public HeroInfo HeroInfo => heroInfo;
    
    HeroInfo heroInfo;
    
    public void SetInfo(Hero hero, int index = -1) {
        if (hero == null) {
            displayText.text = "";
        } else {
            this.heroInfo = new HeroInfo(hero, index);
            displayText.text = heroInfo.name;
        }
    }

    public void OnClick() {
        OnClickHero?.Invoke(heroInfo);
    }
}
