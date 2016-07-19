using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	
	//public enum MSTATE { DEAD};
	
	public Transform target;
	public Vector3 destroyedTarget;
	public float missileSpeed;
	public float missileDamage;
	public float amtToMove;
	public csClassTower.SpecialAbility ability;

	
	public bool onceEnterFlag = true;
	public float missileTimer = 0; 
	
	// Use this for initialization
	void Start () {
		//transform.LookAt(target);
	}
	
	// Update is called once per frame
	void Update () {

		missileTimer += Time.deltaTime * PlayScene.playSpeed;
		
		if(PlayScene.state == PlayScene.STATE.IDLE)
		{
			if(target == null)
			{			
				Vector3 v = (destroyedTarget - gameObject.transform.position).normalized;
				gameObject.transform.Translate(v * amtToMove);
				
				if(Mathf.Abs(destroyedTarget.x - gameObject.transform.position.x) < 25 &&
					Mathf.Abs(destroyedTarget.y - gameObject.transform.position.y) < 25)
				{
					Destroy(this.gameObject);
				}
				if(destroyedTarget.x == 0 && destroyedTarget.y == 0) 
					Destroy(this.gameObject);
				
				if(missileTimer > 3) 
					Destroy(this.gameObject);
				
			} 
			
			else if(target != null)
			{
				amtToMove = PlayScene.playSpeed * missileSpeed * Time.deltaTime;
				Vector3 v = (target.transform.position-gameObject.transform.position).normalized;
				destroyedTarget = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
				gameObject.transform.Translate(v * amtToMove);
				
				if(target.gameObject.GetComponent<Monster>().mState == Monster.MSTATE.DEAD)
				{
					target = null;
					return;
				} 
			}

		}
		
	}
	
	void OnTriggerEnter(Collider coll){

		if(coll.gameObject.tag == "MONSTER" && onceEnterFlag == true) 
		{
			switch(ability)
			{
			case csClassTower.SpecialAbility.Normal:
				AttackProcessing(coll, "Normal");
				break;
			case csClassTower.SpecialAbility.multiplexTarget:
				AttackProcessing(coll, "Multiplex");
				break;
			case csClassTower.SpecialAbility.SlowSpeed:
				AttackProcessing(coll, "SlowSpeed");
				break;
			case csClassTower.SpecialAbility.Splash:

				Collider[] enemies = Physics.OverlapSphere(this.gameObject.transform.position, 50);
				for(int i = 0; i < enemies.Length; i++)
				{
					if(enemies[i].gameObject.tag == "MONSTER")
					{	
						if(enemies[i].gameObject.GetComponent<Monster>().mState == Monster.MSTATE.DEAD)
							continue; 
						
						string[] kind = new string[2]{"Splash", (missileDamage * 0.4f).ToString()}; 
						enemies[i].gameObject.SendMessage("DamagedByTower", kind, SendMessageOptions.DontRequireReceiver);
					}
				}
				AttackProcessing(coll, "Splash");
				break;
			}
		}
	}

	void AttackProcessing(Collider coll, string attackType)
	{
		onceEnterFlag = false; 
		string[] kind = new string[2]{attackType, missileDamage.ToString()};
		coll.gameObject.SendMessage("DamagedByTower", kind, SendMessageOptions.DontRequireReceiver);
		Destroy(this.gameObject);
		PlayScene.CurrentMissileCount--;
	}
}
