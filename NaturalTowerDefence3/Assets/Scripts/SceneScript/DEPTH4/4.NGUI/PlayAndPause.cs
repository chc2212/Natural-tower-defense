using UnityEngine;
using System.Collections;

public class PlayAndPause : MonoBehaviour {
	
	public UISprite sprite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.state == PlayScene.STATE.IDLE)
			sprite.spriteName = "btn_pause";
		else if(PlayScene.state == PlayScene.STATE.PAUSE)
			sprite.spriteName = "btn_play";
	}
	
	void OnClick()
	{
		if(this.gameObject.name == "2.PlayAndPause")
		{
			if(PlayScene.state == PlayScene.STATE.IDLE)
				PlayScene.state = PlayScene.STATE.PAUSE;
			else if(PlayScene.state == PlayScene.STATE.PAUSE)
				PlayScene.state = PlayScene.STATE.IDLE;
		}
	}
}
