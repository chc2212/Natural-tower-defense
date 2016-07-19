using UnityEngine;
using System.Collections;

public class MapScene : MonoBehaviour {
	
	public GameObject stageSelect;
	
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnClick()
	{
		stageSelect.SetActive(true);
	}
	
	void Test()
	{
		stageSelect.SetActive(false);
	}
}
