using UnityEngine;
using System.Collections;

public class ConstructTower : MonoBehaviour {
	
	public PlayScene PlaySceneObj;
	
	public UISprite B_Arrow;
	public UISprite N_Arrow;
	
	public UISprite sprite1;
	public UISprite sprite2;
	public UISprite sprite3;
	public UISprite sprite4;
	
	public UILabel label1;
	public UILabel label2;
	public UILabel label3;
	public UILabel label4;
	
	public static int pageNum = 0;
	
	public static int TowerNum = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.name == "8.Tower1") // update
		{
			sprite1.spriteName = "tower"+(pageNum+1);
			sprite2.spriteName = "tower"+(pageNum+2);
			sprite3.spriteName = "tower"+(pageNum+3);
			sprite4.spriteName = "tower"+(pageNum+4);
			
			label1.text = PlaySceneObj.towerDB.towerList[pageNum+0].towerLevelList[0].needSilver.ToString();
			label2.text = PlaySceneObj.towerDB.towerList[pageNum+1].towerLevelList[0].needSilver.ToString();
			label3.text = PlaySceneObj.towerDB.towerList[pageNum+2].towerLevelList[0].needSilver.ToString();
			label4.text = PlaySceneObj.towerDB.towerList[pageNum+3].towerLevelList[0].needSilver.ToString();
		}
	}
	
	void OnClick(){
		switch(this.gameObject.name){
		case "7.Back Arrow":
			if(pageNum > 0)
				pageNum -= 1;
			break;
		case "12.Next Arrow":
			if(pageNum < 1)
				pageNum += 1;
			break;
		case "8.Tower1":
			TowerNum = pageNum + 0;
			if(PlaySceneObj.user.userInfo.inPlaySilver >= PlaySceneObj.towerDB.towerList[TowerNum].towerLevelList[0].needSilver)
				MakeTile(1+pageNum);
			break;
		case "9.Tower2":
			TowerNum = pageNum + 1;
			if(PlaySceneObj.user.userInfo.inPlaySilver >= PlaySceneObj.towerDB.towerList[TowerNum].towerLevelList[0].needSilver)
				MakeTile(2+pageNum); 
			break;
		case "10.Tower3":
			TowerNum = pageNum + 2;
			if(PlaySceneObj.user.userInfo.inPlaySilver >= PlaySceneObj.towerDB.towerList[TowerNum].towerLevelList[0].needSilver)
				MakeTile(3+pageNum); 
			break;
		case "11.Tower4":
			TowerNum = pageNum + 3;
			if(PlaySceneObj.user.userInfo.inPlaySilver >= PlaySceneObj.towerDB.towerList[TowerNum].towerLevelList[0].needSilver)
				MakeTile(4+pageNum); 
			break;
		}
	}
	
	void MakeTile(int towerNumber){
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			PlayScene.state = PlayScene.STATE.TOWER;
			PlaySceneObj.FieldTileONOFF(true);
		}
		else if(PlayScene.state == PlayScene.STATE.TOWER)
		{
			PlayScene.state = PlayScene.STATE.IDLE;
			PlaySceneObj.FieldTileONOFF(false);
		}
	}
}
