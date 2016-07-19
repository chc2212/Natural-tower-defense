using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(csStageDB))]
public class csStageInspector : Editor{	
	// I know that this code start...
	
	static int mStageIndex = 0;
	
	bool mConfirmMapListDelete = false;
	bool mConfirmstageListDelete = false;
	
	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		csStageDB db = target as csStageDB;
		NGUIEditorTools.DrawSeparator();
		
		csStageManager currentSM = null;
		csClassStage currentCS = null;
		
		// stage check
		if(db.mapList == null || db.mapList.Count == 0)
		{
			mStageIndex = 0;
		}
		
		else
		{
			mStageIndex = Mathf.Clamp(mStageIndex, 0, db.mapList.Count - 1);
			currentSM = db.mapList[mStageIndex];
		}
		
		// mapList check
		if(db.mapList.Count != 0)
		{
			if (db.mapList[mStageIndex].stageList == null || db.mapList[mStageIndex].stageList.Count == 0)
			{
				db.mapList[mStageIndex].stageIndex = 0;
			}
			else
			{
				db.mapList[mStageIndex].stageIndex = Mathf.Clamp(db.mapList[mStageIndex].stageIndex, 0, db.mapList[mStageIndex].stageList.Count - 1);
				currentCS = db.mapList[mStageIndex].stageList[db.mapList[mStageIndex].stageIndex];
			}
		}

		// delete button click after..
		if (mConfirmMapListDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete 'MapList : " + currentSM.type +"'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmMapListDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					db.mapList.RemoveAt(mStageIndex);
					mConfirmMapListDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}
		
