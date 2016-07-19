using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower_FIRE : MonoBehaviour {

	public const int N_TARGET = 3;

	public enum TSTATE {WAIT, SEARCH, ATTACK, DEAD};
	
	public TSTATE tState = TSTATE.WAIT;
	
	public csTowerDB towerDB;
	public TowerInfo towerInfo;
	public csUserSaveNLoad user;
	
	public float health;		
	public float maxHealth;
	public float defense;	
	public float attackDamage;	
	public float attackSpeed;	
	public float attackRange;
	public float missileSpeed;
	public float buildTime;
	public csClassTower.SpecialAbility ability;
	
	public float finalDamage;
	public float finalSpeed;
	public float finalHealth; 
	
	public float nextHealth;		
	public float nextDefense;	
	public float nextAttackDamage;	
	public float nextAttackSpeed;	
	public float nextAttackRange;
	public float nextMissileSpeed;
	public float nextBuildTime;
	
	public float nextFinalDamage;
	public float nextFinalSpeed;
	public float nextFinalHealth;
	
	public int towerLevel = 0;
	public int towerType = 0;
	
	public float ftime;
	public float constructTime = 0;
	
	Collider[] enemies;
	Collider cTarget;
	List<Collider> lTarget = new List<Collider>();
	int monsterCount = 0;

	float a = 0;
	float b = 0;

	public AudioScirpt SoundManager;

	void Start () {
		user = GameObject.Find ("0.User").GetComponent<csUserSaveNLoad>();
		
		towerDB = GameObject.Find("DB_TOWER_TEST").GetComponent<csTowerDB>();
		towerInfo = GameObject.Find("3.GameManager").GetComponent<TowerInfo>();
		SoundManager = GameObject.Find("4.Sound Manager").GetComponent<AudioScirpt>();
		SetInfo();
	}
	
	// Update is called once per frame
	void Update () {
		
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			if(finalHealth <= 0)
			{
				tState = TSTATE.DEAD;
			}
			
			switch(tState)
			{
			case TSTATE.WAIT:
				constructTime += Time.deltaTime * PlayScene.playSpeed;

				if(constructTime < buildTime)
				{
					float color = constructTime / buildTime;
					this.gameObject.GetComponent<tk2dSprite>().color = new Color(color * 255f, color * 255f, color *255f, 255f) / 255f;
					return;
				}
				else
				{
					this.gameObject.GetComponent<tk2dSprite>().color = new Color(255f, 255f, 255f, 255f) / 255f;
					tState = TSTATE.SEARCH;
					constructTime = 0;
				}
				break;
				
			case TSTATE.SEARCH:
				ftime += Time.deltaTime;
				if(ftime < finalSpeed / PlayScene.playSpeed)	return;

				cTarget = null;
				lTarget.Clear();
				
				enemies = Physics.OverlapSphere(this.gameObject.transform.position, attackRange);

				if(ability == csClassTower.SpecialAbility.multiplexTarget)
				{
					monsterCount = 0;
					float[] distance = new float[enemies.Length];
					int[] minPosition = new int[enemies.Length];

					for(int i =0; i < enemies.Length; i++)
					{
						minPosition[i] = i;
						distance[i] = 99999f;
					}

					for(int i = 0; i < enemies.Length; i++)
					{
						if(enemies[i].gameObject.tag == "MONSTER")
						{
							distance[i] = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - enemies[i].gameObject.transform.position.x), 2)
							                         + Mathf.Pow((this.gameObject.transform.position.y - enemies[i].gameObject.transform.position.y), 2));

							monsterCount++;
						}
					} 

					for(int i = 0; i < enemies.Length; i++)
					{
						float temp_value = 0;
						int temp_position = 0;
						
						for(int j = 0; j < enemies.Length - 1; j++)
						{
							if(distance[j] > distance[j+1])
							{
								temp_value = distance[j];	distance[j] = distance[j+1];	distance[j+1] = temp_value;
								temp_position = minPosition[j];	minPosition[j] = minPosition[j+1];	minPosition[j+1] = temp_position;
							}
						}
					} 

					for(int i = 0; i < monsterCount; i++)	
					{
						if(i == N_TARGET)
							break;

						lTarget.Add(enemies[minPosition[i]]);
						//Debug.Log("What is it : "+enemies[minPosition[i]]);

						tState = TSTATE.ATTACK;
					}
				}

				else
				{
					for(int i = 0; i < enemies.Length; i++)
					{
						if(enemies[i].gameObject.tag == "MONSTER")
						{	
							if(enemies[i].gameObject.GetComponent<Monster>().mState == Monster.MSTATE.DEAD)
								continue; 
							
							if(cTarget == null)
								cTarget = enemies[i];
							
							else if(cTarget != null)
							{
								a = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - cTarget.gameObject.transform.position.x), 2)
								               + Mathf.Pow((this.gameObject.transform.position.y - cTarget.gameObject.transform.position.y), 2));
								b = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - enemies[i].gameObject.transform.position.x), 2)
								               + Mathf.Pow((this.gameObject.transform.position.y - enemies[i].gameObject.transform.position.y), 2));
							
								if(a > b){ 
									cTarget = enemies[i];
								}
							}
						}
					}		
					if(cTarget != null)
						tState = TSTATE.ATTACK;
				}

				break;
				
			case TSTATE.ATTACK:
				ftime += Time.deltaTime;
				if(ftime < finalSpeed / PlayScene.playSpeed)	return; 		

				if(ability == csClassTower.SpecialAbility.multiplexTarget)
				{
					tState = TSTATE.SEARCH;
					FireProcessing("multiplexTarget");

					ftime = 0.0f; 
				} 

				else
				{
					if(cTarget != null)
					{
						if(cTarget.gameObject.GetComponent<Monster>().mState == Monster.MSTATE.DEAD)
						{
							cTarget = null;
							return;
						} 

						switch(ability)
						{
						case csClassTower.SpecialAbility.Normal:
							FireProcessing("Normal");
							break;
						case csClassTower.SpecialAbility.SlowSpeed:
							FireProcessing("SlowSpeed");
							break;
						case csClassTower.SpecialAbility.Splash:
							FireProcessing("Splash");
							break;
						}

						ftime = 0.0f;
					}
					
					if(cTarget == null){
						tState = TSTATE.SEARCH;
						break;
					}
					
					a = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - cTarget.gameObject.transform.position.x), 2)
					               + Mathf.Pow((this.gameObject.transform.position.y - cTarget.gameObject.transform.position.y), 2));
					
					if(cTarget != null && a > attackRange) 
						tState = TSTATE.SEARCH;
				}

				break;
				
			case TSTATE.DEAD:
				SoundManager.Effect_TowerDestroy_ON();
				PlayScene.CurrentTowerCount --;
				Destroy(this.gameObject);
				break;
			}
		}		
	}

	void FireProcessing(string type)
	{
		GameObject Missile = null;
		GameObject[] L_Missile = new GameObject[N_TARGET];

		switch (type) 
		{
		case "Normal":
			Missile = Instantiate(Resources.Load ("Prefabs/3.MISSILE/pfNormalMissile")) as GameObject;
			break;
		case "multiplexTarget":
			for(int i = 0; i < monsterCount; i++)
			{
				if(i == N_TARGET)
					break;

				L_Missile[i] = Instantiate(Resources.Load ("Prefabs/3.MISSILE/pfLightningMissile")) as GameObject;

				PlayScene.CurrentMissileCount++;
				
				L_Missile[i].tag = "MISSILE";
				L_Missile[i].name = "missile";
				L_Missile[i].transform.parent = this.gameObject.transform.FindChild("Missile Port").transform;
				L_Missile[i].transform.position = this.gameObject.transform.FindChild("Missile Port").transform.position;
				
				L_Missile[i].GetComponent<Missile>().missileSpeed = missileSpeed;
				L_Missile[i].GetComponent<Missile>().missileDamage = finalDamage;
				L_Missile[i].GetComponent<Missile>().ability = ability;
				
				L_Missile[i].GetComponent<Missile>().target = lTarget[i].gameObject.transform;
			}
			break;
		case "SlowSpeed":
			Missile = Instantiate(Resources.Load ("Prefabs/3.MISSILE/pfSlowMissile")) as GameObject;
			break;
		case "Splash":
			Missile = Instantiate(Resources.Load ("Prefabs/3.MISSILE/pfFireMissile")) as GameObject;
			break;
		default :
			Missile = Instantiate(Resources.Load ("Prefabs/3.MISSILE/pfNormalMissile")) as GameObject;
			break;
		}

		if (ability != csClassTower.SpecialAbility.multiplexTarget)
		{
			PlayScene.CurrentMissileCount++;

			Missile.tag = "MISSILE";
			Missile.name = "missile";
			Missile.transform.parent = this.gameObject.transform.FindChild ("Missile Port").transform;
			Missile.transform.position = this.gameObject.transform.FindChild ("Missile Port").transform.position;

			Missile.GetComponent<Missile> ().missileSpeed = missileSpeed;
			Missile.GetComponent<Missile> ().missileDamage = finalDamage;
			Missile.GetComponent<Missile> ().ability = ability;

			Missile.GetComponent<Missile> ().target = cTarget.gameObject.transform;	
		}
	}
	
	void OnMouseUp(){
		if(PlayScene.state == PlayScene.STATE.IDLE && tState != TSTATE.WAIT)
		{
	
			PlayScene.state = PlayScene.STATE.TOWERINFO;


		}
		else if(PlayScene.state == PlayScene.STATE.SPELL1)
		{
			this.gameObject.GetComponent<Tower_FIRE>().finalHealth += 50 + user.userInfo.stat.finalIntelligence * 30;
			if(this.gameObject.GetComponent<Tower_FIRE>().finalHealth >= this.gameObject.GetComponent<Tower_FIRE>().maxHealth)
				this.gameObject.GetComponent<Tower_FIRE>().finalHealth = this.gameObject.GetComponent<Tower_FIRE>().maxHealth;
			Debug.Log ("Heal complete");
			Spell.spellTimer[0] = 0;
			SoundManager.Effect_Heal_ON();
			PlayScene.state = PlayScene.STATE.IDLE;
		}
	}
	
	void DamagedByMonster(int damage){
		if(damage - defense > 0.5f)
			finalHealth = finalHealth - (damage - defense);
		else if(damage - defense <= 0.5f)
			finalHealth = finalHealth - 0.5f; 
	}
	
	public void SetInfo()
	{
		health = towerDB.towerList[towerType].towerLevelList[towerLevel].health;
		defense = towerDB.towerList[towerType].towerLevelList[towerLevel].defense;
		attackDamage = towerDB.towerList[towerType].towerLevelList[towerLevel].attackDamage;
		attackSpeed = towerDB.towerList[towerType].towerLevelList[towerLevel].attackSpeed;
		attackRange = towerDB.towerList[towerType].towerLevelList[towerLevel].attackRange;
		missileSpeed = towerDB.towerList[towerType].towerLevelList[towerLevel].missileSpeed;
		buildTime = towerDB.towerList[towerType].towerLevelList[towerLevel].buildTime;
		ability = towerDB.towerList [towerType].towerLevelList [towerLevel].ability;


		
		finalDamage = attackDamage + ((user.userInfo.stat.finalStrength * 0.1f) * attackDamage); 
		finalHealth = health + ((user.userInfo.stat.finalConstitution * 0.1f) * health);
		finalSpeed = attackSpeed * Mathf.Pow(0.9f, user.userInfo.stat.finalAgility);
		
		maxHealth = finalHealth;


		if(towerLevel + 1 < towerDB.towerList[towerType].towerLevelList.Count)
		{
			nextHealth = towerDB.towerList[towerType].towerLevelList[towerLevel+1].health;
			nextDefense = towerDB.towerList[towerType].towerLevelList[towerLevel+1].defense;
			nextAttackDamage = towerDB.towerList[towerType].towerLevelList[towerLevel+1].attackDamage;
			nextAttackSpeed = towerDB.towerList[towerType].towerLevelList[towerLevel+1].attackSpeed;
			nextAttackRange = towerDB.towerList[towerType].towerLevelList[towerLevel+1].attackRange;
			nextMissileSpeed = towerDB.towerList[towerType].towerLevelList[towerLevel+1].missileSpeed;
			nextBuildTime = towerDB.towerList[towerType].towerLevelList[towerLevel+1].buildTime;
			
			nextFinalDamage = nextAttackDamage + ((user.userInfo.stat.finalStrength * 0.1f) * nextAttackDamage);
			nextFinalHealth = nextHealth + ((user.userInfo.stat.finalConstitution * 0.1f) * nextHealth);
			nextFinalSpeed = nextAttackSpeed * Mathf.Pow(0.9f, user.userInfo.stat.finalAgility);
		}
	} 
}
