using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : MonoBehaviour {
    [SerializeField] private Step[] steps;

    private void Start() {
        Step.OnClicked += OnClickStep;

        Invoke("Entered", 0.1f);
    }

    private void Entered() {
        OverlayManager.Instance.Show("BasicOverlay");
    }

    private void OnClickStep(StepInfo stepInfo) {
        PopupManager.Instance.Show("ReadyPopup");
    }
    
}
