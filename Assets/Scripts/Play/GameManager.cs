using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager> {
	public enum GAMEOVER_TYPE {
		WIN,
		LOSE,
		NONE
	}
	[SerializeField] private Transform HeroCharacterRoot;
	[SerializeField] private Transform EnemyCharacterRoot;
	
	private List<CharacterBase> characters = new List<CharacterBase>();
	
	public const float CELL_OFFSET = 2.5f;

	public static List<DeckCell.DeckCellInfo> heroCharacters;
	public static List<DeckCell.DeckCellInfo> enemyCharacters;
	
	public GAMEOVER_TYPE GameoverType { get; private set; }

	public void SetupBoard() {
		// var heroCharacters = BoardControl.Instance.GetDeckCellInfos(CharacterBase.Type.GUARDIAN);
		
		foreach (var character in heroCharacters) {
			var characterBase = Resources.Load<CharacterBase>("Prefabs/Play/Hero");
			var characterBasePrefab = GameObject.Instantiate(characterBase, HeroCharacterRoot);
			characterBasePrefab.SetInfo(character.Character, CharacterBase.Type.GUARDIAN);
			characterBasePrefab.SetPosition(character.Coord);
			characters.Add(characterBasePrefab);
		}

		// var enemyCharacters = BoardControl.Instance.GetDeckCellInfos(CharacterBase.Type.ENEMY);
		
		foreach (var character in enemyCharacters) {
			var characterBase = Resources.Load<CharacterBase>("Prefabs/Play/Enemy");
			var characterBasePrefab = GameObject.Instantiate(characterBase, EnemyCharacterRoot);
			characterBasePrefab.SetInfo(character.Character, CharacterBase.Type.ENEMY);
			characterBasePrefab.SetPosition(character.Coord);
			characters.Add(characterBasePrefab);
		}
	}
	
	public void ReadyGame() {
		GameoverType = GAMEOVER_TYPE.NONE;
		SetupBoard();
	}
	
	public void StartGame() {
		foreach (var character in characters) {
			character.Init();
		}
	}

	public CharacterBase GetCloseCharacter(CharacterBase ch) {
		var chType = (ch.CharacterType == CharacterBase.Type.GUARDIAN) ? CharacterBase.Type.ENEMY : CharacterBase.Type.GUARDIAN;
		
		return GetCloseCharacter(ch, chType);
	}

	private CharacterBase GetCloseCharacter(CharacterBase ch, CharacterBase.Type chType) {
		float dis = Mathf.Infinity;
		CharacterBase closeCharacter = null;
		
		foreach (var character in characters) {
			if (character.CharacterType != chType) continue;
			if (character.IsDead) continue;
			
			var disPos = ch.transform.position - character.transform.position;
			if (Mathf.Abs(disPos.magnitude) < dis) {
				dis = Mathf.Abs(disPos.magnitude);
				closeCharacter = character;
			}
		}

		return closeCharacter;
	}

	public void Attack(CharacterBase to, float dmg) {
		// TODO: 공격 못하고, 초기화?
		if (to.IsDead) return;
		
		to.AttackedFromOther(dmg);
	}

	public void Disappear(CharacterBase disappearCharacter) {
		if (GameoverType != GAMEOVER_TYPE.NONE) return;
		
		GameoverType = CheckGameEnd();
		
		if (GameoverType != GAMEOVER_TYPE.NONE) {
			GameOver();
		}
	}

	private void GameOver() {
		// save data
		GameData.Instance.StepClear();
		GameData.Instance.SavePlayerData();
		
		// result popup
		bool isWin = GameoverType == GAMEOVER_TYPE.WIN;
		PopupManager.Instance.Show("ResultPopup", isWin, param => { SceneManager.LoadScene("MapScene"); });
		
		// character animation
		foreach (var character in characters) {
			character.GameOver();
		}
	}

	private GAMEOVER_TYPE CheckGameEnd() {
		bool isAllHeroDead = true;
		bool isAllEnemyDead = true;
		
		foreach (var character in characters) {
			if (!character.IsDead) {
				if (character.CharacterType == CharacterBase.Type.ENEMY) isAllEnemyDead = false;
				else isAllHeroDead = false;
			}
		}

		if (isAllEnemyDead && !isAllHeroDead) return GAMEOVER_TYPE.WIN;
		if (isAllHeroDead && !isAllEnemyDead) return GAMEOVER_TYPE.LOSE;
		return GAMEOVER_TYPE.NONE;
	}

}
