using UnityEngine;
using System.Collections;

public class WaveStatusBar : MonoBehaviour {

	public GameObject gameManager;
	public UILabel currentLabel;
	public UILabel LastLabel;
	public GameObject main;

	int wave;
	int waveCount;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		main.SetActive (false);
		
		wave = gameManager.GetComponent<PlayScene>().wave;
		waveCount = gameManager.GetComponent<CreateMonster>().waveCount;
		
		if(this.gameObject.name == "CurrentWave")
		{
			//if(wave == waveCount)
				//waveCount--;
			currentLabel.text = waveCount.ToString();
		}
		
		else if(this.gameObject.name == "LastWave")
			LastLabel.text = wave.ToString();
	}
}
