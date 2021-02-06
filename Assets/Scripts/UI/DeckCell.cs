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

    public Character CellCharacter { get; private set; }

    public Vector2Int Coord => coord;
    private Vector2Int coord;

    public void SetCoord(int idx) {
        coord.x = idx / 5;
        coord.y = idx % 5;
        cell.Coord = coord;
    }

    public void Init() {
        CellCharacter = null;
        displayText.text = "";
        
    }

    public void SetInfo(Character character) {
        if (character == null) {
            displayText.text = "";
        } else {
            CellCharacter = character;
            displayText.text = GameData.Instance.CharacterInfos[character.type].name;
        }
    }

    public void SetSelect(Character character) {
        CellCharacter = character;
    }

    public void SetHighlight(bool isActive) {
        image.color = isActive? Color.red : Color.white;
    }
}
