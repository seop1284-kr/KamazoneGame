using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroInfo {
    public int index;
    public string name;
    public int hp;
    public int curHp;
    public int str;
    public int lv;

    public HeroInfo(Hero hero) {
        index = hero.index;
        name = hero.name;
        hp = hero.hp;
        curHp = hero.curHp;
        str = hero.str;
        lv = hero.lv;
    }
}

public class ProfileCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI displayText;
    HeroInfo heroInfo;
    public void SetInfo(Hero hero) {
        if (hero == null) {
            displayText.text = "";
        } else {
            this.heroInfo = new HeroInfo(hero);
            displayText.text = heroInfo.name;
        }
        
    }


}