		else if (mConfirmstageListDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete '" + currentCS.name + "'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmstageListDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					db.mapList[mStageIndex].stageList.RemoveAt(db.mapList[mStageIndex].stageIndex);
					mConfirmstageListDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}

		
		else if(!mConfirmstageListDelete && !mConfirmMapListDelete)
		{
			
			// "ADD MAP_LIST" button
			GUI.backgroundColor = Color.green;
			
			NGUIEditorTools.DrawSeparator();
			
			if (GUILayout.Button ("ADD MAP_LIST"))
			{
				// MAP_LIST ADD
				csStageManager im = new csStageManager();
				db.mapList.Add(im);
				mStageIndex = db.mapList.Count - 1;
				
				currentSM = im;
			}

			if(currentSM != null)
			{
				NGUIEditorTools.DrawSeparator();
				// "DELETE MAP_LIST" button
				GUILayout.BeginHorizontal();
				{
					GUI.backgroundColor = Color.red;
					
					if (GUILayout.Button("DELETE MAP_LIST"))
					{
						mConfirmMapListDelete = true;
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
					GUILayout.Label("/ " + db.mapList.Count, GUILayout.Width(40f));
					if (mStageIndex + 1 == db.mapList.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) {++mStageIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
		
				// stage check because mStageIndex data out of range
				if(db.mapList == null || db.mapList.Count == 0)
				{
					mStageIndex = 0;
				}
				
				else
				{
					mStageIndex = Mathf.Clamp(mStageIndex, 0, db.mapList.Count - 1);
					currentSM = db.mapList[mStageIndex];
				}
				
				NGUIEditorTools.DrawSeparator();
			
				csStageManager.MapType mapType = 
					(csStageManager.MapType)EditorGUILayout.EnumPopup("MapList", currentSM.type);
				
				if(!mapType.Equals(currentSM.type))	currentSM.type = mapType;
			}
			NGUIEditorTools.DrawSeparator();
			
			GUI.backgroundColor = Color.green;
			
			if(db.mapList.Count != 0)
			{
				if (GUILayout.Button("ADD Stage"))
				{
					csClassStage cm = new csClassStage();
					db.mapList[mStageIndex].stageList.Add(cm);
					db.mapList[mStageIndex].stageIndex = db.mapList[mStageIndex].stageList.Count - 1;
					
					cm.name = "New Stage";
					cm.description = "Stage Description";
					
					currentCS = cm;
					//Debug.Log ("hihihi");
				}
			}

			GUI.backgroundColor = Color.white;
			
			
			if (currentCS != null)
			{
				NGUIEditorTools.DrawSeparator();
				
				// << 1 / 1 >>
				GUILayout.BeginHorizontal();
				{
					if (db.mapList[mStageIndex].stageIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) {--db.mapList[mStageIndex].stageIndex; }
					GUI.color = Color.white;
					db.mapList[mStageIndex].stageIndex = EditorGUILayout.IntField(db.mapList[mStageIndex].stageIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.mapList[mStageIndex].stageList.Count, GUILayout.Width(40f));
					if (db.mapList[mStageIndex].stageIndex + 1 == db.mapList[mStageIndex].stageList.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) {++db.mapList[mStageIndex].stageIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();
				
				NGUIEditorTools.DrawSeparator();
				
				// Stage name and delete Stage button
				GUILayout.BeginHorizontal();
				{
					GUILayout.Label("StageName", GUILayout.Width (82f));
					string stageName = EditorGUILayout.TextField(currentCS.name);
		
					GUI.backgroundColor = Color.red;
		
					if (GUILayout.Button("Delete", GUILayout.Width(55f)))
					{
						mConfirmstageListDelete = true;
					}
					GUI.backgroundColor = Color.white;
					
					if (!stageName.Equals(currentCS.name))
					{
						currentCS.name = stageName;
					}
				}
				GUILayout.EndHorizontal();
				
				string stageDesc = GUILayout.TextArea(currentCS.description, 100, GUILayout.Height(50f));

				int mSetWave = EditorGUILayout.IntField("Wave", currentCS.setWave);
				int mSetMonsterNumberPerWave = EditorGUILayout.IntField("MonNumPerWave", currentCS.setMonsterNumberPerWave);
				float mTimer = EditorGUILayout.FloatField("Timer", currentCS.monsterTimer);
				int mWaveTimer = EditorGUILayout.IntField("WaveTimer", currentCS.waveTimer);
				int mSemiBossNumber = EditorGUILayout.IntField("SemiBossNum", currentCS.semiBossNumber);
				int mSemiBossAfterWave = EditorGUILayout.IntField("SemiWave", currentCS.semiBossAfterWave);
				int mSemiBossTimer = EditorGUILayout.IntField("SBTimer", currentCS.semiBossTimer);
				int mBossNumber = EditorGUILayout.IntField("BossNumber", currentCS.bossNumber);
				int mBossAfterWave = EditorGUILayout.IntField("BossWave", currentCS.bossAfterWave);
				int mBossTimer = EditorGUILayout.IntField("BossTimer", currentCS.bossTimer);
				
				NGUIEditorTools.DrawSeparator();
				
				string[] mItemName = new string[currentCS.itemName.Length];
				int[] mGetItemPer = new int[currentCS.getItemPer.Length];
				
				for(int i = 0; i < currentCS.itemName.Length; i++)
				{
					mItemName[i] = EditorGUILayout.TextField("ItemName"+(i+1), currentCS.itemName[i]);
					mGetItemPer[i] = EditorGUILayout.IntField("GetPersent"+(i+1), currentCS.getItemPer[i]);
				}
				
				if (!stageDesc.Equals(currentCS.description) || 
					mSetWave != currentCS.setWave ||
					mSetMonsterNumberPerWave != currentCS.setMonsterNumberPerWave ||
					mTimer != currentCS.monsterTimer ||
					mWaveTimer != currentCS.waveTimer ||
					mSemiBossNumber != currentCS.semiBossNumber ||
					mSemiBossAfterWave != currentCS.semiBossAfterWave ||
					mSemiBossTimer != currentCS.semiBossTimer ||
					mBossNumber != currentCS.bossNumber ||
					mBossAfterWave != currentCS.bossAfterWave ||
					mBossTimer != currentCS.bossTimer)
				{
					currentCS.description = stageDesc;
					currentCS.setWave = mSetWave;
					currentCS.setMonsterNumberPerWave = mSetMonsterNumberPerWave;
					currentCS.monsterTimer = mTimer;
					currentCS.waveTimer = mWaveTimer;
					currentCS.semiBossNumber = mSemiBossNumber;
					currentCS.semiBossAfterWave = mSemiBossAfterWave;
					currentCS.semiBossTimer = mSemiBossTimer;
					currentCS.bossNumber = mBossNumber;
					currentCS.bossAfterWave = mBossAfterWave;
					currentCS.bossTimer = mBossTimer;
				}
				
				for(int i = 0; i < currentCS.itemName.Length; i++)
				{
					if(!mItemName[i].Equals(currentCS.itemName[i]) ||
						mGetItemPer[i] != currentCS.getItemPer[i])
					{
						currentCS.itemName[i] = mItemName[i];
						currentCS.getItemPer[i] = mGetItemPer[i];
					}
				}

			} // end of if (currentCS != null)
			
		} // end of else
		
	} // end of OnInspectorGUI
	
} // end of class csStageDBInspector
