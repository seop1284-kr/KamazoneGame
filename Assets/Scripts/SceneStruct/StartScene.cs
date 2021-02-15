using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class StartScene : MonoBehaviour
{
    public Button continueBtn;
    public Canvas popup;

    
    private string dataPath;
    
    void Start() {
        dataPath = Application.persistentDataPath + "/gameData.dat";
        if (hasLoadData()) continueBtn.gameObject.SetActive(true);
    }

    // 이어하기 버튼 // 게임 시작
    public void OnClickContinueBtn() {
        StartGame();
    }

    // 시작 버튼 // 저장된 데이터가 있는 경우 팝업 띄우기 // 저장된 데이터가 없는 경우 초기화 데이터로 시작 
    public void OnClickNewBtn() {
        if (hasLoadData()) {
            popup.gameObject.SetActive(true);
        } else {
            StartGame(true);
        }
    }

    // 팝업 확인 버튼 // 초기화 데이터로 시작
    public void OnClickOkBtn() {
        StartGame(true);
    }

    // 팝업 취소 버튼 // 팝업 없애기
    public void OnClickCancelBtn() {
        popup.gameObject.SetActive(false);
    }

    
    // 저장 데이터 있는지 확인 함수
    public bool hasLoadData() {
        if (File.Exists(dataPath)) return true;
        else return false;
    }

    private void StartGame(bool isInit = false) {
        if (isInit) {
            GameData.Instance.InitPlayerData();
        }
        GameData.Instance.LoadPlayerData();
        SceneManager.LoadScene("MapScene");
    }

}
