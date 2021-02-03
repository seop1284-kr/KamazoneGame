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
        
        // 나중에 수정
        if (IsPassed()) {
            sprite.color = Color.grey;
        }

        if (IsCharacterOn()) {
            sprite.color = Color.green;    
        }

        if (IsCleared()) {
            sprite.color = Color.grey;
        }

        if (IsNextStep() && !IsPlaying()) {
            sprite.color = Color.yellow;
        }

        
        displayText.text = info.type.ToString();
    }

    // 나중에 삭제
    private bool IsPassed() {
        if (info.index < GameData.Instance.playerInfo.levelIdx) return true;
        return false;
    }

    private bool IsCleared() {
        foreach(int i in GameData.Instance.playerInfo.clearedLevelList) {
            if (info.index == i) { return true; }
        }
        return false;
        
    }

    private bool IsCharacterOn() {
        if (GameData.Instance.playerInfo.levelIdx == info.index) { return true; }
        return false;
    }

    private bool IsNextStep() {
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;
        foreach(int i in GameData.Instance.stages[curStageIdx].levels[curLevelIdx].tails) {
            if (info.index == i) { return true; }
        }
        return false;
    }

    private bool IsPlaying() {
        return GameData.Instance.playerInfo.isPlaying;
    }



    private void OnMouseDown() {
         OnClicked?.Invoke(info);
    }

}
