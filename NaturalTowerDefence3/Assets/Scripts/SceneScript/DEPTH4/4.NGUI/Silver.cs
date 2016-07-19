using UnityEngine;
using System.Collections;

public class Silver : MonoBehaviour {
	
	public UILabel label;
	public csUserSaveNLoad user;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		label.text = user.userInfo.inPlaySilver.ToString();
	}
}
