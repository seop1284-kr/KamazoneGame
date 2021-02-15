using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : MonoBehaviour {
    [SerializeField] private CharacterBase[] characters;
    [SerializeField] private Transform HeroCharacterRoot;
    [SerializeField] private Transform EnemyCharacterRoot;
    void Start() {
        foreach (var ch in characters) {
            //ch.CharacterType = CharacterBase.Type.GUARDIAN;
        }
        
        GameManager.Instance.Init(HeroCharacterRoot, EnemyCharacterRoot);
        GameManager.Instance.ReadyGame();
        GameManager.Instance.StartGame();

        GameManager.Instance.OnGameOver += GameOver;
    }

    void GameOver(GAMEOVER_TYPE gameoverType) {
        bool isWin = gameoverType == GAMEOVER_TYPE.WIN;
        PopupManager.Instance.Show("ResultPopup", isWin, param => {
            SceneManager.LoadScene("MapScene");
        });
    }

}
