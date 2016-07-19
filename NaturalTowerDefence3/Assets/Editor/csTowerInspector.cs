using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(csTowerDB))]
public class csTowerInspector : Editor {

	// I know that this code start...
	
	static int mTowerIndex = 0;
	
	bool mConfirmTowerDelete = false;
	bool mConfirmTowerLevelDelete = false;
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		csTowerDB db = target as csTowerDB;
		NGUIEditorTools.DrawSeparator();
		
		csTowerManager currentTM = null;
		csClassTower currentCT = null;
		
		// stage check
		if(db.towerList == null || db.towerList.Count == 0)
		{
			mTowerIndex = 0;
		}
		
		else
		{
			mTowerIndex = Mathf.Clamp(mTowerIndex, 0, db.towerList.Count - 1);
			currentTM = db.towerList[mTowerIndex];
		}
		
		// towerList check
		if(db.towerList.Count != 0)
		{
			if (db.towerList[mTowerIndex].towerLevelList == null || db.towerList[mTowerIndex].towerLevelList.Count == 0)
			{
				db.towerList[mTowerIndex].towerLevelIndex = 0;
			}
			else
			{
				db.towerList[mTowerIndex].towerLevelIndex = Mathf.Clamp(db.towerList[mTowerIndex].towerLevelIndex, 0, db.towerList[mTowerIndex].towerLevelList.Count - 1);
				currentCT = db.towerList[mTowerIndex].towerLevelList[db.towerList[mTowerIndex].towerLevelIndex];
			}
		}

		// delete button click after..
		if (mConfirmTowerDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete 'Tower : " + currentTM.type +"'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmTowerDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					db.towerList.RemoveAt(mTowerIndex);
					mConfirmTowerDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}
		
