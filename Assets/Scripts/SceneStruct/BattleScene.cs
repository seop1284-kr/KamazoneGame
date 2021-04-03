using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleScene : SceneBase {
    [SerializeField] private CharacterBase[] characters;
    [SerializeField] private Transform HeroCharacterRoot;
    [SerializeField] private Transform EnemyCharacterRoot;

    void Start() {
        Entered();
    }

    public override void Entered() {
        base.Entered();
        
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
        PopupManager.Instance.Show("ResultPopup", isWin, param => { SceneManager.Instance.ChangeScene("MapScene"); });
    }

    public override void OnEscape() {
        OnClickPause();
    }

    public void OnClickPause() {
        GameManager.Instance.Pause();
        
        PopupManager.Instance.Show("PausePopup", null, param => {
            
            GameManager.Instance.Resume();
            
            var exitType = (PausePopup.EXIT_TYPE) param;
            if (exitType == PausePopup.EXIT_TYPE.LOBBY) {
                SceneManager.Instance.ChangeScene("MapScene");
            }
        });
    }
}
