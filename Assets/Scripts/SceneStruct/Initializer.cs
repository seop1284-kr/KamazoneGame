using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoSingleton<Initializer> {
    // 임시 코드
    private static bool isCalled = false;
   
    void Start() {
        GameData.Instance.LoadMonsterData();
        GameData.Instance.LoadStageData();
        GameData.Instance.LoadPlayerData();
    }

    // 게임 종료
    void OnApplicationQuit() {
        Debug.Log("game quit");
        GameData.Instance.SavePlayerData();

    }
    // 게임 일시정지
    void OnApplicationPause() {
        Debug.Log("game pause");
        GameData.Instance.SavePlayerData();

    }

}