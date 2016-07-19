using UnityEngine;
using System.Collections;

public class csUserSaveNLoad : MonoBehaviour {

	public csUserInfo userInfo = new csUserInfo();
	
	public static bool loadFlag = false;
	public static bool pictureFlag = false;
	public static bool gameStartFlag = false;
	
	// Use this for initialization
	void Start () {
		
	    // this array should be filled before you can use EncryptedPlayerPrefs :
	
	    EncryptedPlayerPrefs.keys=new string[5];
	
	    EncryptedPlayerPrefs.keys[0]="23Wrudre";
	    EncryptedPlayerPrefs.keys[1]="SP9DupHa";
	    EncryptedPlayerPrefs.keys[2]="frA5rAS3";
	    EncryptedPlayerPrefs.keys[3]="tHat2epr";
	    EncryptedPlayerPrefs.keys[4]="jaw3eDAs";
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(loadFlag)
		{
	
			LoadTest (); 
			loadFlag = false;
			pictureFlag = true;
			
			gameStartFlag = true;
		}
		
	}
	
	public void SaveTest(){
		EncryptedPlayerPrefs.SetInt("DataExist", 1);
		EncryptedPlayerPrefs.SetInt("Strength", userInfo.stat.strength);
		EncryptedPlayerPrefs.SetInt("Agility", userInfo.stat.agility);
		EncryptedPlayerPrefs.SetInt("Constitution", userInfo.stat.constitution);
		EncryptedPlayerPrefs.SetInt("Intelligence", userInfo.stat.intelligence);
		EncryptedPlayerPrefs.SetInt("RemainPoint", userInfo.stat.remainPoint);
		
		EncryptedPlayerPrefs.SetInt("CurrentADLevel", userInfo.castle.currentADLevel);
		EncryptedPlayerPrefs.SetInt("CurrentHealthLevel", userInfo.castle.currentHealthLevel);
		EncryptedPlayerPrefs.SetInt("CurrentDefenseLevel", userInfo.castle.currentDefenseLevel);
		EncryptedPlayerPrefs.SetInt("CurrentSlot", userInfo.castle.currentSlot);
		
		EncryptedPlayerPrefs.SetInt("Gold", userInfo.gold);
		EncryptedPlayerPrefs.SetInt("Silver", userInfo.silver);

	
		
		for(int i =0; i<5; i++)
		{
			EncryptedPlayerPrefs.SetInt("TowerType"+i, userInfo.tower.currentTowerLevel[i]);
		}
		
		EncryptedPlayerPrefs.SetInt ("MaxClearStage", userInfo.maxClearStage);
		for(int i = 0; i < userInfo.stageClearInfo.Length; i++)
		{
			EncryptedPlayerPrefs.SetInt ("StageClearInfo"+i, userInfo.stageClearInfo[i]);
		}
		
		EncryptedPlayerPrefs.SetString("PicturePath", userInfo.picturePath);

		EncryptedPlayerPrefs.SetInt ("killEnemyCount", userInfo.achieveInfo.killEnemyCount);
		EncryptedPlayerPrefs.SetInt ("stageClearCount", userInfo.achieveInfo.stageClearCount);
	}
	
	public void LoadTest(){
		userInfo.stat.strength = EncryptedPlayerPrefs.GetInt("Strength", 0);
		userInfo.stat.agility = EncryptedPlayerPrefs.GetInt("Agility", 0);
		userInfo.stat.constitution = EncryptedPlayerPrefs.GetInt("Constitution", 0);
		userInfo.stat.intelligence = EncryptedPlayerPrefs.GetInt("Intelligence", 0);
		userInfo.stat.remainPoint = EncryptedPlayerPrefs.GetInt ("RemainPoint", 3);
	
		userInfo.castle.currentADLevel = EncryptedPlayerPrefs.GetInt("CurrentADLevel", 0);
		userInfo.castle.currentHealthLevel = EncryptedPlayerPrefs.GetInt("CurrentHealthLevel", 0);
		userInfo.castle.currentDefenseLevel = EncryptedPlayerPrefs.GetInt("CurrentDefenseLevel", 0);
		userInfo.castle.currentSlot = EncryptedPlayerPrefs.GetInt("CurrentSlot", 1);

			
		userInfo.gold = EncryptedPlayerPrefs.GetInt("Gold", 20);
		userInfo.silver = EncryptedPlayerPrefs.GetInt("Silver", 0);

		
		for(int i=0; i<5; i++)
		{
			userInfo.tower.currentTowerLevel[i] = EncryptedPlayerPrefs.GetInt("TowerType"+i, 0);
		}
		
		
		userInfo.maxClearStage = EncryptedPlayerPrefs.GetInt ("MaxClearStage", 0);
		for(int i = 0; i < userInfo.stageClearInfo.Length; i++)
		{
			userInfo.stageClearInfo[i] = EncryptedPlayerPrefs.GetInt ("StageClearInfo"+i, 0);
		}
		userInfo.picturePath = EncryptedPlayerPrefs.GetString("PicturePath", "");

		userInfo.achieveInfo.killEnemyCount = EncryptedPlayerPrefs.GetInt("killEnemyCount", 0);
		userInfo.achieveInfo.stageClearCount = EncryptedPlayerPrefs.GetInt("stageClearCount", 0);


	}
	
	public void InitTest(){
		EncryptedPlayerPrefs.DeleteKey("DataExist");
		EncryptedPlayerPrefs.DeleteKey("Strength");
		EncryptedPlayerPrefs.DeleteKey("Agility");
		EncryptedPlayerPrefs.DeleteKey("Constitution");
		EncryptedPlayerPrefs.DeleteKey("Intelligence");
		EncryptedPlayerPrefs.DeleteKey("RemainPoint");
		EncryptedPlayerPrefs.DeleteKey("CurrentADLevel");
		EncryptedPlayerPrefs.DeleteKey("CurrentHealthLevel");
		EncryptedPlayerPrefs.DeleteKey("CurrentDefenseLevel");
		EncryptedPlayerPrefs.DeleteKey("CurrentSlot");
		EncryptedPlayerPrefs.DeleteKey("Gold");
		EncryptedPlayerPrefs.DeleteKey("Silver");
		
		for(int i=0; i<15; i++)
		{
			EncryptedPlayerPrefs.DeleteKey("ItemSlot"+i);
		}
		
		for(int i=0; i<7; i++)
		{
			EncryptedPlayerPrefs.DeleteKey("EquipSlot"+i);
		}
		
		for(int i=0; i<5; i++)
		{
			EncryptedPlayerPrefs.DeleteKey("TowerType"+i);
		}
		
		EncryptedPlayerPrefs.DeleteKey("MaxClearStage");
		for(int i = 0; i < userInfo.stageClearInfo.Length; i++)
		{
			EncryptedPlayerPrefs.DeleteKey("StageClearInfo"+i);
		}
		
		EncryptedPlayerPrefs.DeleteKey("PicturePath");
				
		
		EncryptedPlayerPrefs.SetString("EquipSlot"+0, "JACK1");
		EncryptedPlayerPrefs.SetString("ItemSlot"+0, "HAND1-1");

		EncryptedPlayerPrefs.DeleteKey("killEnemyCount");
		EncryptedPlayerPrefs.DeleteKey("stageClearCount");

	}
}
