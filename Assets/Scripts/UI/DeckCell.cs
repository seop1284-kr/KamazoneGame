using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine.UI;

public class CellInfo {
    public int index;
    public int type;
    public string name;
    public int hp;
    public int str;
    public int lv;

    public CellInfo(Monster monster) {
        index = -1;
        type = monster.type;
        name = monster.name;
        hp = monster.hp;
        str = monster.str;
        lv = monster.lv;
    }
    
    public CellInfo(HeroInfo hero) {
        index = hero.index;
        type = hero.type;
        name = hero.name;
        hp = hero.hp;
        str = hero.str;
        lv = hero.lv;
    }
}

public class DeckCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private MoveableCell cell;
    [SerializeField] private Image image;

    public CellInfo CellInfo { get; private set; }

    public Vector2Int Coord => coord;
    private Vector2Int coord;

    public void SetCoord(int idx) {
        coord.x = idx / 5;
        coord.y = idx % 5;
        cell.Coord = coord;
    }

    public void Init() {
        CellInfo = null;
        displayText.text = "";
        
    }

    public void SetSell(CellInfo cellInfo) {
        CellInfo = cellInfo;
        displayText.text = CellInfo.name;
    }
    
    public void SetInfo(Monster monster) {
        if (monster == null) {
            displayText.text = "";
        } else {
            SetSell(new CellInfo(monster));
        }
    }
    
    public void SetInfo(HeroInfo hero) {
        if (hero == null) {
            displayText.text = "";
        } else {
            SetSell(new CellInfo(hero));
        }
    }

    public void SetSelect(CellInfo cellInfo) {
        CellInfo = cellInfo;
    }

    public void SetHighlight(bool isActive) {
        image.color = isActive? Color.red : Color.white;
    }
}
