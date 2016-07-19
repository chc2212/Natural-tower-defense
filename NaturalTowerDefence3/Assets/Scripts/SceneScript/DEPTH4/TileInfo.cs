using UnityEngine;
using System.Collections;

public class TileInfo : MonoBehaviour {
	
	public PlayScene PlaySceneObj;
	public Vector3 mousePos;
	
	public int tileInfo = 1; // 1 - green , 2 - red
	public bool tempFlag = false;
	public Transform towerInfo;
	
	RaycastHit hit;
	Ray ray;
	
	Collider[] tiles;

	public AudioScirpt SoundManager;
	// Use this for initialization
	void Start () {
		PlaySceneObj = GameObject.Find("3.GameManager").GetComponent<PlayScene>();
		//this.gameObject.GetComponent<tk2dSprite>().color = new Color(0f, 235f, 38f, 120f)/255f; // setting green
		SoundManager = GameObject.Find("4.Sound Manager").GetComponent<AudioScirpt>();
		tileInfo = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.state == PlayScene.STATE.INIT)
		{
			tileInfo = 1;
		} 
		
		
		if(tileInfo == 1){
			this.gameObject.GetComponent<tk2dSprite>().color = new Color(159f, 159f, 159f, 79f) / 255f; // color gray? // 

			tiles = Physics.OverlapSphere(this.gameObject.transform.position, 30f);
			
			int count = 0;
			//tempFlag = false;
			for(int i = 0; i < tiles.Length; i++)
			{
				if(tiles[i].gameObject.tag == "MONSTER")
				{
					count++;
					this.gameObject.GetComponent<tk2dSprite>().color = new Color(235f, 15f, 5f, 120f) / 255f; 

				}
				if(count > 0)
					tempFlag = true;
				else
					tempFlag = false;	
			}
		}
		else if(tileInfo == 2){
			this.gameObject.GetComponent<tk2dSprite>().color = new Color(235f, 15f, 5f, 120f) / 255f; // color red
		}
		
		if(towerInfo == null)
		{
			tileInfo = 1;
		} 

		if (Input.GetMouseButtonDown(0)){
			mousePos = Input.mousePosition;
		}
		if (Input.GetMouseButtonUp(0)){
			
			if(Input.mousePosition.x >= (mousePos.x - 10) && Input.mousePosition.x <= (mousePos.x + 10)
			&& Input.mousePosition.y >= (mousePos.y - 10) && Input.mousePosition.y <= (mousePos.y + 10)){ 
				
				ray = Camera.main.ScreenPointToRay(mousePos);

				if(Physics.Raycast(ray, out hit, Mathf.Infinity)) 
	  			{ 
					if(hit.collider.gameObject.tag == "Tile")
					{
					//Debug.Log(hit.collider.gameObject);
						if(PlayScene.state == PlayScene.STATE.TOWER && hit.collider.gameObject.GetComponent<TileInfo>().tempFlag == false 
							&& hit.collider.gameObject.GetComponent<TileInfo>().tileInfo == 1) // color green?
						{
							//Debug.Log ("positon :"+hit.collider.gameObject.transform.position);
							GameObject Tower = Instantiate(Resources.Load ("Prefabs/1.TOWER/Tower"+(ConstructTower.TowerNum+1))) as GameObject;
							PlayScene.CurrentTowerCount++;
							PlaySceneObj.user.userInfo.inPlaySilver -= (int)PlaySceneObj.towerDB.towerList[ConstructTower.TowerNum].towerLevelList[0].needSilver;
							Tower.tag = "TOWER";
							Tower.name = "Tower"+ConstructTower.TowerNum;
							Tower.gameObject.GetComponent<Tower_FIRE>().towerType = ConstructTower.TowerNum;
							Tower.gameObject.GetComponent<Tower_FIRE>().towerLevel = 0;
							Tower.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, 0.8f);
							Tower.transform.parent = PlaySceneObj.Tower.transform;

							hit.collider.gameObject.GetComponent<TileInfo>().towerInfo = Tower.transform;

							hit.collider.gameObject.GetComponent<TileInfo>().tileInfo = 2;
							PlayScene.state = PlayScene.STATE.IDLE;
							PlaySceneObj.FieldTileONOFF(false);

							SoundManager.Effect_TowerMake_ON();
						} // end of if state end color
					}
				} // end of if Raycast
			} // end of if MouseButtonUp and MousePosition
		}
	} // end of Update
	
	

}

