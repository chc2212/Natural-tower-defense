using UnityEngine;
using System.Collections;

public class MonsterStatusBar : MonoBehaviour {
	
	public UISlider slider;
	
	float currentHealth;
	float maxHealth;
	float fixedX;
	
	float positionX;
	// Use this for initialization
	void Start () {
		positionX = this.gameObject.transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {
		fixedX = -(this.gameObject.transform.parent.gameObject.GetComponent<tk2dSprite>().CurrentSprite.untrimmedBoundsData[1].x 
			* this.gameObject.transform.parent.gameObject.GetComponent<tk2dSprite>().scale.x) / 2;
		
		currentHealth = this.gameObject.transform.parent.gameObject.GetComponent<Monster>().health;
		maxHealth = this.gameObject.transform.parent.gameObject.GetComponent<Monster>().maxHealth;
		slider.value = currentHealth / maxHealth;
		//Debug.Log(this.gameObject.transform.parent.gameObject.GetComponent<tk2dSprite>().CurrentSprite.untrimmedBoundsData[1].x);
		this.gameObject.transform.localPosition = new Vector3(positionX + fixedX, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
	}
}
