using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicOverlay : OverlayBase
{
    public void OnClickBack() {
        if (PopupManager.Instance.IsOpenedPopup) {
            PopupManager.Instance.Close();
        } else {
            // scene change
            
        }
    }
}
