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
        PopupManager.Instance.Show("ReadyPopup", GameData.Instance.stages[0].levels[stepInfo.index], par => {
            if (par != null) {
                var result = (string) par;
                if (result == "start") {
                    SceneManager.LoadScene("PlayScene");
                }
            }
        });
    }
}
