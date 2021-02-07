using System.Collections.Generic;

// stage = floor
public class Stage {
	public Level[] levels;
	public int index;
	public string name;

}

// level = step
public class Level {
	public int index;	// unique
	public int stage;	// floor = stage 배열 인덱스와 연결
	public int row;
	public int col;
	public Type type;
	public int[] tails;
	public int[] monsters;	// monster index array
	public int[] monstersPos;	// monster position = row + (col * 5)
}

// character 종류별 기본 정보 (캐릭터 지식백과 느낌)
public class CharacterInfo {
    public int type;	// 종류
    public string name;
    public int hp;
    public int str;
	public int lv;	// 필요한가?
}

// 소유하고있는 캐릭터
[System.Serializable]
public class Character {
	public int index;	// 각 캐릭터의 고유 값, 같은 종류라도 구분하도록
	public int type;
    public int hp;
	public int curHp;
    public int str;
	public int lv;

	public Character() {
		
	}
	
	public Character(CharacterInfo characterInfo) {
		index = -1;
		type = characterInfo.type;
		hp = characterInfo.hp;
		curHp = characterInfo.hp;
		str = characterInfo.str;
		lv = characterInfo.lv;
	}
}

[System.Serializable]
public class Player {
	public string name;
	public int money;
	public Character[] heros;	// 보유하고 있는 영웅들
	public int stageIdx;
	public int levelIdx;
	public List<int> clearedLevelList;	// 클리어한 레벨 인덱스 리스트
	public List<Character> monsters;	// 현재 상대하고 있는 몬스터들
	public bool isPlaying;	// 플레이 중?
}
