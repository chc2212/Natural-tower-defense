[System.Serializable]
public class csClassMonster{
	
	public string name;
	public string description; 
	public MonsterType type;
	public float health;		
	public float defense;		
	public float attackDamage;	
	public float attackSpeed;	
	public float attackRange;
	public float detectRange;
	public float moveSpeed;
	public float giveSilver;
	public SpecialAbility ability;
	
	public int moveImageNumber;
	public int attackImageNumber;

	public enum MonsterType{
		Wave,
		SemiBoss,
		Boss	
	}
	
	
	public enum SpecialAbility{
		None
	}
}