		else if (mConfirmTowerLevelDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete '" + currentCT.name + "'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmTowerLevelDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					db.towerList[mTowerIndex].towerLevelList.RemoveAt(db.towerList[mTowerIndex].towerLevelIndex);
					mConfirmTowerLevelDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}

		
		else if(!mConfirmTowerLevelDelete && !mConfirmTowerDelete)
		{	
			// "ADD TOWER" button
			GUI.backgroundColor = Color.green;
			
			NGUIEditorTools.DrawSeparator();
			
			if (GUILayout.Button ("ADD TOWER"))
			{
				// TOWER ADD
				csTowerManager tm = new csTowerManager();
				db.towerList.Add(tm);
				mTowerIndex = db.towerList.Count - 1;
				
				currentTM = tm;
			}

			if(currentTM != null)
			{
				NGUIEditorTools.DrawSeparator();
				// "DELETE TOWER" button
				GUILayout.BeginHorizontal();
				{
					GUI.backgroundColor = Color.red;
					
					if (GUILayout.Button("DELETE TOWER"))
					{
						mConfirmTowerDelete = true;
					}
					GUI.backgroundColor = Color.white;
				}
				GUILayout.EndHorizontal();
				
				GUI.backgroundColor = Color.white;
				
				GUILayout.BeginHorizontal();
				{
					if (mTowerIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) {--mTowerIndex; }
					GUI.color = Color.white;
					mTowerIndex = EditorGUILayout.IntField(mTowerIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.towerList.Count, GUILayout.Width(40f));
					if (mTowerIndex + 1 == db.towerList.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) {++mTowerIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
		
				// stage check because mTowerIndex data out of range
				if(db.towerList == null || db.towerList.Count == 0)
				{
					mTowerIndex = 0;
				}
				
				else
				{
					mTowerIndex = Mathf.Clamp(mTowerIndex, 0, db.towerList.Count - 1);
					currentTM = db.towerList[mTowerIndex];
				}
				
				NGUIEditorTools.DrawSeparator();
			
				csTowerManager.TowerType towerType = 
					(csTowerManager.TowerType)EditorGUILayout.EnumPopup("TowerType", currentTM.type);
				
				if(!towerType.Equals(currentTM.type))	currentTM.type = towerType;
			}
			NGUIEditorTools.DrawSeparator();
			
			GUI.backgroundColor = Color.green;
			
			if(db.towerList.Count != 0)
			{
				if (GUILayout.Button("ADD TOWER_LEVEL"))
				{
					csClassTower cm = new csClassTower();
					db.towerList[mTowerIndex].towerLevelList.Add(cm);
					db.towerList[mTowerIndex].towerLevelIndex = db.towerList[mTowerIndex].towerLevelList.Count - 1;
					
					cm.name = "New TOWERLEVEL";
					cm.description = "TOWER Description";
					
					currentCT = cm;
					//Debug.Log ("hihihi");
				}
			}

			GUI.backgroundColor = Color.white;
			
			
			if (currentCT != null)
			{
				NGUIEditorTools.DrawSeparator();
				
				// << 1 / 1 >>
				GUILayout.BeginHorizontal();
				{
					if (db.towerList[mTowerIndex].towerLevelIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) {--db.towerList[mTowerIndex].towerLevelIndex; }
					GUI.color = Color.white;
					db.towerList[mTowerIndex].towerLevelIndex = EditorGUILayout.IntField(db.towerList[mTowerIndex].towerLevelIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.towerList[mTowerIndex].towerLevelList.Count, GUILayout.Width(40f));
					if (db.towerList[mTowerIndex].towerLevelIndex + 1 == db.towerList[mTowerIndex].towerLevelList.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) {++db.towerList[mTowerIndex].towerLevelIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
				
				NGUIEditorTools.DrawSeparator();
				
				// Tower name and delete Tower button
				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("TowerName", GUILayout.Width (82f));
					string towerName = EditorGUILayout.TextField(currentCT.name);
		
					GUI.backgroundColor = Color.red;
		
					if (GUILayout.Button("Delete", GUILayout.Width(55f)))
					{
						mConfirmTowerLevelDelete = true;
					}
					GUI.backgroundColor = Color.white;
					
					if (!towerName.Equals(currentCT.name))
					{
						currentCT.name = towerName;
					}
				}
				GUILayout.EndHorizontal();
				
				string towerDesc = GUILayout.TextArea(currentCT.description, 100, GUILayout.Height(50f));
		
	
				GUILayout.BeginHorizontal();
				float mHealth = EditorGUILayout.FloatField("Health", currentCT.health);
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				float mDefense = EditorGUILayout.FloatField("Defense", currentCT.defense);
				GUILayout.EndHorizontal();		

				GUILayout.BeginHorizontal();
				float mAttackDamage = EditorGUILayout.FloatField("AttackDamage", currentCT.attackDamage);
				GUILayout.EndHorizontal();
				
				GUILayout.BeginHorizontal();
				float mAttackSpeed = EditorGUILayout.FloatField("AttackSpeed", currentCT.attackSpeed);
				GUILayout.EndHorizontal();		
				

				float mAttackRange = EditorGUILayout.FloatField("AttackRange", currentCT.attackRange);
				float mBuildTime = EditorGUILayout.FloatField("BuildTime", currentCT.buildTime);
				float mMissileSpeed = EditorGUILayout.FloatField("MissileSpeed", currentCT.missileSpeed);
				float mNeedSilver = EditorGUILayout.FloatField("NeedSilver", currentCT.needSilver);
				float mNeedGold = EditorGUILayout.FloatField("NeedGold", currentCT.needGold);
				

				csClassTower.SpecialAbility specialAbility = 
					(csClassTower.SpecialAbility)EditorGUILayout.EnumPopup("SpecialAbility", currentCT.ability);
			
				if (!towerDesc.Equals(currentCT.description) ||
					mHealth != currentCT.health ||
					mDefense != currentCT.defense ||
					mAttackDamage != currentCT.attackDamage ||
					mAttackSpeed != currentCT.attackSpeed ||
					mAttackRange != currentCT.attackRange ||
					mBuildTime != currentCT.buildTime ||
					mNeedSilver != currentCT.needSilver ||
					mMissileSpeed != currentCT.missileSpeed ||
					!specialAbility.Equals (currentCT.ability) ||
					mNeedGold != currentCT.needGold)
				{
					currentCT.description = towerDesc;
					currentCT.health = mHealth;
					currentCT.defense = mDefense;
					currentCT.attackDamage = mAttackDamage;
					currentCT.attackSpeed = mAttackSpeed;
					currentCT.attackRange = mAttackRange;
					currentCT.buildTime = mBuildTime;
					currentCT.needSilver = mNeedSilver;
					currentCT.missileSpeed = mMissileSpeed;
					currentCT.ability = specialAbility;
					currentCT.needGold = mNeedGold;
				}

			} // end of if (currentCT != null)
			
		} // end of else
		
	} // end of OnInspectorGUI
	
} // end of class csTowerDBInspector