using UnityEngine;
using System.Collections;

public class SetStage : MonoBehaviour {

	public UISprite StageBackground;
	public UILabel StageNumber;
	public GameObject Star1;
	public GameObject Star2;
	public GameObject Star3;
	
	public int indexNumber;
	public int star;
	
	
	// Use this for initialization
	void Start () {
		StageBackground = this.gameObject.transform.FindChild("1.Sprite (bg_stage)").gameObject.GetComponent<UISprite>();
		StageNumber = this.gameObject.transform.FindChild("2.StageNum").gameObject.GetComponent<UILabel>();
		Star1 = this.gameObject.transform.FindChild("3.Sprite (img_star)").gameObject;
		Star2 = this.gameObject.transform.FindChild("4.Sprite (img_star)").gameObject;
		Star3 = this.gameObject.transform.FindChild("5.Sprite (img_star)").gameObject;

	}
	
	// Update is called once per frame
	void Update () {
		if(CreateStage.maxClearStage+1 >= indexNumber)
		{
			StageBackground.spriteName = "bg_stage";
			StageNumber.gameObject.SetActive(true);
			
			StageNumber.text = indexNumber.ToString();
			this.gameObject.GetComponent<UIButtonMessage>().target = GameObject.Find ("4.Sound Manager");
		}
		else
		{
			StageBackground.spriteName = "bg_stage_gray";
			StageNumber.gameObject.SetActive(false);
		}
		
		star = CreateStage.stageClearInfo[indexNumber-1];
		
		if(star > 3)
			star = 3;
		
		if(star == 0)
		{
			Star1.SetActive(false);
			Star2.SetActive(false);
			Star3.SetActive(false);			
		}
		if(star == 1)
		{
			Star1.SetActive(true);
			Star2.SetActive(false);
			Star3.SetActive(false);
		}
		if(star == 2)
		{
			Star1.SetActive(true);
			Star2.SetActive(true);
			Star3.SetActive(false);
		}
		if(star == 3)
		{
			Star1.SetActive(true);
			Star2.SetActive(true);
			Star3.SetActive(true);
		}
	}
}
