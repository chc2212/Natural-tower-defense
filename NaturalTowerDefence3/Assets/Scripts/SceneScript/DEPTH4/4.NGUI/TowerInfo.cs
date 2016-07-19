using UnityEngine;
using System.Collections;

public class TowerInfo : MonoBehaviour {
	
	public GameObject towerInfo;
	public GameObject towerInfoMax;
	public csTowerDB towerDB;
	public csUserSaveNLoad user;
	
	public GameObject gameManager;
	
	//
	public GameObject tower;
	public UISprite TowerSprite;
	
	public UILabel health;
	public UILabel attackDamage;
	public UILabel attackSpeed;
	public UILabel attackRange;
	public UILabel defense;
	
	public UILabel nextHealth;
	public UILabel nextAttackDamage;
	public UILabel nextAttackSpeed;
	public UILabel nextAttackRange;
	public UILabel nextDefense;

	public UILabel upgrade;
	//
	
	public UISprite TowerSpriteMax;
	
	public UILabel healthMax;
	public UILabel attackDamageMax;
	public UILabel attackSpeedMax;
	public UILabel attackRangeMax;
	public UILabel defenseMax;
		
	
	int towerType;
	int towerLevel;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void UpgradeButton(){
		if(user.userInfo.tower.currentTowerLevel[towerType] <= towerLevel) 
			return;

		if(user.userInfo.inPlaySilver >= (int)towerDB.towerList[towerType].towerLevelList[towerLevel+1].needSilver)
		{
			user.userInfo.inPlaySilver = user.userInfo.inPlaySilver - (int)towerDB.towerList[towerType].towerLevelList[towerLevel+1].needSilver;
			tower.GetComponent<Tower_FIRE>().towerLevel++; 
			tower.GetComponent<Tower_FIRE>().tState = Tower_FIRE.TSTATE.WAIT; 
			tower.GetComponent<Tower_FIRE>().SetInfo();
			towerLevel += 1;
	
		}	
	}
	
	void XButton(){
		towerInfo.SetActive(false);
		towerInfoMax.SetActive(false);
		PlayScene.state = PlayScene.STATE.IDLE;
	}
	
	
	public void SetUpgradeInfo(int type, int level, GameObject tempTower){
		tower = tempTower;
		towerType = type;
		towerLevel = level;
		
		if(user.userInfo.tower.currentTowerLevel[towerType] <= towerLevel)
		{
			towerInfo.SetActive(false);
			towerInfoMax.SetActive(true);
			TowerSpriteMax.spriteName = "tower"+(type + 1);
			
			healthMax.text = tower.GetComponent<Tower_FIRE>().maxHealth.ToString("N0")+"("+tower.GetComponent<Tower_FIRE>().health.ToString("N0")+"+"
				+(tower.GetComponent<Tower_FIRE>().maxHealth - tower.GetComponent<Tower_FIRE>().health).ToString("N0")+")";

			defenseMax.text = tower.GetComponent<Tower_FIRE>().defense.ToString("N0");

			attackDamageMax.text = tower.GetComponent<Tower_FIRE>().finalDamage.ToString("N0")+"("+tower.GetComponent<Tower_FIRE>().attackDamage.ToString("N0")+"+"
				+(tower.GetComponent<Tower_FIRE>().finalDamage - tower.GetComponent<Tower_FIRE>().attackDamage).ToString("N0")+")";

			attackSpeedMax.text = tower.GetComponent<Tower_FIRE>().finalSpeed.ToString("N2")+"("+tower.GetComponent<Tower_FIRE>().attackSpeed.ToString("N2")+"-"
				+(tower.GetComponent<Tower_FIRE>().attackSpeed - tower.GetComponent<Tower_FIRE>().finalSpeed).ToString("N2")+")";

			attackRangeMax.text = tower.GetComponent<Tower_FIRE>().attackRange.ToString();

		}

		else
		{
			towerInfoMax.SetActive(false);
			towerInfo.SetActive(true);
			
			TowerSprite.spriteName = "tower"+(type + 1);
			
			upgrade.text = "upgrade("+towerDB.towerList[towerType].towerLevelList[towerLevel+1].needSilver+"$)";

			health.text = tower.GetComponent<Tower_FIRE>().maxHealth.ToString("N0");
			nextHealth.text = tower.GetComponent<Tower_FIRE>().nextFinalHealth.ToString("N0");
			
			defense.text = tower.GetComponent<Tower_FIRE>().defense.ToString("N0");
			nextDefense.text = tower.GetComponent<Tower_FIRE>().nextDefense.ToString("N0");
			
			attackDamage.text = tower.GetComponent<Tower_FIRE>().finalDamage.ToString("N0");
			nextAttackDamage.text = tower.GetComponent<Tower_FIRE>().nextFinalDamage.ToString("N0");
			
			attackSpeed.text = tower.GetComponent<Tower_FIRE>().finalSpeed.ToString("N2");
			nextAttackSpeed.text = tower.GetComponent<Tower_FIRE>().nextFinalSpeed.ToString("N2");
			
			attackRange.text = tower.GetComponent<Tower_FIRE>().attackRange.ToString();
			nextAttackRange.text = tower.GetComponent<Tower_FIRE>().nextAttackRange.ToString();
		}
				
	}
}
