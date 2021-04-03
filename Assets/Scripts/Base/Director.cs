using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class Director : MonoSingleton<Director> {
    private void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            if (PopupManager.Instance.IsOpenedPopup) {
                var popup = PopupManager.Instance.TopPopup;
                Assert.IsNotNull(popup, "popup != null");
                Assert.IsNotNull(popup.popupBase, "popup.popupBase != null");
                
                popup.popupBase.OnEscape();
            } else {
                Assert.IsNotNull(SceneManager.Instance.CurrentScene, "CurrentScene != null");
                
                SceneManager.Instance.CurrentScene.OnEscape();
            }
        }
    }
}
