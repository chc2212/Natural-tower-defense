using UnityEngine;
using System.Collections;

public class CastleStatusBar : MonoBehaviour {
	
	public UISlider slider;
	public GameObject castle;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.state == PlayScene.STATE.IDLE && castle != null)
		{
			slider.value = (castle.GetComponent<CastleInfo>().health / castle.GetComponent<CastleInfo>().maxHealth);
		}
	}
}
