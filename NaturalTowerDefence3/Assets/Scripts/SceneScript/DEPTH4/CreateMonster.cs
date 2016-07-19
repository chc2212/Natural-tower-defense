using UnityEngine;
using System.Collections;

public class CreateMonster : MonoBehaviour {
	
	public GameObject Road1, Road2, Road3, Road4;
	
	public PlayScene pScene;
	public csMonsterDB monsterDB;
	
	public float WAITTIME = 5;
		
	public int createdMonster = 0;
	public int waveCount = 1;
	
	public float timer = 0; 
	public float sbTimer = 0;
	public float bTimer = 0; 
	
	public float alertTimer = 0;
	public float waitTimer = 0;


	// Use this for initialization
	void Start () {
		/*
		Road1 = GameObject.Find ("Road1");
		Road2 = GameObject.Find ("Road2");
		Road3 = GameObject.Find ("Road3");
		Road4 = GameObject.Find ("Road4");
		
		monsterDB = GameObject.Find("DB_Monster_TEST").GetComponent<csMonsterDB>();
		*/
	}
	
	// Update is called once per frame
	void Update () {
		
		if(PlayScene.createMonsterInitFlag == true)
		{
			createdMonster = 0;
			waveCount = 1;
			timer = 0;
			sbTimer = 0;
			bTimer = 0;
			waitTimer = 0; 
			alertTimer = 0;
			
			PlayScene.createMonsterInitFlag = false;
			PlayScene.EnterMonsterType = 1; 
			PlayScene.appearSemiBoss = false;
			PlayScene.appearBoss = false;
			PlayScene.alertSemi = false;
			PlayScene.alertBoss = false;
			
			timer = pScene.monsterTimer;

		} 
		
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			waitTimer += Time.deltaTime * PlayScene.playSpeed;
			if(waitTimer < WAITTIME)
				return; 
			else
			{
				PlayScene.waitFlag = true; 
				PlayScene.monsterAlertFlag = true; 
			}
	
			alertTimer += Time.deltaTime * PlayScene.playSpeed;
	
			if(waveCount == pScene.wave)
				PlayScene.monsterAlertFlag = false; 
			

			if(PlayScene.createMonsterFlag)
			{
				switch(PlayScene.EnterMonsterType)
				{
				case 1:
					if(pScene.semiBossNumber != 0 && waveCount == pScene.semiBossWave)
					{
						sbTimer += Time.deltaTime * PlayScene.playSpeed;
						PlayScene.alertSemi = true;
					}
					if(waveCount == pScene.bossWave)
					{
						bTimer += Time.deltaTime * PlayScene.playSpeed;
						PlayScene.alertBoss = true;
					}
						
					timer += Time.deltaTime * PlayScene.playSpeed;
					
					if(timer < pScene.monsterTimer)
						return; 
					
					GameObject Monster = Instantiate(Resources.Load("Prefabs/2.Monster/STAGE"+pScene.mapNumber+"-"+pScene.stageNumber+"/pfMonster")) as GameObject;
					PlayScene.CurrentMonsterCount++;
					setMonster("MONSTER", "monster", Monster, 0);
					timer = 0;
					
					if(createdMonster == pScene.monNumPerWave)
					{
						PlayScene.createMonsterFlag = false;
						createdMonster = 0;
					}
					break;
				case 2:
					sbTimer += Time.deltaTime * PlayScene.playSpeed;
					
					if(sbTimer < pScene.semiBossTimer + pScene.monNumPerWave * pScene.monsterTimer)
						return;
					for(int i = 0; i < pScene.semiBossNumber; i++)
					{
						GameObject SemiBoss = Instantiate(Resources.Load("Prefabs/2.Monster/STAGE"+pScene.mapNumber+"-"+pScene.stageNumber+"/pfSemiBoss")) as GameObject;
						PlayScene.CurrentMonsterCount++;
						
						int r = 0;
						
						switch(i)
						{
						case 0:
							r = 1;
							break;
						case 1:
							r = 2;
							break;
						case 2:
							r = 0;
							break;
						case 3:
							r = 3;
							break;
						}
						setMonster("MONSTER", "SemiBoss", SemiBoss, r);
					}
					PlayScene.createMonsterFlag = false;
					PlayScene.EnterMonsterType = 1;
					PlayScene.alertSemi = false;
					PlayScene.appearSemiBoss = true;
					alertTimer = 0;
					createdMonster = 0;
					
					break;
				case 3:
					bTimer += Time.deltaTime * PlayScene.playSpeed;
					
					if(bTimer < pScene.bossTimer + pScene.monNumPerWave * pScene.monsterTimer)
						return;
					
					GameObject Boss = Instantiate(Resources.Load("Prefabs/2.Monster/STAGE"+pScene.mapNumber+"-"+pScene.stageNumber+"/pfBoss")) as GameObject;
					PlayScene.CurrentMonsterCount++;
					setMonster("MONSTER", "Boss", Boss, 1);
					
					PlayScene.createMonsterFlag = false;
					PlayScene.alertBoss = false;
					PlayScene.appearBoss = true;
					createdMonster = 0;
					
					break;
				}
			}
			
			if(pScene.semiBossNumber != 0 && PlayScene.appearSemiBoss == false && waveCount == pScene.semiBossWave && PlayScene.createMonsterFlag == false)
			{
				PlayScene.EnterMonsterType = 2;
				PlayScene.createMonsterFlag = true;
				//PlayScene.alertSemi = true;
			}
			if(PlayScene.appearBoss == false && waveCount == pScene.bossWave && PlayScene.createMonsterFlag == false)
			{
				PlayScene.EnterMonsterType = 3;
				PlayScene.createMonsterFlag = true;
				PlayScene.alertBoss = true;
			}
			
			if(waveCount < pScene.wave && PlayScene.createMonsterFlag == false)
			{
				if(alertTimer > pScene.waveTimer + (pScene.monNumPerWave * pScene.monsterTimer))
				{
					alertTimer = 0;
					waveCount++;
					PlayScene.createMonsterFlag = true;
					
					timer = pScene.monsterTimer;
				}
			} 			
		}
		
