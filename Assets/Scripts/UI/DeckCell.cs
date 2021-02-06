using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine.UI;

// ready popup에서 덱을 배치할수있는 셀
public class DeckCell : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private MoveableCell cell;
    [SerializeField] private Image image;

    public class DeckCellInfo {
        public Character Character;
        public Vector2Int Coord;
    }
    
    public DeckCellInfo deckCellInfo = new DeckCellInfo();

    public Character CellCharacter => deckCellInfo.Character;

    public Vector2Int Coord => deckCellInfo.Coord;
    // private Vector2Int coord;

    public void SetCoord(int idx) {
        deckCellInfo.Coord.x = idx / 5;
        deckCellInfo.Coord.y = idx % 5;
        cell.Coord = deckCellInfo.Coord;
    }

    public void Init() {
        deckCellInfo.Character = null;
        displayText.text = "";
        
    }

    public void SetInfo(Character character) {
        if (character == null) {
            displayText.text = "";
        } else {
            deckCellInfo.Character = character;
            displayText.text = GameData.Instance.CharacterInfos[character.type].name;
        }
    }

    public void SetSelect(Character character) {
        deckCellInfo.Character = character;
    }

    public void SetHighlight(bool isActive) {
        image.color = isActive? Color.red : Color.white;
    }
}
