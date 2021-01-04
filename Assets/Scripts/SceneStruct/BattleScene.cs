using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleScene : MonoBehaviour {
    [SerializeField] private Character[] characters;
    [SerializeField] private Character[] enemies;

    void Start() {
        foreach (var ch in characters) {
            ch.CharacterType = Character.Type.GUARDIAN;
        }
        foreach (var ch in enemies) {
            ch.CharacterType = Character.Type.ENEMY;
        }
        GameManager.Instance.ReadyGame(characters.ToList(), enemies.ToList());
    }

}
