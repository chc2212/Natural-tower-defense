[System.Serializable]
public class csClassTower{
	
	public string name;
	public string description; 
	public float health;		
	public float defense;		
	public float attackDamage;	
	public float attackSpeed;	
	public float attackRange;
	public float needSilver;
	public float buildTime;
	public float missileSpeed;
	public float needGold;
	
	public SpecialAbility ability;
	
	public enum SpecialAbility{
		Normal,
		Splash,
		multiplexTarget,
		SlowSpeed
	}
}
