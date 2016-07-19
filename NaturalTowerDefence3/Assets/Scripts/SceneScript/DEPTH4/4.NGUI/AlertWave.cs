using UnityEngine;
using System.Collections;

public class AlertWave : MonoBehaviour {
	
	public UILabel label;
	public GameObject gameManager;
	
	public UILabel Stage;
	
	public float alertTime;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(this.gameObject.name == "10.StageLabel")
			Stage.text = "[STAGE "+gameManager.GetComponent<PlayScene>().stageNumber.ToString()+"]";
		
		if(this.gameObject.name == "10.Alert Wave")
		{
			if(PlayScene.waitFlag == false){
				alertTime = gameManager.GetComponent<CreateMonster>().WAITTIME - gameManager.GetComponent<CreateMonster>().waitTimer;
			
				label.text = ""+alertTime.ToString("N0")+"seconds until monsters spawn"; 
			}
			
			if(PlayScene.monsterAlertFlag && PlayScene.waitFlag && PlayScene.alertSemi == false){
				alertTime = gameManager.GetComponent<PlayScene>().waveTimer
					+ (gameManager.GetComponent<PlayScene>().monNumPerWave * gameManager.GetComponent<PlayScene>().monsterTimer)
						- gameManager.GetComponent<CreateMonster>().alertTimer;

				label.text = ""+alertTime.ToString("N0")+"seconds until next wave spawn";	
				
			}
			
			if(PlayScene.alertSemi == true)
			{
				alertTime = gameManager.GetComponent<PlayScene>().semiBossTimer
					+ (gameManager.GetComponent<PlayScene>().monNumPerWave * gameManager.GetComponent<PlayScene>().monsterTimer)
						- gameManager.GetComponent<CreateMonster>().sbTimer;
			
				label.text = ""+alertTime.ToString("N0")+"seconds until the intermidiate boss spawn";
			}
			
			if(PlayScene.alertBoss == true)
			{
				alertTime = gameManager.GetComponent<PlayScene>().bossTimer 
					+ (gameManager.GetComponent<PlayScene>().monNumPerWave * gameManager.GetComponent<PlayScene>().monsterTimer)
						- gameManager.GetComponent<CreateMonster>().bTimer;
			
				label.text = ""+alertTime.ToString("N0")+"seconds until the final boss spawn";
			}
			
			if(PlayScene.alertBoss == false && PlayScene.monsterAlertFlag == false && PlayScene.waitFlag == true)
			{
				label.text = "";
			}
		}
		
	
	}
}
