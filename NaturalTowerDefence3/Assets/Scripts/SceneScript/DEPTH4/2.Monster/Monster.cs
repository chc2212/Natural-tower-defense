using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour {
	
	public enum MSTATE {WAIT, MOVE, ATTACK, DEAD};
	
	public MSTATE mState = MSTATE.WAIT;
	
	public PlayScene PlaySceneObj;
	public csUserSaveNLoad user;
	public GameObject RoadEnd1;
	
	public float amtToMove = 0f;
	
	public float maxHealth;
	public float health;
	public float defense;
	public float attackDamage;
	public float attackSpeed;
	public float attackRange;
	public float detectRange;
	public float moveSpeed;
	public float finalMoveSpeed;
	
	public int moveImageNumber;
	public int moveIN = 1;
	public int attackImageNumber;
	int attackIN = 1;
	bool attackImageFlag = false;
	
	public int monsterNumber;
	
	Collider[] towers;
	
	Collider cTarget;
	float a = 0;
	float b = 0;
	
	public Transform target;
	float attackTimer = 0;
	float changeImageTimer = 0;
	float speedTimer = 0;
	float deadTimer = 1;

	public AudioScirpt SoundManager;
	//float ftime = 0.0f;
	
	// Use this for initialization
	void Start () {
		maxHealth = health;
		finalMoveSpeed = moveSpeed;
		PlaySceneObj = GameObject.Find("3.GameManager").GetComponent<PlayScene>();
		RoadEnd1 = GameObject.Find ("RoadEnd1");
		user = GameObject.Find ("0.User").GetComponent<csUserSaveNLoad>();
		SoundManager = GameObject.Find("4.Sound Manager").GetComponent<AudioScirpt>();
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			towers = Physics.OverlapSphere(this.gameObject.transform.position, detectRange); 
			
			amtToMove = PlayScene.playSpeed * moveSpeed * Time.deltaTime; 

			if(speedTimer >= 0)
			{
				speedTimer -= Time.deltaTime * PlayScene.playSpeed;
				this.GetComponent<tk2dSprite>().color = new Color(53f, 96f, 255f, 255f) / 255f; // blue
			}
			else 
			{
				moveSpeed = finalMoveSpeed;
				this.GetComponent<tk2dSprite>().color = new Color(255f, 255f, 255f, 255f) / 255f; // white
			}

			switch(mState){
			case MSTATE.WAIT:
				mState = MSTATE.MOVE;
				break;
			case MSTATE.MOVE:
				
				changeImageTimer += Time.deltaTime * PlayScene.playSpeed;
				if(changeImageTimer >= 0.1f)
				{
					if(moveIN < moveImageNumber)
						moveIN += 1;
					else
						moveIN = 1;
					GetComponent<tk2dSprite>().SetSprite(GetComponent<tk2dSprite>().GetSpriteIdByName(moveIN.ToString()));
					changeImageTimer = 0;
				} 
				
				cTarget = null;
				
				for(int i = 0; i < towers.Length; i++)
				{
					if(towers[i].gameObject.tag == "TOWER" || towers[i].gameObject.tag == "CASTLE")
					{
						if(this.gameObject.transform.position.x - 15 < towers[i].gameObject.transform.position.x)
						{
							if(this.gameObject.name == "monster")
							{
								if(Mathf.Abs(this.gameObject.transform.position.y - towers[i].gameObject.transform.position.y) > 20)
								{
									continue;

								}
							}
							if(cTarget == null)
								cTarget = towers[i];
							else if(cTarget != null)
							{
								a = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - cTarget.gameObject.transform.position.x), 2)
								               + Mathf.Pow((this.gameObject.transform.position.y - cTarget.gameObject.transform.position.y), 2));
								b = Mathf.Sqrt(Mathf.Pow((this.gameObject.transform.position.x - towers[i].gameObject.transform.position.x), 2)
								               + Mathf.Pow((this.gameObject.transform.position.y - towers[i].gameObject.transform.position.y), 2));

								if(a > b){ 
									cTarget = towers[i];
								}
							} 
						}
					}
				}
				if(cTarget != null)
				{
					mState = MSTATE.ATTACK;
					target = cTarget.gameObject.transform;
					return;		
				}
				
				if(transform.position.x < RoadEnd1.transform.position.x)
					transform.Translate(Vector3.right * amtToMove);
			
				break;
			case MSTATE.ATTACK:
				if(attackImageFlag == true)
				{
					changeImageTimer += Time.deltaTime * PlayScene.playSpeed;
					if(changeImageTimer >= attackSpeed - attackImageNumber * 0.2f)
					{
						if(changeImageTimer >= 0.2f + (attackSpeed - attackImageNumber * 0.2f))
						{
							if(attackIN < attackImageNumber)
							{
								attackIN += 1;
								changeImageTimer = attackSpeed - attackImageNumber * 0.2f;
							}
							else
							{
								attackIN = 1;
								changeImageTimer = 0;
							}
							GetComponent<tk2dSprite>().SetSprite(GetComponent<tk2dSprite>().GetSpriteIdByName("attack"+attackIN.ToString()));
						}
					} 
				}
				else if(attackImageFlag == false)
				{
					changeImageTimer += Time.deltaTime * PlayScene.playSpeed;
					if(changeImageTimer >= 0.1f)
					{
						if(moveIN < moveImageNumber)
							moveIN += 1;
						else
							moveIN = 1;
						GetComponent<tk2dSprite>().SetSprite(GetComponent<tk2dSprite>().GetSpriteIdByName(moveIN.ToString()));
						changeImageTimer = 0;
					} 
				}
				
				if(target == null)
					mState = MSTATE.MOVE;
				
				else if(target != null)
				{	
					if(Mathf.Sqrt(Mathf.Pow((transform.position.x - target.transform.position.x), 2) 
					              + Mathf.Pow((transform.position.y - target.transform.position.y), 2)) > attackRange) 
					{
						attackImageFlag = false;
						
						Vector3 v = (target.transform.position - this.gameObject.transform.position).normalized; 
						transform.Translate(v * amtToMove); 
					}
					else
					{
						attackTimer += Time.deltaTime * PlayScene.playSpeed; 
						
						attackImageFlag = true;
						
						if(attackTimer > attackSpeed)
						{
							if(target.tag == "CASTLE")
							{
								target.gameObject.transform.parent.gameObject.SendMessage("DamagedByMonster", attackDamage, SendMessageOptions.DontRequireReceiver);
							}
							else
							{
								target.gameObject.SendMessage("DamagedByMonster", attackDamage, SendMessageOptions.DontRequireReceiver);
							}
							attackTimer = 0;
						}
					}
				}
				break;
				
			case MSTATE.DEAD:
				if(this.gameObject.transform.FindChild("Progress Bar") != null)
					Destroy(this.gameObject.transform.FindChild("Progress Bar").gameObject); 
				
				GetComponent<tk2dSprite>().SetSprite(GetComponent<tk2dSprite>().GetSpriteIdByName("die"));
				
				deadTimer -= Time.deltaTime * PlayScene.playSpeed;
				
				GetComponent<tk2dSprite>().color = Color.Lerp(new Color(1,1,1,0), new Color(1,1,1,1), deadTimer/1.0f);
				// fade out
				
				if(deadTimer <= 0)
				{
					deadTimer = 1;
					
					PlayScene.CurrentMonsterCount--;
					PlaySceneObj.MonsterDead(monsterNumber);
					user.userInfo.achieveInfo.killEnemyCount++;
					SoundManager.Effect_MonsterDestroy_ON();
					Destroy(this.gameObject);
				}
				break;
			}
			
			if(health <= 0)
				mState = MSTATE.DEAD;
		}
	}
	
	void OnMouseUp(){
		if(PlayScene.state == PlayScene.STATE.SPELL2)
		{
			this.gameObject.GetComponent<Monster>().health -= 100 + (user.userInfo.stat.finalIntelligence * 40);
			
			Debug.Log ("공격 완료");
			Spell.spellTimer[1] = 0;
			SoundManager.Effect_Lightning_ON();
			PlayScene.state = PlayScene.STATE.IDLE;
		}
	}
	
	void DamagedByTower(string[] kind){
		//Debug.Log (kind[0]+"<-kind[0], kind[1]->"+kind[1]);
		string type = kind[0];
		float damage = float.Parse (kind [1]);
		//Debug.Log (damage);


		switch (type) 
		{
		case "Normal":
			if(damage - defense > 0.5f)
				health = health - (damage - defense);
			else if(damage - defense <= 0.5f)
				health = health - 0.5f; 
			SoundManager.Effect_Missile_Redball_ON();
			break;
		case "SlowSpeed":
		
			SlowSpeed();
			if(damage - defense > 0.5f)
				health = health - (damage - defense);
			else if(damage - defense <= 0.5f)
				health = health - 0.5f; 

			SoundManager.Effect_Missile_Freeze_ON();
			break;
		case "Splash":
			if(damage - defense > 0.5f)
				health = health - (damage - defense);
			else if(damage - defense <= 0.5f)
				health = health - 0.5f;
			SoundManager.Effect_Missile_Fire_ON();
			break;
		case "Multiplex":
			if(damage - defense > 0.5f)
				health = health - (damage - defense);
			else if(damage - defense <= 0.5f)
				health = health - 0.5f; 
			SoundManager.Effect_Missile_Laser_ON();
			break;
		}
	}

	void SlowSpeed(){
		speedTimer = 2.0f;
		moveSpeed = finalMoveSpeed * 0.75f;

	}
}
