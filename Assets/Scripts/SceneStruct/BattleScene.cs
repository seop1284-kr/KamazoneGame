using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleScene : MonoBehaviour {
    [SerializeField] private Character[] characters;

    void Start() {
        foreach (var ch in characters) {
            ch.CharacterType = Character.Type.GUARDIAN;
        }
        
        GameManager.Instance.ReadyGame(characters.ToList());
        GameManager.Instance.StartGame();
    }

}
