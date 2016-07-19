using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class csTowerManager{
	
	public List<csClassTower> towerLevelList = new List<csClassTower>();
	
	public TowerType type;
	
	public enum TowerType{
		FIRE,
		LIGHTNING,
		ICE,
		LASER,
		POWER
	}
	
	public int towerLevelIndex = 0;
}
