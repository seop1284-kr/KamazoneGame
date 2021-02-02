using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonFx.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GameData : Singleton<GameData> {
    public class Profile {
        
    }

    public Stage[] stages;
    public Monster[] monsters;
    public Player playerInfo;
    private string dataPath = Application.persistentDataPath + "/gameData.dat";

    // 스테이지 데이터 가져오기
    public void LoadStageData() {
        string fileName = "stage_data";
        TextAsset textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;

        if (textAsset == null) { Debug.LogError(fileName + " is not found"); }
        else { stages = JsonReader.Deserialize<Stage[]>(textAsset.text); }
    }

    // 몬스터 데이터 가져오기
    public void LoadMonsterData() {
        string fileName = "monster_data";
        TextAsset textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;

        if (textAsset == null) { Debug.LogError(fileName + " is not found"); }
        else { monsters = JsonReader.Deserialize<Monster[]>(textAsset.text); }
    }

    // 플레이어 저장 데이터 가져오기
    public void LoadPlayerData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        playerInfo = (Player)bf.Deserialize(file);
        // cheat
        file.Close();
    }

    // 플레이어 데이터 초기화
    public void InitPlayerData() {
        
        string fileName = "player_init_data";
        TextAsset textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        if (textAsset == null) { Debug.LogError(fileName + " is not found"); }
        else {
            Player player = new Player();
            player = JsonReader.Deserialize<Player>(textAsset.text);
            bf.Serialize(file, player);
            file.Close();
        }
    }
}

public enum Type {
    BATTLE,
    QUESTION,
    SHOP,
    START,
    BOSS
}

