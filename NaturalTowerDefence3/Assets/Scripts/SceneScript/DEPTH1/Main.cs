using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour {

	public AudioScirpt SoundManager;

	public static int currentSceneNumber = 1; 
	public static int currentGameStage = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(currentSceneNumber == 1)
		{
			SoundManager.BGM_START_ON();
		
			SoundManager.BGM_COMBAT_OFF();

		}
		if(currentSceneNumber == 2)
		{
			SoundManager.BGM_START_OFF();
		
			SoundManager.BGM_COMBAT_OFF();
		}
		if(currentSceneNumber == 3)
		{
			SoundManager.BGM_START_OFF();


			if(currentGameStage>=3)
				currentGameStage = 3;

			SoundManager.BGM_COMBAT_ON(currentGameStage);
		}
	}
}
