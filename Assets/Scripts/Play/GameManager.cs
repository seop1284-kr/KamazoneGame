using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
	[SerializeField] private Transform HeroCharacterRoot;
	[SerializeField] private Transform EnemyCharacterRoot;
	
	private List<CharacterBase> characters = new List<CharacterBase>();
	
	public const float CELL_OFFSET = 2.5f;

	public static List<DeckCell.DeckCellInfo> heroCharacters;
	public static List<DeckCell.DeckCellInfo> enemyCharacters;

	public void SetupBoard() {
		// var heroCharacters = BoardControl.Instance.GetDeckCellInfos(CharacterBase.Type.GUARDIAN);
		
		foreach (var character in heroCharacters) {
			var characterBase = Resources.Load<CharacterBase>("Prefabs/Play/Character");
			var characterBasePrefab = GameObject.Instantiate(characterBase, HeroCharacterRoot);
			characterBasePrefab.SetInfo(character.Character, CharacterBase.Type.GUARDIAN);
			characterBasePrefab.SetPosition(character.Coord);
			characters.Add(characterBasePrefab);
		}

		// var enemyCharacters = BoardControl.Instance.GetDeckCellInfos(CharacterBase.Type.ENEMY);
		
		foreach (var character in enemyCharacters) {
			var characterBase = Resources.Load<CharacterBase>("Prefabs/Play/Character");
			var characterBasePrefab = GameObject.Instantiate(characterBase, EnemyCharacterRoot);
			characterBasePrefab.SetInfo(character.Character, CharacterBase.Type.GUARDIAN);
			characterBasePrefab.SetPosition(character.Coord);
			characters.Add(characterBasePrefab);
		}
	}
	
	public void ReadyGame() {
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
			
			var disPos = ch.transform.position - character.transform.position;
			if (Mathf.Abs(disPos.magnitude) < dis) {
				dis = Mathf.Abs(disPos.magnitude);
				closeCharacter = character;
			}
		}

		return closeCharacter;
	}

	public void Attack(CharacterBase to, float dmg) {
		to.AttackedFromOther(dmg);
	}

}
