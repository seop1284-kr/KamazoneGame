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
        if (level != null) {
            type = level.type;
            index = level.index;
            row = level.row;
            col = level.col;    
        } else {
            type = Type.EMPTY;
            index = -1;
            row = -1;
            col = -1;
        }
        
    }
}

// MapScene에서 플레이어가 밟는 발판
public class Step : MonoBehaviour {
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private SpriteRenderer sprite;

    public static Action<Step> OnClicked;

    private StepInfo info;
    private Vector3 pos;
    
    public Vector3 Pos => gameObject.transform.position;
    public int Index => info.index;

    private StepStatusType stepStatus;
    public StepStatusType StepStatus {
        get { return stepStatus; }
        set { stepStatus = value; }
    }
    
    public void SetInfo(Level level) {

        this.info = new StepInfo(level);
        displayText.text = info.type.ToString();

        switch (StepStatus)
        {
            case StepStatusType.NORMAL:
                sprite.color = Color.white;
                break;
            case StepStatusType.CHARON:
                sprite.color = Color.green;
                break;
            case StepStatusType.PASSED:
                sprite.color = Color.grey;
                break;
            case StepStatusType.NEXT:
                sprite.color = Color.yellow;
                break;
        }
    }

    public StepInfo GetInfo() {
        return info;
    }

    public void SetActive(bool isActive) {
        gameObject.SetActive(isActive);
    }

    private void OnMouseDown() {
         OnClicked?.Invoke(this);
    }

}
