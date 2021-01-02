using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : Singleton<OverlayManager> {
    private Dictionary<string, OverlayBase> _overlayache = new Dictionary<string, OverlayBase>();

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

    public void Add(string overlayName, OverlayBase overlay) {
        _overlayache.Add(overlayName, overlay);
    }
    
    public void Remove(string overlayName) {
        _overlayache.Remove(overlayName);
    }
}
