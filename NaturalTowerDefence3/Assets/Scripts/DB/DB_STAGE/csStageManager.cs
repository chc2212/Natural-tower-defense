using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class csStageManager{
	public List<csClassStage> stageList = new List<csClassStage>();
	
	public MapType type;
	
	public enum MapType{
		SPRING,
		SUMMER,
		FALL,
		WINTER
	}
	
	public int stageIndex = 0;
}
