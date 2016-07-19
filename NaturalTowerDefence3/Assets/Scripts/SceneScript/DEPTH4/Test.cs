using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	
	public csUserSaveNLoad user;
	// Use this for initialization
	void Start () {
		StartCoroutine(Wait(10f));
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnMouseDown(){
	
		
	}
	
	IEnumerator Wait (float waittime) 
	{
		Debug.Log ("현재 STATE : "+PlayScene.state);

		yield return new WaitForSeconds(waittime);
		StartCoroutine(Wait(10f));
	}
}

