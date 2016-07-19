using UnityEngine;
using System.Collections;

public class x124 : MonoBehaviour {
	
	public UILabel label;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayScene.playSpeed == 1){
			label.text = "X1";
		}	
		else if(PlayScene.playSpeed == 2){
			label.text = "X2";
		}
		else if(PlayScene.playSpeed == 4){
			label.text = "X4";
		}	
	}
	
	void OnClick(){
		if(PlayScene.playSpeed == 1){
			PlayScene.playSpeed = 2;
		}
			
		else if(PlayScene.playSpeed == 2){
			PlayScene.playSpeed = 4;
		}
		else if(PlayScene.playSpeed == 4){
			PlayScene.playSpeed = 1;
		}
	}
}
