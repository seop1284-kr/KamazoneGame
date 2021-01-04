using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager> {
	private List<Character> guardians = new List<Character>();
	private List<Character> enemies = new List<Character>();


	public void ReadyGame(List<Character> gList, List<Character> eList) {
		guardians = gList;
		enemies = eList;
	}
	
	public void StartGame() {
		
	}

	public Character GetCloseCharacter(Character ch) {
		if (ch.CharacterType == Character.Type.GUARDIAN) {
			return GetCloseCharacter(ch, enemies);
 		} else {
			return GetCloseCharacter(ch, guardians);
		}
	}

	private Character GetCloseCharacter(Character ch, List<Character> characters) {
		float dis = Mathf.Infinity;
		Character closeCharacter = null;
		
		foreach (var enemy in characters) {
			var disPos = ch.transform.position - enemy.transform.position;
			if (Mathf.Abs(disPos.magnitude) < dis) {
				dis = Mathf.Abs(disPos.magnitude);
				closeCharacter = enemy;
			}
		}

		return closeCharacter;
	}

}
