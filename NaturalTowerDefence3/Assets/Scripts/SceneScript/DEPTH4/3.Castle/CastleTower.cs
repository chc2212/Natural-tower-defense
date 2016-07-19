using UnityEngine;
using System.Collections;

public class CastleTower : MonoBehaviour {
	
	public CastleInfo castle;
	
	public float attackDamage;	
	public float attackSpeed;	
	public float attackRange;
	public float missileSpeed;
	
	public float ftime;
	Collider[] enemies;
	
	// Use this for initialization
	void Start () {
		castle = GameObject.Find("4.Castle").GetComponent<CastleInfo>();

		SetCastleInfo();
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			ftime += Time.deltaTime; 
	
			if(ftime < attackSpeed / PlayScene.playSpeed)	return;
			
			enemies = Physics.OverlapSphere(this.gameObject.transform.position, attackRange);
			for(int i = 0; i < enemies.Length; i++)
			{
				if(enemies[i].gameObject.tag == "MONSTER")
				{				
					GameObject Missile = Instantiate(Resources.Load ("Prefabs/3.MISSILE/pfMissile")) as GameObject;
					PlayScene.CurrentMissileCount++;
					
					Missile.tag = "MISSILE";
					Missile.name = "missile";
					Missile.transform.position = this.gameObject.transform.position;
					Missile.transform.parent = this.gameObject.transform;
					
					Missile.GetComponent<Missile>().target = enemies[i].gameObject.transform;
					Missile.GetComponent<Missile>().missileSpeed = missileSpeed;
					Missile.GetComponent<Missile>().missileDamage = attackDamage;
					
					break;
				}
			}
			
			ftime = 0;
		}

	}
	
	public void SetCastleInfo()
	{
		attackDamage = castle.turretAttackDamage;
		attackSpeed = castle.turretAttackSpeed;
		attackRange = castle.turretAttackRange;
		missileSpeed = castle.turretMissileSpeed;			
	}
}
