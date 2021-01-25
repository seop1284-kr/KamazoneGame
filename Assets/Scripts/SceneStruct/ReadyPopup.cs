using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPopup : PopupBase {
    
    [SerializeField] private DeckCell[] enemyDeckCells;
    
    private Level level;

    public override void Enter(object param) {
        level = param as Level;
        InitBoard();
    }
    public void InitBoard() {
        foreach (var enemyDeckCell in enemyDeckCells) {
            enemyDeckCell.SetInfo(null);
        }
        for (int i = 0; i < level.monstersPos.Length; ++i) {
            enemyDeckCells[level.monstersPos[i]].SetInfo(GameData.Instance.monsters[level.monsters[i]]);
        }
    }

    public void OnClickStart() {
        PopupManager.Instance.Close("start");
        
    }
}
