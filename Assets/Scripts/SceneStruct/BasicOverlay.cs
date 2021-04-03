using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicOverlay : OverlayBase {
    public void OnClickBack() {
        if (PopupManager.Instance.IsOpenedPopup) {
            PopupManager.Instance.Close();
        } else {
            // scene change
            SceneManager.Instance.ChangeScene("StartScene");
        }
    }
}
