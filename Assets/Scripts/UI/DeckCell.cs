using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MonsterInfo {
    public int index;
    public string name;
    public int hp;
    public int str;
    public int lv;

    public MonsterInfo(Monster monster) {
        index = monster.index;
        name = monster.name;
        hp = monster.hp;
        str = monster.str;
        lv = monster.lv;
    }
}

public class DeckCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI displayText;
    MonsterInfo monsterInfo;
    public void SetInfo(Monster monster) {
        if (monster == null) {
            displayText.text = "empty";
        } else {
            this.monsterInfo = new MonsterInfo(monster);
            displayText.text = monsterInfo.name;
        }
        
    }
}
