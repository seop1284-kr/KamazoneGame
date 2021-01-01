using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonFx.Json;
using System.IO;

public class GameData : Singleton<GameData> {
    public class Profile {
        
    }

    public Stage[] stages;

    // 스테이지 데이터 가져오기
    public void LoadStageData() {
        string fileName = "stage_data";
        TextAsset textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;

        if (textAsset == null) { Debug.LogError(fileName + " is not found"); }
        else { stages = JsonReader.Deserialize<Stage[]>(textAsset.text); }
    }
}

public enum Type {
    BATTLE,
    QUESTION,
    SHOP,
    START,
    BOSS
}

