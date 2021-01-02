using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRoot : MonoBehaviour {
    private PopupBase[] _popups;
    
    void Start() {
        _popups = GetComponentsInChildren<PopupBase>(true);
        foreach (var popup in _popups) {
            PopupManager.Instance.Add(popup.name, popup);
        }
    }

    private void OnDestroy() {
        foreach (var popup in _popups) {
            PopupManager.Instance.Remove(popup.name);
        }
    }
}
