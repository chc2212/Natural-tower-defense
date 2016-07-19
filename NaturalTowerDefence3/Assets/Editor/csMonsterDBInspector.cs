using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(csMonsterDB))]
public class csMonsterDBInspector : Editor
{
	// I know that this code start...
	
	static int mStageIndex = 0;
	
	bool mConfirmStageDelete = false;
	bool mConfirmMonsterDelete = false;
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		csMonsterDB db = target as csMonsterDB;
		NGUIEditorTools.DrawSeparator();
		
		//db.stageList.
		csMonsterManager currentSM = null;
		csClassMonster currentCM = null;
		
		// stage check
		if(db.stageList == null || db.stageList.Count == 0)
		{
			mStageIndex = 0;
		}
		
		else
		{
			mStageIndex = Mathf.Clamp(mStageIndex, 0, db.stageList.Count - 1);
			currentSM = db.stageList[mStageIndex];
		}
		
		// monster check
		if(db.stageList.Count != 0)
		{
			if (db.stageList[mStageIndex].monsterList == null || db.stageList[mStageIndex].monsterList.Count == 0)
			{
				db.stageList[mStageIndex].monsterIndex = 0;
			}
			else
			{
				db.stageList[mStageIndex].monsterIndex = Mathf.Clamp(db.stageList[mStageIndex].monsterIndex, 0, db.stageList[mStageIndex].monsterList.Count - 1);
				currentCM = db.stageList[mStageIndex].monsterList[db.stageList[mStageIndex].monsterIndex];
			}
		}

		// delete button click after..
		if (mConfirmStageDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete 'Stage" +(mStageIndex + 1)+"'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmStageDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					//NGUIEditorTools.RegisterUndo("Delete Inventory Item", db);
					db.stageList.RemoveAt(mStageIndex);
					mConfirmStageDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}
		
