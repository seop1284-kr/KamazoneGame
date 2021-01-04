using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPopup : PopupBase {
    
    private DeckCell[] deckCells;
    
    private Level level;
    public override void Enter(object param) {
        level = param as Level;

    }
    public void InitBoard() {
        for (int i = 0; i < level.monsters.Length; ++i) {
            
        }
    }

    public void OnClickStart() {
        PopupManager.Instance.Close("start");
        
    }
}
