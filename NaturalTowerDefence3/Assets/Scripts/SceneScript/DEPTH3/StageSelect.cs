using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour {
	
	public GameObject playScene;
	public GameObject Stage;

	
	public csStageDB stageDB;
	

	
	int mapNum = 1;
	public int stageNum = 0;
	
	
	// Use this for initialization
	void Start () {
		Stage = GameObject.Find("Stage");
		playScene = GameObject.Find("3.GameManager");
		stageDB = GameObject.Find("DB_STAGE_TEST").GetComponent<csStageDB>();
		
		stageNum = GetComponent<SetStage>().indexNumber;

	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnClick(){
		if(this.gameObject.transform.FindChild("1.Sprite (bg_stage)").gameObject.GetComponent<UISprite>().spriteName == "bg_stage") 
		{
			Stage.GetComponent<CreateStage>().SetScreen();
			PlayScene.state = PlayScene.STATE.INIT;
	
			playScene.GetComponent<PlayScene>().initInfo(mapNum, stageNum);
		}

	}
	
}
