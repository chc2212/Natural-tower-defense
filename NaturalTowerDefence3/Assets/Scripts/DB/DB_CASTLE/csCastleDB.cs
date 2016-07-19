using UnityEngine;
using System.Collections;


public class csCastleDB : MonoBehaviour{
	public UIAtlas iconAtlas;
	public string iconName = "";
	
	public int[] castleHealth;
	public int[] castleHealthExpense;
	public int[] castleDefense;
	public int[] castleDefenseExpense;
	public int[] turretAttackDamage;
	public int turretAttackSpeed;
	public int turretAttackRange;
	public int turretMissileSpeed;
	public int[] turretAttackDamageExpense;
	public int maxTurretSlot;
	public int[] maxTurretSlotExpense;
}
