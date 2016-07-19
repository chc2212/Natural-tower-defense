using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	
	public GameObject[] Blur;
	
	public GameObject[] SpellBlur;
	
	public static float[] spellTime = {60, 30};
	public static bool[] isSpellClick = {false, false};
	public static float[] spellTimer = {0, 0};
	
	int spellNumber;
	
	// Use this for initialization
	void Start () {
		if(this.gameObject.name == "5.Sprite (btn_spell1)")
		{
			spellNumber = 0;
		}
		else if(this.gameObject.name == "5.Sprite (btn_spell2)")
		{
			spellNumber = 1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		switch (this.gameObject.name) 
		{
		case "5.Sprite (btn_spell1)":
		case "5.Sprite (btn_spell2)":
			if (PlayScene.state == PlayScene.STATE.IDLE) 
			{
				Blur [0].SetActive (false);
				Blur [1].SetActive (false);	

				spellTimer [spellNumber] += Time.deltaTime * PlayScene.playSpeed;
			}

			if (spellTimer [spellNumber] >= spellTime [spellNumber]) 
			{
				isSpellClick [spellNumber] = true;
			} 
			else
				isSpellClick [spellNumber] = false;

			SpellBlur [spellNumber].GetComponent<UISprite> ().fillAmount = (spellTime [spellNumber] - spellTimer [spellNumber]) / spellTime [spellNumber];

			break;
		}
	}

	void OnClick()
	{
		switch(this.gameObject.name)
		{
		case "5.Sprite (btn_spell1)":
			if(isSpellClick[spellNumber] == true)
			{
				PlayScene.state = PlayScene.STATE.SPELL1;
				Blur[0].SetActive(true);
				Blur[1].SetActive(true);
			}
			break;
		case "5.Sprite (btn_spell2)":
			if(isSpellClick[spellNumber] == true)
			{
				PlayScene.state = PlayScene.STATE.SPELL2;
				Blur[0].SetActive(true);
				Blur[1].SetActive(true);
			}
			break;
		case "7.Blur Spell_UP":
		case "7.Blur Spell_DOWN":
			PlayScene.state = PlayScene.STATE.IDLE;
			break;
		}
	}
}
