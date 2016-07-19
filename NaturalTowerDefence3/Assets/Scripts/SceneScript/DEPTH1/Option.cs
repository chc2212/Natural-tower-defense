using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour {

	public csUserSaveNLoad userInfo;
	public GameObject option;

	public UISprite BGM;
	public UISprite Effect;

	public UIPanel panel1;
	public UIPanel panel2;

	public UIPanel panel3;

	public GameObject warningPopup;
	public UILabel label;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(AudioScirpt.bgmFlag)	
			BGM.spriteName = "btn_bgsound_on";
		else 
			BGM.spriteName = "btn_bgsound_off";
		if(AudioScirpt.effectFlag)
			Effect.spriteName = "btn_sound_on";
		else
			Effect.spriteName = "btn_sound_off";
	}

	void OnClick()
	{
		switch (this.gameObject.name) 
		{
		case "2.BGSound":
			if(AudioScirpt.bgmFlag)
			{
				BGM.spriteName = "btn_bgsound_off";
				AudioScirpt.bgmFlag = false;
			}
			else
			{
				BGM.spriteName = "btn_bgsound_on";
				AudioScirpt.bgmFlag = true;
			}
			break;
		case "3.EffectSound":
			if(AudioScirpt.effectFlag)
			{
				Effect.spriteName = "btn_sound_off";
				AudioScirpt.effectFlag = false;
			}
			else
			{
				Effect.spriteName = "btn_sound_on";
				AudioScirpt.effectFlag = true;
			}
			break;
		case "4.DeleteData":
			panel3.alpha = 0.5f;
			warningPopup.SetActive(true);
		
			break;
		case "5.Exit":
			panel1.alpha = 1.0f; 
			panel2.alpha = 1.0f;
			option.SetActive(false);
			break;

		case "3.Yes Button" :
			csUserSaveNLoad.loadFlag = true;
			userInfo.InitTest();

			warningPopup.SetActive(false);
			panel3.alpha = 1.0f;
			break;
		case "4.No Button" :
			warningPopup.SetActive(false);
			panel3.alpha = 1.0f;
			break;
		}
	}
}
