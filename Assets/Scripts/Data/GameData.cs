using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonFx.Json;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;


// singlton -> static으로..
public class GameData : Singleton<GameData> {
    public class Profile {
        
    }

    public Stage[] stages;
    public Dictionary<int, CharacterInfo> CharacterInfos = new Dictionary<int, CharacterInfo>();
    public Player playerInfo = null;
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

        if (textAsset == null) {
            Debug.LogError(fileName + " is not found");
        } else {
            var characterInfoArray = JsonReader.Deserialize<CharacterInfo[]>(textAsset.text);
            foreach (var characterInfo in characterInfoArray) {
                if (CharacterInfos.ContainsKey(characterInfo.type)) continue;
                CharacterInfos.Add(characterInfo.type, characterInfo);
            }
        }
    }

    // 플레이어 데이터 저장하기
    public void SavePlayerData() {
        if (playerInfo == null) return;
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        bf.Serialize(file, playerInfo);
        file.Close();
    }

    // 플레이어 저장 데이터 가져오기
    public void LoadPlayerData() {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(dataPath, FileMode.Open);
        playerInfo = (Player)bf.Deserialize(file);
        file.Close();
    }

    // 플레이어 데이터 초기화
    public void InitPlayerData() {
        
        string fileName = "player_init_data";
        TextAsset textAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(dataPath);

        if (textAsset == null) {
            Debug.LogError(fileName + " is not found");
        } else {
            Player player = JsonReader.Deserialize<Player>(textAsset.text);
            bf.Serialize(file, player);
            file.Close();
        }
    }

    // 스텝 클리어시 데이터 처리
    public void StepClear() {
        // BOSS 스텝이면 스테이지 +1
        if (GameData.Instance.stages[playerInfo.stageIdx].levels[playerInfo.levelIdx].type == Type.BOSS) {
            GameData.Instance.playerInfo.isPlaying = true;
            GameData.Instance.playerInfo.clearedLevelList.Clear();
            GameData.Instance.playerInfo.monsters.Clear();
            GameData.Instance.playerInfo.stageIdx += 1;
            GameData.Instance.playerInfo.levelIdx = 0;
        } else {
            GameData.Instance.playerInfo.isPlaying = false;
            GameData.Instance.playerInfo.clearedLevelList.Add(playerInfo.levelIdx);
            GameData.Instance.playerInfo.monsters.Clear();
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
public enum StepStatusType {
    CHARON,
    NEXT,
    PASSED,
    NORMAL
}

