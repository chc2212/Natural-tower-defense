using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {
	
	
	public GameObject GameOverWindow;
	public GameObject GameOverMenuWindow;

	public GameObject InventoryScene;
	public GameObject GameScene;
	public GameObject CharacterScene;

	public GameObject main;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick(){
		switch(this.gameObject.name)
		{
		case "2.Home Button":
			Main.currentSceneNumber = 1; 
			PlayScene.state = PlayScene.STATE.BEFORE;

			GameScene.SetActive(false);
			GameScene.SetActive(false);
			GameOverWindow.SetActive(false);
			GameOverMenuWindow.SetActive(false);
			main.SetActive(true);	
		
			break;

		
		case "3.Refresh button":
			GameOverWindow.SetActive(false);
			GameOverMenuWindow.SetActive(false);
			PlayScene.state = PlayScene.STATE.RESTART;
			break;
		}
	}

}
