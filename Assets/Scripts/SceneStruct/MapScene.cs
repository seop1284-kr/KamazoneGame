using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScene : MonoBehaviour {
    [SerializeField] private Step startStep;
    [SerializeField] private Step[] steps;
    [SerializeField] private Step bossStep;
    [SerializeField] private CharacterInMap character;


    private void Start() {
        Step.OnClicked = OnClickStep;

        Invoke("Entered", 0f);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.C)) {
            GameData.Instance.playerInfo.isPlaying = false;    
            GameData.Instance.playerInfo.clearedLevelList.Add(GameData.Instance.playerInfo.levelIdx);
            Debug.Log(GameData.Instance.playerInfo.clearedLevelList.Count);
        }
        InitMap();
    }

    private void Entered() {
        OverlayManager.Instance.Show("BasicOverlay");
        InitMap();
    }

    private void InitMap() {
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;

        // 스텝 비우기
        foreach (var step in steps) {
            step.SetActive(false);
        }

        // 스텝 초기화
        for (int i = 0; i < GameData.Instance.stages[curStageIdx].levels.Length; ++i) {
            var level = GameData.Instance.stages[curStageIdx].levels[i];
            if (level.row == -1) {
                // 시작 스텝 또는 보스 스텝
                if (level.type == Type.BOSS) {
                    bossStep.SetInfo(level);
                } else if (level.type == Type.START) {
                    startStep.SetInfo(level);
                }
            } else {
                // 일반 스텝
                int idx = level.row + level.col * 4 - 4;
                Step step = steps[idx];

                step.SetActive(true);
                step.SetInfo(level);                
            }
        }
        // 캐릭터 놓기
        character.Set();

    }

    private void OnClickStep(StepInfo stepInfo) {
        if (IsCleared(stepInfo)) {
            // block
        } else if (IsNextStep(stepInfo) && !IsPlaying()) {
            // move CharacterInMap
            GameData.Instance.playerInfo.levelIdx = stepInfo.index;
            GameData.Instance.playerInfo.isPlaying = true;
        } else if (IsCharacterOn(stepInfo)){
            // readyScene
            PopupManager.Instance.Show("ReadyPopup", GameData.Instance.stages[0].levels[stepInfo.index], par => {
                if (par != null) {
                    var result = (string) par;
                    if (result == "start") {
                        SceneManager.LoadScene("BattleScene");
                    }
                }
            });
        } else {
            //block
        }
        InitMap();
    }

    // 나중에 프로퍼티로 수정
    private bool IsCleared(StepInfo stepInfo) {
        foreach(int i in GameData.Instance.playerInfo.clearedLevelList) {
            if (stepInfo.index == i) { return true; }
        }
        return false;
    }

    private bool IsCharacterOn(StepInfo stepInfo) {
        if (GameData.Instance.playerInfo.levelIdx == stepInfo.index) { return true; }
        return false;
    }

    private bool IsNextStep(StepInfo stepInfo) {
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;
        foreach(int i in GameData.Instance.stages[curStageIdx].levels[curLevelIdx].tails) {
            if (stepInfo.index == i) { return true; }
        }
        return false;
    }

    private bool IsPlaying() {
        return GameData.Instance.playerInfo.isPlaying;
    }

}
