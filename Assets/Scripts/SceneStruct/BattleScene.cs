using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleScene : MonoBehaviour {
    [SerializeField] private CharacterBase[] characters;

    void Start() {
        foreach (var ch in characters) {
            //ch.CharacterType = CharacterBase.Type.GUARDIAN;
        }
        
        GameManager.Instance.ReadyGame();
        GameManager.Instance.StartGame();
    }

}
