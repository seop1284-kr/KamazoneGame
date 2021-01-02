using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayRoot : MonoBehaviour {
    private OverlayBase[] _overlays;
    void Start() {
        _overlays = GetComponentsInChildren<OverlayBase>(true);
        foreach (var overlay in _overlays) {
            OverlayManager.Instance.Add(overlay.name, overlay);
        }
    }

    private void OnDestroy() {
        foreach (var overlay in _overlays) {
            OverlayManager.Instance.Remove(overlay.name);
        }
    }
}