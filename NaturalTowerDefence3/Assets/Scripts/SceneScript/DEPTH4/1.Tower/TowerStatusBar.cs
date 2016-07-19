using UnityEngine;
using System.Collections;

public class TowerStatusBar : MonoBehaviour {
	
	public UISlider slider;
	
	float currentHealth;
	float maxHealth;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = this.gameObject.transform.parent.gameObject.GetComponent<Tower_FIRE>().finalHealth;
		maxHealth = this.gameObject.transform.parent.gameObject.GetComponent<Tower_FIRE>().maxHealth;
		slider.value = currentHealth / maxHealth;
	}
}
