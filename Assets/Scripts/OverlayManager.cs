using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoSingleton<OverlayManager> {
    private Dictionary<string, OverlayBase> _overlayache = new Dictionary<string, OverlayBase>();
    
    void Start() {
        var overlays = GetComponentsInChildren<OverlayBase>(true);
        foreach (var overlay in overlays) {
            _overlayache.Add(overlay.name, overlay);
        }

    }

    public void Show(string overlayName) {
        if (_overlayache.ContainsKey(overlayName)) {
            _overlayache[overlayName].Show();
        } else {
            Debug.LogError(overlayName + " is not defined");
        }
    }
    
    public void Close(string overlayName) {
        if (_overlayache.ContainsKey(overlayName)) {
            _overlayache[overlayName].Hide();
        } else {
            Debug.LogError(overlayName + " is not defined");
        }
    }
}