		if(waveCount == pScene.wave && PlayScene.CurrentMonsterCount == 0 && PlayScene.appearBoss == true)
		{
			PlayScene.state = PlayScene.STATE.GAMEFINISH;
			waveCount = 0;
		} 
		
	} 
	
	void setMonster(string tag, string name, GameObject Monster, int position)
	{			
		Monster.tag = tag;
		Monster.name = name;

		Monster.GetComponent<Monster>().health = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].health;
		Monster.GetComponent<Monster>().defense = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].defense;
		Monster.GetComponent<Monster>().attackDamage = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].attackDamage;
		Monster.GetComponent<Monster>().attackRange = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].attackRange;
		Monster.GetComponent<Monster>().detectRange = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].detectRange;
		Monster.GetComponent<Monster>().attackSpeed = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].attackSpeed;
		Monster.GetComponent<Monster>().moveSpeed = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].moveSpeed;
		Monster.GetComponent<Monster>().monsterNumber = PlayScene.EnterMonsterType-1;	
		Monster.GetComponent<Monster>().moveImageNumber = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].moveImageNumber;
		Monster.GetComponent<Monster>().attackImageNumber = monsterDB.stageList[pScene.stageNumber-1].monsterList[PlayScene.EnterMonsterType-1].attackImageNumber;

		if(PlayScene.EnterMonsterType == 1)
		{
			int r = Random.Range(0, 4);
			setPosition(Monster, r);
		}
		else if(PlayScene.EnterMonsterType == 2 || PlayScene.EnterMonsterType == 3)
		{
			setPosition(Monster, position);
		}
	}
	
	void setPosition(GameObject Monster, int position)
	{
		

		Vector3 fixedRoadPoint1 = new Vector3(Road1.gameObject.transform.position.x - (Road1.gameObject.GetComponent<BoxCollider>().size.x / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.x / 2,
			Road1.gameObject.transform.position.y - (Road1.gameObject.GetComponent<BoxCollider>().size.y / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.y / 2,
			Road1.gameObject.transform.position.z);
		Vector3 fixedRoadPoint2 = new Vector3(Road2.gameObject.transform.position.x - (Road2.gameObject.GetComponent<BoxCollider>().size.x / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.x / 2,
			Road2.gameObject.transform.position.y - (Road2.gameObject.GetComponent<BoxCollider>().size.y / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.y / 2,
			Road2.gameObject.transform.position.z);
		Vector3 fixedRoadPoint3 = new Vector3(Road3.gameObject.transform.position.x - (Road3.gameObject.GetComponent<BoxCollider>().size.x / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.x / 2,
			Road3.gameObject.transform.position.y - (Road3.gameObject.GetComponent<BoxCollider>().size.y / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.y / 2,
			Road3.gameObject.transform.position.z);
		Vector3 fixedRoadPoint4 = new Vector3(Road4.gameObject.transform.position.x - (Road4.gameObject.GetComponent<BoxCollider>().size.x / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.x / 2,
			Road4.gameObject.transform.position.y - (Road4.gameObject.GetComponent<BoxCollider>().size.y / 2) + Monster.gameObject.GetComponent<BoxCollider>().size.y / 2,
			Road4.gameObject.transform.position.z);
		
		switch(position){
		case 0 :
			Monster.transform.position = fixedRoadPoint1;
			Monster.transform.parent = Road1.transform;
			createdMonster++;
			break;
		case 1 :
			Monster.transform.position = fixedRoadPoint2;
			Monster.transform.parent = Road2.transform;
			createdMonster++;
			break;
		case 2 :
			Monster.transform.position = fixedRoadPoint3;
			Monster.transform.parent = Road3.transform;
			createdMonster++;
			break;
		case 3 :
			Monster.transform.position = fixedRoadPoint4;
			Monster.transform.parent = Road4.transform;
			createdMonster++;
			break;	
		}
	}

}

