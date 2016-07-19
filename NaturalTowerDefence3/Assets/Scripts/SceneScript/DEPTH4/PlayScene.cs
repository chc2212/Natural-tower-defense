using UnityEngine;
using System.Collections;

public class PlayScene : MonoBehaviour {
	
	public enum STATE { BEFORE , INIT, IDLE, OPTION, TOWER, TOWERINFO, GAMEOVER, GAMEFINISH, PAUSE, RESTART, NEXTSTAGE, SPELL1, SPELL2, SHOWMAP};
	
	public GameObject tkCamera;
	public GameObject FieldTile;
	public GameObject Tower;
	public GameObject Monster;
	
	public GameObject gameOver;
	public GameObject gameOverMenu;
	public GameObject gameFinish;
	public GameObject gameFinishMenu;
	public GameObject MapShowBlur;

	public CreateMonster cm;
	
	public CastleInfo castle;
	
	public csUserSaveNLoad user;
	public csTowerDB towerDB;
	public csMonsterDB monsterDB;
	public csStageDB stageDB;
	
	public static STATE state = STATE.BEFORE;
	public static STATE beforeState = STATE.BEFORE;
	
	public int mapNumber;
	public int stageNumber;
	public int wave;
	public int monNumPerWave;
	public float monsterTimer;
	public int waveTimer; 
	public int semiBossNumber;
	public int semiBossWave; 
	public int semiBossTimer;
	public int bossNumber;
	public int bossWave;
	public int bossTimer;	

	public static int PlayCount = 0;
	public static int getItemCount = 0;
	
	public static int CurrentMonsterCount = 0;
	public static int CurrentTowerCount = 0;
	public static int CurrentMissileCount = 0;
	
	public static int playSpeed = 1;
	public static bool createMonsterFlag = false;
	public static bool createMonsterInitFlag = false;
	public static bool monsterAlertFlag = false;
	public static bool waitFlag = false;
	public static int EnterMonsterType = 0; // 1 - wave , 2 - semiboss, 3 - boss
	public static bool appearSemiBoss = false;
	public static bool alertSemi = false;
	public static bool appearBoss = false;
	public static bool alertBoss = false;
	
	public static float incomeGold = 0;

	
	Touch touchB;
	Vector2 touchPosB, touchPosE;
	float testB, testE;
	
	private int testMoveCount = 0;
	
	public static float gameTimer = 0;
	float gameOverTimer = 0;
	float gameClearTimer = 0;
	float mapShowTimer = 0;
	float mapShowTime = 3;

