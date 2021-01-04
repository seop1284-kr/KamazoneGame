using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyPopup : PopupBase {
    public void OnClickStart() {
        PopupManager.Instance.Close("start");
        
    }
}
