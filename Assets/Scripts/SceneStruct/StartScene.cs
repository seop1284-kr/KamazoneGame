using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScene : MonoBehaviour
{
    public void OnClcikNewBtn() {
        Debug.Log("hi");

        SceneManager.LoadScene("MapScene");
    }

    // // 플레이어 저장 데이터 가져오기
    // private void LoadPlayerData() {
    //     string fileName = "player_info";
    //     TextAsset textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;

    //     if (textAsset == null) { Debug.LogError(fileName + " is not found"); }
    //     else { monsters = JsonReader.Deserialize<Monster[]>(textAsset.text); }

    // }

}
