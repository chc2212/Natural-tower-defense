using UnityEngine;
using System.Collections;

public class InitStage : MonoBehaviour {
	
	public csCastleDB castleDB;
	public csUserSaveNLoad user;
	
	public GameObject castle;
	public PlayScene PlaySceneObj;
	
	int countX = 10; 
	int countY = 4; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(PlayScene.state == PlayScene.STATE.INIT && PlayScene.PlayCount != 0)
		{
			PlaySceneObj.FieldTileONOFF(true); 
			
			for(int i = 0; i < countX * countY; i++)
			{
				GameObject gameObj = GameObject.Find ("FieldTile"+i);
				Destroy(gameObj);
			} // delete tile
			
			GameObject[] towerObj = GameObject.FindGameObjectsWithTag("TOWER");
			GameObject[] missileObj = GameObject.FindGameObjectsWithTag("MISSILE");
			GameObject[] monsterObj = GameObject.FindGameObjectsWithTag("MONSTER");
			for(int i = 0; i < towerObj.Length; i++)
			{
				Destroy(towerObj[i]);
			}
			PlayScene.CurrentTowerCount = 0;
			
			for(int i = 0; i < missileObj.Length; i++)
			{
				Destroy(missileObj[i]);
			}
			PlayScene.CurrentMissileCount = 0;
			
			for(int i = 0; i < monsterObj.Length; i++)
			{
				Destroy(monsterObj[i]);
			}
			PlayScene.CurrentMonsterCount = 0;
		
			
			for(int i = 0; i < castle.gameObject.transform.childCount; i++)
			{
				Destroy(castle.gameObject.transform.GetChild(i).gameObject);
			} 
			
			PlayScene.playSpeed = 1; 
			
			PlayScene.PlayCount = 0; 
			PlayScene.incomeGold = 0;
		}
		
		if(PlayScene.state == PlayScene.STATE.INIT)
		{
			castle.SetActive(true);
			castle.GetComponent<CastleInfo>().health = castleDB.castleHealth[user.userInfo.castle.currentHealthLevel];
			castle.GetComponent<CastleInfo>().maxHealth = castle.GetComponent<CastleInfo>().health;
			castle.GetComponent<CastleInfo>().defense = castleDB.castleDefense[user.userInfo.castle.currentDefenseLevel];
			castle.GetComponent<CastleInfo>().turretAttackDamage = castleDB.turretAttackDamage[user.userInfo.castle.currentADLevel];
			castle.GetComponent<CastleInfo>().turretAttackRange = castleDB.turretAttackRange;
			castle.GetComponent<CastleInfo>().turretAttackSpeed = castleDB.turretAttackSpeed;
			castle.GetComponent<CastleInfo>().turretMissileSpeed = castleDB.turretMissileSpeed;
			
			for(int i = 0; i < 4; i++)
			{
				GameObject castlePoint = Instantiate(Resources.Load ("Prefabs/pfCastlePoint")) as GameObject;
				//castlePoint.transform.parent = castle.transform;
				castlePoint.transform.position = new Vector3(1610 + (i * 20), 515 - (i * 95), 0.9f);
				castlePoint.transform.localScale = new Vector3(1f, 1f, 1f);
				castlePoint.transform.parent = castle.transform;
				castlePoint.name = "point"+(i+1);
				castlePoint.tag = "CASTLE";
			}
			
			for(int i = 0; i < 4; i++)
			{
				GameObject TowerShadow = Instantiate(Resources.Load ("Prefabs/pfShadowTower")) as GameObject;
				
				switch(i){
				case 0:
					TowerShadow.transform.position = new Vector3(1695, 420, 0.8f);
					break;
				case 1:
					TowerShadow.transform.position = new Vector3(1740, 315, 0.8f);
					break;
				case 2:
					TowerShadow.transform.position = new Vector3(1660, 510, 0.8f);
					break;
				case 3:
					TowerShadow.transform.position = new Vector3(1777, 224, 0.8f);
					break;
				}
				
				TowerShadow.transform.parent = castle.transform;
				TowerShadow.name = "TowerShadow"+(i+1);
			}	
			
			for(int i = 0; i < user.userInfo.castle.currentSlot; i++)
			{
				GameObject castleTowerPoint = Instantiate(Resources.Load ("Prefabs/pfcastleTower")) as GameObject;
				
				switch(i){
				case 0:
					castleTowerPoint.transform.position = new Vector3(1695, 440, 0.8f);
					break;
				case 1:
					castleTowerPoint.transform.position = new Vector3(1740, 335, 0.8f);
					break;
				case 2:
					castleTowerPoint.transform.position = new Vector3(1660, 530, 0.8f);
					break;
				case 3:
					castleTowerPoint.transform.position = new Vector3(1777, 244, 0.8f);
					break;
				}
				
				castleTowerPoint.transform.parent = castle.transform;
				castleTowerPoint.name = "CastleTower"+(i+1);
			}

			
			
			///////////////
			
			int col = 0 , row = 0;
			int x = 0, y = 0;
			
			for(int i = 0; i < countX * countY; i++){
				col = i % countX;
				row = i / countX;
				
				x = col * 155;
				y = row * 95;
				//gameObj = GameObject.Find ("FieldTile");
				GameObject tile = Instantiate(Resources.Load ("Prefabs/pfTileGreen")) as GameObject;
				tile.name = "FieldTile"+i;
				tile.transform.position = new Vector3(160+x, 515-y, 0.9f);
				tile.transform.parent = PlaySceneObj.FieldTile.transform;
				tile.GetComponent<BoxCollider>().size = new Vector3(150, 90, 0.5f);
				//tile.SetActive(false);
			}
			PlaySceneObj.FieldTileONOFF(false);
			///////////////
			
			PlayScene.createMonsterInitFlag = true;
			PlayScene.createMonsterFlag = true;
			PlayScene.monsterAlertFlag = false;
			PlayScene.waitFlag = false;
			
			PlaySceneObj.tkCamera.transform.position = new Vector3(0, 0, 0);
			
			user.userInfo.inPlaySilver = user.userInfo.silver; 
			if(user.userInfo.inPlaySilver == 0)
				user.userInfo.inPlaySilver = 90; 
			PlayScene.gameTimer = 0;
			PlayScene.getItemCount = 0; 
			
			Spell.spellTimer[0] = 100;
			Spell.spellTimer[1] = 100;

			if(PlaySceneObj.stageNumber == 1)
				PlayScene.state =  PlayScene.STATE.SHOWMAP;
			else
				PlayScene.state =  PlayScene.STATE.IDLE;
			PlayScene.PlayCount++;
		}
	}	
	
}
