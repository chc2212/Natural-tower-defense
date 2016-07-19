using UnityEngine;
using System.Collections;

public class GameFinish : MonoBehaviour {

	public GameObject GameFinishWindow;
	public GameObject GameFinishMenuWindow;

	public GameObject InventoryScene;
	public GameObject GameScene;
	public GameObject CharacterScene;

	public GameObject[] Star;
	public CastleInfo castle;

	public UILabel PlayTime;
	public UILabel IncomeGold;
	
	public csStageDB stageDB;
	public GameObject playScene;
	public GameObject main;

	int mapNum = 0;
	int stageNum = 0;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		IncomeGold.text = PlayScene.incomeGold.ToString("N0");
		
		if(PlayScene.gameTimer % 60 < 10 && (int)PlayScene.gameTimer / 60 < 10)
		{
			PlayTime.text = "0"+((int)PlayScene.gameTimer / 60).ToString("N0")+":0"+((int)PlayScene.gameTimer % 60).ToString("N0");
		}
		
		else if(PlayScene.gameTimer % 60 < 10 && (int)PlayScene.gameTimer / 60 > 10)
		{
			PlayTime.text = ((int)PlayScene.gameTimer / 60).ToString("N0")+":0"+((int)PlayScene.gameTimer % 60).ToString("N0");
		}
		else if(PlayScene.gameTimer % 60 > 10 && (int)PlayScene.gameTimer / 60 < 10)
		{
			PlayTime.text = "0"+((int)PlayScene.gameTimer / 60).ToString("N0")+":"+((int)PlayScene.gameTimer % 60).ToString("N0");
		}
		else
			PlayTime.text = ((int)PlayScene.gameTimer / 60).ToString("N0")+":"+((int)PlayScene.gameTimer % 60).ToString("N0");


		if(castle.health / castle.maxHealth >= 0.95)
		{
			for(int i = 0; i < Star.Length; i++)
			{
				Star[i].SetActive(true);
			}
		}
		else if(castle.health / castle.maxHealth >= 0.7)
		{
			for(int i = 0; i < Star.Length; i++)
			{
				Star[i].SetActive(true);
				if(i > 1)
					Star[i].SetActive(false);
			}
		}
		else
		{
			for(int i = 0; i < Star.Length; i++)
			{
				Star[i].SetActive(true);
				if(i > 0)
					Star[i].SetActive(false);
			}
		}
	}
	
	void OnClick(){
		switch(this.gameObject.name)
		{
		case "1.Home button":
			Main.currentSceneNumber = 1; 
			PlayScene.state = PlayScene.STATE.BEFORE;
			//InventoryScene.SetActive(true);
			//CharacterScene.SetActive(true);
			
			GameScene.SetActive(false);
			GameFinishWindow.SetActive(false);
			GameFinishMenuWindow.SetActive(false);
		
			main.SetActive(true);	

			break;

		case "3.Refresh Button":
			GameFinishWindow.SetActive(false);
			GameFinishMenuWindow.SetActive(false);
			PlayScene.state = PlayScene.STATE.INIT;
			break;
		case "2.Play Button":
			mapNum = playScene.GetComponent<PlayScene>().mapNumber;
			stageNum = playScene.GetComponent<PlayScene>().stageNumber;
			
			if(stageNum + 1 > stageDB.mapList[mapNum-1].stageList.Count)
			{

				break;
			}
			PlayScene.state = PlayScene.STATE.NEXTSTAGE;
			GameFinishWindow.SetActive(false);
			GameFinishMenuWindow.SetActive(false);
			break;
		}
	}

}
