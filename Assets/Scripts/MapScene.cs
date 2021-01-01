using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScene : MonoBehaviour {
    [SerializeField] private Step[] steps;

    private void Start() {
        GameData.Instance.LoadStageData();
        Debug.Log(GameData.Instance.stages[0].name);
        Debug.Log(GameData.Instance.stages[0].levels[0].type);
    } 
    
}
