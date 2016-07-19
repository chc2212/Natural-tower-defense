using UnityEngine;
using System.Collections;

public class LevelSelectCancel : MonoBehaviour {

	public GameObject StageSelect;
	public GameObject Character;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		StageSelect.SetActive(false);
		//Character.SetActive(true);
	
	}
}
