using UnityEngine;
using System.Collections;

public class CreateStage : MonoBehaviour {
	
	
	public GameObject Stage;
	public GameObject GameScene;
	public GameObject InventoryScene;
	public GameObject StageSelect;

	
	public csUserSaveNLoad user;
	
	public bool createStageFlag = false;
		
	int countX = 6;
	int countY = 3;
	int col = 0 , row = 0;
	int xSize = 90, ySize = 105;
	int xStart = -220, yStart = 90;
	
	public static int maxClearStage = 0;
	public static int[] stageClearInfo = new int[18];
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		maxClearStage = user.userInfo.maxClearStage;
		stageClearInfo = user.userInfo.stageClearInfo;
		
		if(createStageFlag == false)
		{
			
			createStageFlag = true;
		
			int x = 0, y = 0;
			
			for(int i = 0; i < countX * countY; i++)
			{
				col = i % countX;
				row = i / countX;
				
				x = col * xSize;
				y = row * ySize;
				
				GameObject StageLevel =  Instantiate(Resources.Load ("Prefabs/pfStage")) as GameObject;
				
				StageLevel.name = "Stage"+(i+1);
				StageLevel.transform.parent = Stage.transform;
				StageLevel.transform.localPosition = new Vector3(xStart+x, yStart-y, 0);
				StageLevel.transform.localScale = new Vector3(1, 1, 1);
				
				StageLevel.GetComponent<SetStage>().indexNumber = i+1;
				StageLevel.GetComponent<SetStage>().star = stageClearInfo[i];
				
			} 
		}
	}
	
	public void SetScreen()
	{
		GameScene.SetActive(true);
		InventoryScene.SetActive(false);
		StageSelect.SetActive(false);
	}
}
