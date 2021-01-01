public class Stage {
	public Level[] levels;

}

public class Level {
	public int index;	// unique
	public int stage;	// floor = stage 배열 인덱스와 연결
	public int row;
	public int col;
	public Type type;
}
