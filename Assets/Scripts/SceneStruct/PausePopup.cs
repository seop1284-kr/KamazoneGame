using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePopup : PopupBase
{
    public enum EXIT_TYPE {
        CLOSE,
        LOBBY
    }

    public override void Enter(object param) {
        base.Enter(param);
        
        
    }

    public void OnClickClose() {
        PopupManager.Instance.Close(EXIT_TYPE.CLOSE);
        
    }

    public void OnClickLobby() {
        PopupManager.Instance.Close(EXIT_TYPE.LOBBY);
    }

    public override void OnEscape() {
        OnClickClose();
    }
}
