﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScene : SceneBase {
    [SerializeField] private Step startStep;
    [SerializeField] private Step[] steps;
    [SerializeField] private Step bossStep;
    [SerializeField] private CharacterInMap character;
    [SerializeField] private InfoText floorText;
    [SerializeField] private InfoText stageNameText;
    

    private void Start() {
        Step.OnClicked = OnClickStep;

        Invoke("Entered", 0f);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.C)) {
            GameData.Instance.StepClear();
            InitMap();
        }
        
        if(Input.GetKeyDown(KeyCode.R)) {
            PlayerPrefs.DeleteAll();
            InitMap();
        }
        
       
    }

    public override void Entered() {
        base.Entered();
        
        OverlayManager.Instance.Show("BasicOverlay");
        InitMap();
    }

    private void InitMap() {
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;
        
        // 현재 스테이지 정보 출력
        floorText.Info = (curStageIdx + 1).ToString() + " 스테이지";
        stageNameText.Info = GameData.Instance.stages[curStageIdx].name;

        // 스텝 비우기
        foreach (var step in steps) {
            step.SetActive(false);
            step.SetInfo(null);
        }

        // 스텝 초기화
        for (int i = 0; i < GameData.Instance.stages[curStageIdx].levels.Length; ++i) {
            var level = GameData.Instance.stages[curStageIdx].levels[i];
            if (level.row == -1) {
                // 시작 스텝 또는 보스 스텝
                if (level.type == Type.BOSS) {
                    bossStep.StepStatus = CheckStepType(level);
                    bossStep.SetInfo(level);
                } else if (level.type == Type.START) {
                    startStep.StepStatus = CheckStepType(level);
                    startStep.SetInfo(level);
                }
            } else {
                // 일반 스텝
                int idx = level.row + level.col * 4 - 4;
                Step step = steps[idx];

                step.SetActive(true);
                step.StepStatus = CheckStepType(level);
                step.SetInfo(level);
            }
        }

        // 캐릭터 이동
        character.Set(CheckCharPos());

        // 저장
        GameData.Instance.SavePlayerData();

    }

    // 스텝 클릭 액션 함수
    private void OnClickStep(Step step) {
        switch (step.StepStatus)
        {
            case StepStatusType.CHARON:
                switch (step.GetInfo().type)
                {
                    case Type.START:
                        // start step
                        PopupManager.Instance.Show("StoryPopup", GameData.Instance.stages[GameData.Instance.playerInfo.stageIdx], par => {
                            if (par != null) {
                                var result = (string) par;
                                if (result == "ok") {
                                    GameData.Instance.StepClear();
                                    InitMap();
                                }
                            }
                        });
                                
                        break;
                    case Type.SHOP:
                        // shop step
                        PopupManager.Instance.Show("ShopPopup", GameData.Instance.playerInfo, par => {
                            if (par != null) {
                                var result = (string) par;
                                if (result == "pass") {
                                    GameData.Instance.StepClear();
                                    InitMap();
                                }
                            }
                        });
                        break;
                    case Type.BATTLE:
                        // battle step - readypopup
                        PopupManager.Instance.Show("ReadyPopup", GameData.Instance.stages[GameData.Instance.playerInfo.stageIdx].levels[step.GetInfo().index], par => {
                            if (par != null) {
                                var result = (string) par;
                                if (result == "start") {
                                    SceneManager.Instance.ChangeScene("BattleScene");
                                    InitMap();
                                }
                            }
                        });
                        break;

                    case Type.BOSS:
                        // boss step
                        PopupManager.Instance.Show("ReadyPopup", GameData.Instance.stages[GameData.Instance.playerInfo.stageIdx].levels[step.GetInfo().index], par => {
                            if (par != null) {
                                var result = (string) par;
                                if (result == "start") {
                                    SceneManager.Instance.ChangeScene("BattleScene");
                                    InitMap();
                                }
                            }
                        });
                        break;

                    case Type.QUESTION:
                        // question step
                        break;

                }
                break;
                
            case StepStatusType.NEXT:
                // move CharacterInMap
                if (!GameData.Instance.playerInfo.isPlaying) {
                    GameData.Instance.playerInfo.levelIdx = step.GetInfo().index;
                    GameData.Instance.playerInfo.isPlaying = true;
                }
                break;
            case StepStatusType.PASSED:
                // block
                break;
            case StepStatusType.NORMAL:
                // block
                break;
        }
        InitMap();
    }



    // 캐릭터 위치 확인
    private Vector3 CheckCharPos() {
        Vector3 pos = new Vector3(0f,0f,0f);
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;
        var level = GameData.Instance.stages[curStageIdx].levels[curLevelIdx];

        foreach (Step step in steps) {
            if (curLevelIdx == step.GetInfo().index) {
                pos = step.Pos;
            }
        }
        // 시작 스텝 또는 보스 스텝
        if (level.type == Type.BOSS) {
            pos = bossStep.Pos;
        } else if (level.type == Type.START) {
            pos = startStep.Pos;
        }

        return pos;

    }

    // Step의 타입 확인
    private StepStatusType CheckStepType(Level level) {
        if (IsCleared(level)) return StepStatusType.PASSED;
        else if (IsCharacterOn(level)) return StepStatusType.CHARON;
        else if (IsNextStep(level) && !GameData.Instance.playerInfo.isPlaying) return StepStatusType.NEXT;
        else return StepStatusType.NORMAL;
    }

    private bool IsCleared(Level level) {
        foreach(int i in GameData.Instance.playerInfo.clearedLevelList) {
            if (level.index == i) { return true; }
        }
        return false;
    }

    private bool IsCharacterOn(Level level) {
        if (GameData.Instance.playerInfo.levelIdx == level.index) { return true; }
        return false;
    }

    private bool IsNextStep(Level level) {
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;
        foreach(int i in GameData.Instance.stages[curStageIdx].levels[curLevelIdx].tails) {
            if (level.index == i) { return true; }
        }
        return false;
    }

    public override void OnEscape() {
        SceneManager.Instance.ChangeScene("StartScene");
    }
}
