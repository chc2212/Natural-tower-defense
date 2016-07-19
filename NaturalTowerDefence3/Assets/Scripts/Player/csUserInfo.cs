using UnityEngine;
using System.Collections;

public class csUserInfo{
	public UIAtlas iconAtlas;
	public string iconName = "";
	
	public csCastle castle = new csCastle();
	public csPlayerStat stat = new csPlayerStat();
	public csTower tower = new csTower();


	public csAchieveInfo achieveInfo = new csAchieveInfo();
	public bool[] bAchievement = new bool[3]; 

	public int cash;
	public int gold;
	public int silver;
	
	public int inPlaySilver;
	
	public int maxClearStage;
	public int[] stageClearInfo = new int[18];
	
	public string picturePath;
	

}