		else if (mConfirmMonsterDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete '" + currentCM.name + "'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmMonsterDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					//NGUIEditorTools.RegisterUndo("Delete Inventory Item", db);
					db.stageList[mStageIndex].monsterList.RemoveAt(db.stageList[mStageIndex].monsterIndex);
					mConfirmMonsterDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}

		
		else if(!mConfirmMonsterDelete && !mConfirmStageDelete)
		{		
			// "ADD MONSTER" button
			GUI.backgroundColor = Color.green;
			
			NGUIEditorTools.DrawSeparator();
			
			if (GUILayout.Button ("ADD Stage"))
			{
				// stage ADD
				csMonsterManager sm = new csMonsterManager();
				db.stageList.Add(sm);
				mStageIndex = db.stageList.Count - 1;
				
				currentSM = sm;
			}

			if(currentSM != null)
			{
				NGUIEditorTools.DrawSeparator();
				// "DELETE STAGE" button
				GUILayout.BeginHorizontal();
				{
					GUI.backgroundColor = Color.red;
					
					if (GUILayout.Button("DELETE STAGE"))
					{
						mConfirmStageDelete = true;
					}
					GUI.backgroundColor = Color.white;
				}
				GUILayout.EndHorizontal();
				
				GUI.backgroundColor = Color.white;
				
				GUILayout.BeginHorizontal();
				{
					if (mStageIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) {--mStageIndex; }
					GUI.color = Color.white;
					mStageIndex = EditorGUILayout.IntField(mStageIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.stageList.Count, GUILayout.Width(40f));
					if (mStageIndex + 1 == db.stageList.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) {++mStageIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
		
				// stage check because mStageIndex data out of range
				if(db.stageList == null || db.stageList.Count == 0)
				{
					mStageIndex = 0;
				}
				
				else
				{
					mStageIndex = Mathf.Clamp(mStageIndex, 0, db.stageList.Count - 1);
					currentSM = db.stageList[mStageIndex];
				}
			}
			
			NGUIEditorTools.DrawSeparator();
			
			GUI.backgroundColor = Color.green;
			
			if(db.stageList.Count != 0)
			{
				if (GUILayout.Button("ADD MONSTER"))
				{
					csClassMonster cm = new csClassMonster();
					db.stageList[mStageIndex].monsterList.Add(cm);
					db.stageList[mStageIndex].monsterIndex = db.stageList[mStageIndex].monsterList.Count - 1;
					
					cm.name = "New Monster";
					cm.description = "Monster Description";
					
					currentCM = cm;
					//Debug.Log ("hihihi");
				}
			}

			GUI.backgroundColor = Color.white;
			
			
			if (currentCM != null)
			{
				NGUIEditorTools.DrawSeparator();
				
				// << 1 / 1 >>
				GUILayout.BeginHorizontal();
				{
					if (db.stageList[mStageIndex].monsterIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) {--db.stageList[mStageIndex].monsterIndex; }
					GUI.color = Color.white;
					db.stageList[mStageIndex].monsterIndex = EditorGUILayout.IntField(db.stageList[mStageIndex].monsterIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.stageList[mStageIndex].monsterList.Count, GUILayout.Width(40f));
					if (db.stageList[mStageIndex].monsterIndex + 1 == db.stageList[mStageIndex].monsterList.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) {++db.stageList[mStageIndex].monsterIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
				
				NGUIEditorTools.DrawSeparator();
				
				// Monster name and delete item button
				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("MonsterName", GUILayout.Width (82f));
					string monsterName = EditorGUILayout.TextField(currentCM.name);
		
					GUI.backgroundColor = Color.red;
		
					if (GUILayout.Button("Delete", GUILayout.Width(55f)))
					{
						mConfirmMonsterDelete = true;
					}
					GUI.backgroundColor = Color.white;
					
					if (!monsterName.Equals(currentCM.name))
					{
						//NGUIEditorTools.RegisterUndo("Rename Item", db);
						currentCM.name = monsterName;
					}
				}
				GUILayout.EndHorizontal();
				
				string itemDesc = GUILayout.TextArea(currentCM.description, 100, GUILayout.Height(50f));
				
				csClassMonster.MonsterType monsterType = 
					(csClassMonster.MonsterType)EditorGUILayout.EnumPopup("MonsterType", currentCM.type);	

				float mHealth = EditorGUILayout.FloatField("Health", currentCM.health);
				float mDefense = EditorGUILayout.FloatField("Defense", currentCM.defense);	
				float mAttackDamage = EditorGUILayout.FloatField("AttackDamage", currentCM.attackDamage);
				float mAttackSpeed = EditorGUILayout.FloatField("AttackSpeed", currentCM.attackSpeed);
				float mAttackRange = EditorGUILayout.FloatField("AttackRange", currentCM.attackRange);
				float mDetectRange = EditorGUILayout.FloatField("DetectRange", currentCM.detectRange);
				float mMoveSpeed = EditorGUILayout.FloatField("MoveSpeed", currentCM.moveSpeed);
				float mGiveSilver = EditorGUILayout.FloatField("GiveSilver", currentCM.giveSilver);
				
				int mMoveImageNumber = EditorGUILayout.IntField("MoveINum", currentCM.moveImageNumber);
				int mAttackImageNumber = EditorGUILayout.IntField("AttackINum", currentCM.attackImageNumber);

				csClassMonster.SpecialAbility specialAbility = 
					(csClassMonster.SpecialAbility)EditorGUILayout.EnumPopup("SpecialAbility", currentCM.ability);

				if (!itemDesc.Equals(currentCM.description) || 
					monsterType != currentCM.type || 
					mHealth != currentCM.health ||
					mDefense != currentCM.defense ||
					mAttackDamage != currentCM.attackDamage ||
					mAttackSpeed != currentCM.attackSpeed ||
					mAttackRange != currentCM.attackRange ||
					mDetectRange != currentCM.detectRange ||
					mMoveSpeed != currentCM.moveSpeed ||
					mGiveSilver != currentCM.giveSilver ||
					!specialAbility.Equals (currentCM.ability) ||
					mMoveImageNumber != currentCM.moveImageNumber ||
					mAttackImageNumber != currentCM.attackImageNumber)
				{
					currentCM.description = itemDesc;
					currentCM.type = monsterType;
					currentCM.health = mHealth;
					currentCM.defense = mDefense;
					currentCM.attackDamage = mAttackDamage;
					currentCM.attackSpeed = mAttackSpeed;
					currentCM.attackRange = mAttackRange;
					currentCM.detectRange = mDetectRange;
					currentCM.moveSpeed = mMoveSpeed;
					currentCM.giveSilver = mGiveSilver;
					currentCM.ability = specialAbility;
					currentCM.moveImageNumber = mMoveImageNumber;
					currentCM.attackImageNumber = mAttackImageNumber;
				}
				
				
			} // end of if (currentCM != null)
			
		} // end of else
		
	} // end of OnInspectorGUI
	
} // end of class csMonsterDBInspector
