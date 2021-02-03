using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine.UI;

public class CharacterInMap : MonoBehaviour {
    private Vector3 from;
    private Vector3 to;
    private float startTime;
    private const float totalTime = 5.0f;

    public Vector3 GetMyPos() {
        int curStageIdx = GameData.Instance.playerInfo.stageIdx;
        int curLevelIdx = GameData.Instance.playerInfo.levelIdx;
        var level = GameData.Instance.stages[curStageIdx].levels[curLevelIdx];
        int idx = level.row + level.col * 4 - 4;
        Vector3 pos;
        pos.x = -12.5f + level.col * 5.0f;
        pos.y = 7.5f - level.row * 5.0f;
        pos.z = 0f;

        // Debug.Log(level.row + " " + level.col); // 2 1
        // Debug.Log(pos.x + " " + pos.y); // 2 1

        return pos;
    }
    public void Set() {
        gameObject.transform.position = GetMyPos();
        
    }

    public void GoTo() {

        to.Set (20, 10, -50);
        startTime = Time.time;

        // 매끄러운 애니메이션을 위해 16ms 마다 move 메소드 invoke
        InvokeRepeating ("move", 0, 0.01667f);
    }
    public void Move() {

        float deltaTime = Time.time - startTime;

        if (deltaTime < totalTime) {
            this.transform.position = Vector3.Lerp (from, to, deltaTime / totalTime);
        } else {
            this.transform.position = to;
            CancelInvoke ("move"); // 애니메이션이 종료되면 invoke repeater 종료
        }
    }

    
}