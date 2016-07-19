using UnityEngine;
using System.Collections;

public class AudioScirpt : MonoBehaviour {

	public static bool bgmFlag = true;
	public static bool effectFlag = true;
	//1.Main
	public GameObject BGM_START;

	//2.Inventory
	public GameObject BGM_INVENTORY;


	//3.Combat
	public GameObject[] BGM_COMBAT;
	public GameObject BGM_DEFEAT;
	public GameObject BGM_WIN;


	public GameObject Effect_TowerMake;
	public GameObject Effect_TowerDestroy;
	public GameObject Effect_MonsterDestroy;
	public GameObject Effect_GainItem;
	public GameObject Effect_Heal;
	public GameObject Effect_Lightning;

	public GameObject Effect_Missile_Lightning;
	public GameObject Effect_Missile_Fire;
	public GameObject Effect_Missile_Freeze;
	public GameObject Effect_Missile_Laser;
	public GameObject Effect_Missile_Redball;

	//4.ETC
	public GameObject Effect_Button;


	void Update(){
		if (bgmFlag) 
			ALL_BGM_ON();
		else
			ALL_BGM_OFF();

		if (effectFlag)
			ALL_EFFECT_ON();
		else
			ALL_EFFECT_OFF();
	}


	//BGM_ON / BGM_OFF
	public void ALL_BGM_ON()
	{
		BGM_START.GetComponent<AudioSource>().mute = false;

		for(int i = 0; i < BGM_COMBAT.Length; i++)
		{
			BGM_COMBAT[i].GetComponent<AudioSource>().mute = false;
		}

		BGM_DEFEAT.GetComponent<AudioSource>().mute = false;
		BGM_WIN.GetComponent<AudioSource>().mute = false;
	}

	public void ALL_BGM_OFF()
	{
		BGM_START.GetComponent<AudioSource>().mute = true;
		BGM_INVENTORY.GetComponent<AudioSource>().mute = true;
		for(int i = 0; i < BGM_COMBAT.Length; i++)
		{
			BGM_COMBAT[i].GetComponent<AudioSource>().mute = true;
		}

		BGM_DEFEAT.GetComponent<AudioSource>().mute = true;
		BGM_WIN.GetComponent<AudioSource>().mute = true;
	}

	//EFFECT_ON / EFFECT_OFF
	public	void ALL_EFFECT_ON()
	{
		Effect_Button.GetComponent<AudioSource>().mute = false;
		Effect_TowerMake.GetComponent<AudioSource>().mute = false;
		Effect_TowerDestroy.GetComponent<AudioSource>().mute = false;
		Effect_MonsterDestroy.GetComponent<AudioSource>().mute = false;
		Effect_GainItem.GetComponent<AudioSource>().mute = false;
		Effect_Heal.GetComponent<AudioSource>().mute = false;
		Effect_Lightning.GetComponent<AudioSource>().mute = false;

		Effect_Missile_Lightning.GetComponent<AudioSource>().mute = false;
		Effect_Missile_Fire.GetComponent<AudioSource>().mute = false;
		Effect_Missile_Freeze.GetComponent<AudioSource>().mute = false;
		Effect_Missile_Laser.GetComponent<AudioSource>().mute = false;
		Effect_Missile_Redball.GetComponent<AudioSource>().mute = false;
	}
	public	void ALL_EFFECT_OFF()
	{
		Effect_Button.GetComponent<AudioSource>().mute = true;
		Effect_TowerMake.GetComponent<AudioSource>().mute = true;
		Effect_TowerDestroy.GetComponent<AudioSource>().mute = true;
		Effect_MonsterDestroy.GetComponent<AudioSource>().mute = true;
		Effect_GainItem.GetComponent<AudioSource>().mute = true;
		Effect_Heal.GetComponent<AudioSource>().mute = true;
		Effect_Lightning.GetComponent<AudioSource>().mute = true;

		Effect_Missile_Lightning.GetComponent<AudioSource>().mute = true;
		Effect_Missile_Fire.GetComponent<AudioSource>().mute = true;
		Effect_Missile_Freeze.GetComponent<AudioSource>().mute = true;
		Effect_Missile_Laser.GetComponent<AudioSource>().mute = true;
		Effect_Missile_Redball.GetComponent<AudioSource>().mute = true;
	}



	public void BGM_START_ON(){	BGM_START.SetActive(true);	}
	public void BGM_START_OFF(){	BGM_START.SetActive(false);	}


	public void BGM_COMBAT_ON(int activeNumber)
	{	
		for(int i = 0; i < BGM_COMBAT.Length; i++)
		{
			if(activeNumber-1 == i)
				BGM_COMBAT[i].SetActive(true);
			else
				BGM_COMBAT[i].SetActive(false);
		}
	}

	public void BGM_COMBAT_OFF()
	{
		for(int i = 0; i < BGM_COMBAT.Length; i++)
		{
			BGM_COMBAT[i].SetActive(false);
		}
	}
	public void BGM_DEFEAT_ON(){	BGM_DEFEAT.SetActive(false); BGM_DEFEAT.SetActive(true);	}
	public void BGM_WIN_ON(){	BGM_WIN.SetActive(false); BGM_WIN.SetActive(true);	}


	public void EFFECT_BUTTON_ON(){	Effect_Button.SetActive(false); Effect_Button.SetActive(true);	}
	public void Effect_TowerMake_ON(){	Effect_TowerMake.SetActive(false); Effect_TowerMake.SetActive(true);	}
	public void Effect_TowerDestroy_ON(){	Effect_TowerDestroy.SetActive(false); Effect_TowerDestroy.SetActive(true);	}
	public void Effect_MonsterDestroy_ON(){	Effect_MonsterDestroy.SetActive(false); Effect_MonsterDestroy.SetActive(true);	}
	public void Effect_GainItem_ON(){	Effect_GainItem.SetActive(false); Effect_GainItem.SetActive(true);	}
	public void Effect_Heal_ON(){	Effect_Heal.SetActive(false); Effect_Heal.SetActive(true);	}
	public void Effect_Lightning_ON(){	Effect_Lightning.SetActive(false); Effect_Lightning.SetActive(true);	}

	public void Effect_Missile_Lightning_ON(){	Effect_Missile_Lightning.SetActive(false); Effect_Missile_Lightning.SetActive(true);	}
	public void Effect_Missile_Fire_ON(){	Effect_Missile_Fire.SetActive(false); Effect_Missile_Fire.SetActive(true);	}
	public void Effect_Missile_Freeze_ON(){	Effect_Missile_Freeze.SetActive(false); Effect_Missile_Freeze.SetActive(true);	}
	public void Effect_Missile_Laser_ON(){	Effect_Missile_Laser.SetActive(false); Effect_Missile_Laser.SetActive(true);	}
	public void Effect_Missile_Redball_ON(){	Effect_Missile_Redball.SetActive(false); Effect_Missile_Redball.SetActive(true);	}
}
