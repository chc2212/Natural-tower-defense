using UnityEngine;
using System.Collections;

public class OptionScene : MonoBehaviour {
	
	public GameObject OptionWindow;
	
	public GameObject InventoryScene;
	public GameObject GameScene;
	public GameObject CharacterScene;
	public GameObject main;
	
	public UISprite BgSound;
	public UISprite Sound;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(AudioScirpt.bgmFlag)	
			BgSound.spriteName = "btn_bgsound_on";
		else 
			BgSound.spriteName = "btn_bgsound_off";
		if(AudioScirpt.effectFlag)
			Sound.spriteName = "btn_sound_on";
		else
			Sound.spriteName = "btn_sound_off";
	}
	
	void OnClick()
	{
		//Debug.Log (this.gameObject.name);
		switch(this.gameObject.name)
		{
		case "1.Option Sprite":
			OptionWindow.SetActive(true);
			PlayScene.beforeState = PlayScene.state;
			PlayScene.state = PlayScene.STATE.OPTION;
			break;
		case "1.Sprite (btn_play)" :
			OptionWindow.SetActive(false);
			PlayScene.state = PlayScene.beforeState;
			break;
		case "2.Sprite (btn_bgsound_on)":
			if(BgSound.spriteName == "btn_bgsound_on")
			{
				AudioScirpt.bgmFlag = false;
				BgSound.spriteName = "btn_bgsound_off";
			}
			else
			{
				AudioScirpt.bgmFlag = true;
				BgSound.spriteName = "btn_bgsound_on";
			}
			break;
		case "3.Sprite (btn_sound_on)":
			if(Sound.spriteName == "btn_sound_on")
			{
				AudioScirpt.effectFlag = false;
				Sound.spriteName = "btn_sound_off";
			}
			else
			{
				AudioScirpt.effectFlag = true;
				Sound.spriteName = "btn_sound_on";
			}
			break;
		case "4.Sprite (btn_restart)":
			PlayScene.state = PlayScene.STATE.RESTART;
			OptionWindow.SetActive(false);
			break;			
		case "5.Sprite (btn_out)":
		
			Main.currentSceneNumber = 1; 
			PlayScene.state = PlayScene.STATE.BEFORE;
		
		
			GameScene.SetActive(false);
			OptionWindow.SetActive(false);
			main.SetActive(true);	

			break;
		}
	}
}
