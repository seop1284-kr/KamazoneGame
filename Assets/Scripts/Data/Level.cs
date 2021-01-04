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

public class Monster {
    public int index;
    public string name;
    public int hp;
    public int str;
	public int lv;
}

