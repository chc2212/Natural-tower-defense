using UnityEngine;
using System.Collections;

public class CastleInfo : MonoBehaviour {
	
	public csCastleDB castleDB;
	public csUserSaveNLoad user;
	
	public float health;
	public float maxHealth;
	public float defense;
	public float turretAttackDamage;
	public float turretAttackRange;
	public float turretMissileSpeed;
	public float turretAttackSpeed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			if(health <= 0)
			{
				Destroy(this.gameObject.transform.FindChild("point1").gameObject);
				Destroy(this.gameObject.transform.FindChild("point2").gameObject);
				Destroy(this.gameObject.transform.FindChild("point3").gameObject);
				Destroy(this.gameObject.transform.FindChild("point4").gameObject);
				
				
				this.gameObject.SetActive(false);
				
				PlayScene.state = PlayScene.STATE.GAMEOVER;
			}
		}
	}
	
	void DamagedByMonster(int damage){
		if(damage - defense > 0.5f)
			health = health - (damage - defense);
		else if(damage - defense <= 0.5f)
			health = health - 0.5f;
	}
}
