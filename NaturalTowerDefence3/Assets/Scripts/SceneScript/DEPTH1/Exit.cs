using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	public GameObject ExitObj;

	public UIPanel panel1;
	public UIPanel panel2;

	public UILabel ExitLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Main.currentSceneNumber == 1)
		{
			;
		}
	}

	void OnClick()
	{

		switch(this.gameObject.name)
		{
		case "3.Yes Button":
			Application.Quit();
			break;
		case "4.No Button":
			ExitObj.SetActive(false);
			panel1.alpha = 1f;
			panel2.alpha = 1f;
			break;
		}
	}

}
