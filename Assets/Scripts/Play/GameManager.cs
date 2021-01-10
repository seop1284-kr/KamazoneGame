using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
	private List<Character> characters = new List<Character>();


	public void ReadyGame(List<Character> gList) {
		characters = gList;

		foreach (var character in characters) {
			character.SetInfo();
		}
	}
	
	public void StartGame() {
		
	}

	public Character GetCloseCharacter(Character ch) {
		var chType = (ch.CharacterType == Character.Type.GUARDIAN) ? Character.Type.ENEMY : Character.Type.GUARDIAN;
		
		return GetCloseCharacter(ch, chType);
	}

	private Character GetCloseCharacter(Character ch, Character.Type chType) {
		float dis = Mathf.Infinity;
		Character closeCharacter = null;
		
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

}
