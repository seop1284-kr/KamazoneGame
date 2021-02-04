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

    public void Set(Vector3 pos) {
        gameObject.transform.position = pos;
        
    }

    // public void GoTo() {

    //     to.Set (20, 10, -50);
    //     startTime = Time.time;

    //     // 매끄러운 애니메이션을 위해 16ms 마다 move 메소드 invoke
    //     InvokeRepeating ("move", 0, 0.01667f);
    // }
    // public void Move() {

    //     float deltaTime = Time.time - startTime;

    //     if (deltaTime < totalTime) {
    //         this.transform.position = Vector3.Lerp (from, to, deltaTime / totalTime);
    //     } else {
    //         this.transform.position = to;
    //         CancelInvoke ("move"); // 애니메이션이 종료되면 invoke repeater 종료
    //     }
    // }

    
}