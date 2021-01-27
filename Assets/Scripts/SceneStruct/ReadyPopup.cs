﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPopup : PopupBase {
    
    [SerializeField] private ProfileControl profileControl;
    [SerializeField] private BoardControl boardControl;
    
    private Level level;

    public override void Enter(object param) {
        level = param as Level;
        boardControl.InitBoard(level);
        profileControl.Init();
    }
    
    public void OnClickStart() {
        PopupManager.Instance.Close("start");
        
    }
}
