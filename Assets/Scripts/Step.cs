using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Step : MonoBehaviour {
    public struct StepInfo {
        public Type type;
        public int index;    // monster info
        public int floor;
        public int row;
        public int col;
    }


    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private SpriteRenderer sprite;

    public Action OnClicked;

    private StepInfo info;

    public void SetInfo(StepInfo info) {
        this.info = info;

        displayText.text = info.type.ToString();

        
    }

    private void SetStatus() {
        // // current
        // if (GameData.Instance.savedData.order == info.order) {
        //     // cleared
        //     if (GameData.Instance.savedData.isCleared) {
        //         
        //     } else {
        //         
        //     }
        //
        //     return;
        // }
        //
        // // next
        // if (GameData.Instance.savedData.order == info.order - 1) {
        //     // opened
        //     if (GameData.Instance.savedData.isCleared) {
        //         
        //     } else {
        //         
        //     }
        // }
    }

    public void OnClick() {
        
    }

    private void OnMouseDown() {
        Debug.LogError("HERE");
        OnClicked?.Invoke();
    }

}
