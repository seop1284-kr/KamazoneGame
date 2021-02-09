using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryPopup : PopupBase {
    [SerializeField] private StoryControl storyControl;
    
    private Stage stage;

    public override void Enter(object param) {
        stage = param as Stage;
        storyControl.ShowStory(stage);
    }
    
    public void OnClickOk() {
        
        PopupManager.Instance.Close("ok");
    }
}
