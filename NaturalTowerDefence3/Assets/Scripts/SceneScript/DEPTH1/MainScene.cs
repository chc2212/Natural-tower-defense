using UnityEngine;
using System.Collections;

public class MainScene : MonoBehaviour {
	
	public GameObject main;
	public GameObject inventory;
	public GameObject inventory_Character;
	public GameObject warningPopup;

	public GameObject Option;
	public GameObject HelpObj;

	public csUserSaveNLoad userInfo;

	public UIPanel panel1;
	public UIPanel panel2;

	public GameObject stageScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		Debug.Log (this.gameObject.name);
		
		switch(this.gameObject.name)
		{
		case "2.Sprite (btn_start)" : 
			
			if(EncryptedPlayerPrefs.GetInt("DataExist", 0) == 1) 
			{
				csUserSaveNLoad.loadFlag = true;

				inventory.SetActive(true);
				stageScene.SetActive(true);				
				break;
			}
			else
			{
				csUserSaveNLoad.loadFlag = true;
			
				userInfo.InitTest();
				inventory.SetActive(true);
				stageScene.SetActive(true);				
				break;
			}

		case "4.Sprite (btn_load)" :
			if(EncryptedPlayerPrefs.GetInt("DataExist", 0) == 1) 
			{
				csUserSaveNLoad.loadFlag = true;
				main.SetActive(false);
				inventory.SetActive(true);
				inventory_Character.SetActive(true);
			
				Main.currentSceneNumber = 2; 
			}
			break;
		case "3.Yes Button" :
			Main.currentSceneNumber = 2;
			csUserSaveNLoad.loadFlag = true;
			userInfo.InitTest();
		
			warningPopup.SetActive(false);
			main.SetActive(false);
			inventory.SetActive(true);
			inventory_Character.SetActive(true);
	
			panel1.alpha = 1.0f;
			panel2.alpha = 1.0f;
			break;
		case "4.No Button" :
			warningPopup.SetActive(false);
			panel1.alpha = 1.0f;
			panel2.alpha = 1.0f;
			break;
		case "4.Sprite (btn_option)":
			panel1.alpha = 0.5f; 
			panel2.alpha = 0.5f;
			Option.SetActive(true);
			break;
		case "8.Sprite (btn_help)":
			panel1.alpha = 0.5f; 
			panel2.alpha = 0.5f;
			HelpObj.SetActive (true);
			break;
		}
	}
}
