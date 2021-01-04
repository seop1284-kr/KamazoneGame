using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public struct StepInfo {
    public Type type;
    public int index;    // monster info
    public int row;
    public int col;

    public StepInfo(Level level) {
        type = level.type;
        index = level.index;
        row = level.row;
        col = level.col;
    }
    
}
public class Step : MonoBehaviour {
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private SpriteRenderer sprite;

    public static Action<StepInfo> OnClicked;

    private StepInfo info;

    public void SetActive(bool isActive) {
        gameObject.SetActive(isActive);
    }

    public void SetInfo(Level level) {
        this.info = new StepInfo(level);

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
        OnClicked?.Invoke(info);
    }

}
