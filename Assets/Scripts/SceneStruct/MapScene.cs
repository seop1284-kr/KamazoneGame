using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScene : MonoBehaviour {
    [SerializeField] private Step[] steps;
    [SerializeField] private Step bossStep;

    private void Start() {
        Step.OnClicked += OnClickStep;

        Invoke("Entered", 0f);
    }

    private void Entered() {
        OverlayManager.Instance.Show("BasicOverlay");
        InitMap();
    }

    private void InitMap() {
        foreach (var step in steps) {
            step.SetActive(false);
        }

        for (int i = 0; i < GameData.Instance.stages[0].levels.Length; ++i) {
            var level = GameData.Instance.stages[0].levels[i];
            if (level.row == -1) {
                if (level.type == Type.BOSS) {
                    bossStep.SetInfo(level);
                }

            } else {
                int idx = level.row + level.col * 4 - 4;
                steps[idx].SetActive(true);
                steps[idx].SetInfo(level);
            }
        }
    }

    private void OnClickStep(StepInfo stepInfo) {
        stepInfo.SetIsNextStep(false);
        foreach (int i in GameData.Instance.stages[0].levels[GameData.Instance.playerInfo.levelIdx].tails) {
            if (stepInfo.index == i) {
                stepInfo.SetIsNextStep(true);
            }
        }

        if (GameData.Instance.playerInfo.isPlaying == true && GameData.Instance.playerInfo.levelIdx == stepInfo.index) {
            // 스텝 진행 중인 경우 // 클릭하면 팝업 출력
            PopupManager.Instance.Show("ReadyPopup", GameData.Instance.stages[0].levels[stepInfo.index], par => {
                if (par != null) {
                    var result = (string) par;
                    if (result == "start") {
                        SceneManager.LoadScene("BattleScene");
                    }
                }
            });
        } else if (GameData.Instance.playerInfo.isPlaying == false && stepInfo.isNextStep) {
            // 스텝 진행 중 아니고 다음 스텝인 경우 // 클릭하면 이동
            GameData.Instance.playerInfo.isPlaying = true;
            GameData.Instance.playerInfo.levelIdx = stepInfo.index;
        }
        
    }

}
