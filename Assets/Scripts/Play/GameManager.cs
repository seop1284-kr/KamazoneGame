using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
	private List<CharacterBase> characters = new List<CharacterBase>();


	public void ReadyGame(List<CharacterBase> gList) {
		characters = gList;

		foreach (var character in characters) {
			character.SetInfo();
		}
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
