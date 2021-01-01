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
}