	public AudioScirpt SoundManager;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(state == STATE.IDLE)
		{
			gameTimer += Time.deltaTime * playSpeed;
		}
		switch(state){
		case STATE.BEFORE:
			break;
		case STATE.INIT :
			testMoveCount = 0;
			break;
		case STATE.IDLE:
			MapShowBlur.SetActive(false);
			Main.currentSceneNumber = 3;
			Main.currentGameStage = stageNumber; // Sound Setting
			TestMove();
			break;
		case STATE.TOWER:
			TestMove(); // UNITY TOUCH
			break;
		case STATE.TOWERINFO:
			break;
		case STATE.OPTION:
			break;
		case STATE.SHOWMAP:
			MapShowBlur.SetActive(true);
			Main.currentSceneNumber = 3;
			Main.currentGameStage = stageNumber; // Sound Setting

			mapShowTimer += Time.deltaTime;
			if(mapShowTimer <= mapShowTime / 2)
				tkCamera.transform.position = new Vector3((mapShowTimer / mapShowTime) * 1320, 0, 0);
			else if(mapShowTimer >= mapShowTime / 2)
				tkCamera.transform.position = new Vector3(1320 - (mapShowTimer / mapShowTime) * 1320, 0, 0);

			if(mapShowTimer >= mapShowTime)
			{
				MapShowBlur.SetActive(false);
				state = STATE.IDLE;
				mapShowTimer = 0;
			}
			break;
		case STATE.GAMEOVER:
			Main.currentGameStage = 0;
			SoundManager.BGM_DEFEAT_ON(); 

			gameOverTimer += Time.deltaTime;
			gameOver.SetActive(true);
			if(gameOverTimer >= 1.5f)
			{
				gameOverMenu.SetActive(true);
				gameOverTimer = 0;
				state = STATE.BEFORE;
			}

			break;
		case STATE.GAMEFINISH:
			Main.currentGameStage = 0; 
			SoundManager.BGM_WIN_ON(); 
			gameClearTimer += Time.deltaTime;
			
			gameFinish.SetActive(true);
			
			if(gameClearTimer >= 1.5f)
			{
				incomeGold = user.userInfo.inPlaySilver / 100;
				user.userInfo.gold += (int)incomeGold;
				gameFinishMenu.SetActive(true);
				gameClearTimer = 0;
				
				state = STATE.BEFORE;
				if(stageNumber < stageDB.mapList[mapNumber-1].stageList.Count) 
				{
					if(stageNumber > user.userInfo.maxClearStage)
						user.userInfo.maxClearStage = stageNumber;
				}
				if(user.userInfo.stageClearInfo[stageNumber-1] == 0)	user.userInfo.stat.remainPoint++;
				user.userInfo.stageClearInfo[stageNumber-1] += 1;
			

				user.userInfo.achieveInfo.stageClearCount++;
				user.SaveTest();
			}
			break;
		case STATE.PAUSE:
			break;
		case STATE.RESTART:
			state = STATE.INIT;
			break;
		case STATE.NEXTSTAGE:		
			initInfo(mapNumber, stageNumber + 1);
			state = PlayScene.STATE.INIT;
			break;
		}		
	}
		
	void TestMove(){

		if(Input.GetMouseButton(0))
		{
			if(Input.mousePosition.y < 175 || Input.mousePosition.y > 645) 
				return;  
			
			//Debug.Log (Input.mousePosition.y);
			testB = Input.mousePosition.x;
			testMoveCount++;
		}
		if(testMoveCount == 1)
		{
			testE = testB;
		}
		if(Input.GetMouseButtonUp(0))
		{
			//testE = Input.mousePosition.x;
			testMoveCount = 2;
		}
	
		
		if(testMoveCount == 2)
		{
			//Debug.Log ("test B: "+testB+", testE: "+testE);
			testMoveCount = 0;
			//if(Mathf.Abs(testE - testB) >= 20)
			//{
				tkCamera.transform.position = new Vector3(tkCamera.transform.position.x + (testE * 2) - (testB * 2), 0, 0);
			//}
			if(tkCamera.transform.position.x < 0)
				tkCamera.transform.position = new Vector3(0, 0, 0);
			if(tkCamera.transform.position.x > 660)
				tkCamera.transform.position = new Vector3(659, 0, 0);
		}		
    }

	public void FieldTileONOFF(bool flag){
		FieldTile.SetActive(flag);
	}
	
	public void MonsterDead(int monNum)
	{
		user.userInfo.inPlaySilver += (int) monsterDB.stageList[stageNumber-1].monsterList[monNum].giveSilver;
	}
	
	public void initInfo(int mapNum, int stageNum){
		stageNumber = stageNum;
		
		wave = stageDB.mapList[mapNum-1].stageList[stageNum-1].setWave;
		monNumPerWave = stageDB.mapList[mapNum-1].stageList[stageNum-1].setMonsterNumberPerWave;
		monsterTimer = stageDB.mapList[mapNum-1].stageList[stageNum-1].monsterTimer;
		waveTimer = stageDB.mapList[mapNum-1].stageList[stageNum-1].waveTimer;
		semiBossNumber = stageDB.mapList[mapNum-1].stageList[stageNum-1].semiBossNumber;
		semiBossWave = stageDB.mapList[mapNum-1].stageList[stageNum-1].semiBossAfterWave;
		semiBossTimer = stageDB.mapList[mapNum-1].stageList[stageNum-1].semiBossTimer;
		bossNumber = stageDB.mapList[mapNum-1].stageList[stageNum-1].bossNumber;
		bossWave = stageDB.mapList[mapNum-1].stageList[stageNum-1].bossAfterWave;
		bossTimer = stageDB.mapList[mapNum-1].stageList[stageNum-1].bossTimer;
	}
	
	IEnumerator Wait (float waittime) 
	{
		yield return new WaitForSeconds(waittime);
	}
	

}

