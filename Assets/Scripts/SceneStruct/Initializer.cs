using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour {
    void Start() {
        GameData.Instance.LoadStageData();
        GameData.Instance.LoadMonsterData();
        GameData.Instance.LoadPlayerData();
        DontDestroyOnLoad(this);
    }
}