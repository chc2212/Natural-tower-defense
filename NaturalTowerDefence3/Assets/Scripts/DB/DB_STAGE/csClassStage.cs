[System.Serializable]
public class csClassStage{
	
	public string name;
	public string description;
	public int setWave;
	public int setMonsterNumberPerWave;
	public float monsterTimer;
	public int waveTimer;
	public int semiBossNumber;
	public int semiBossAfterWave;
	public int semiBossTimer;
	
	public int bossNumber;
	public int bossAfterWave;
	public int bossTimer;

	
	public string[] itemName = new string[3];
	public int[] getItemPer = new int[3];
}
