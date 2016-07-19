using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

static public class UpgradeWidgets
{
	[MenuItem("NGUI/Upgrade Widgets")]
	static public void Upgrade ()
	{
		int count = 0;
		List<UISlicedSprite> sliced = NGUIEditorTools.FindAll<UISlicedSprite>();
		List<UIFilledSprite> filled = NGUIEditorTools.FindAll<UIFilledSprite>();
		List<UITiledSprite> tiled = NGUIEditorTools.FindAll<UITiledSprite>();

		int spriteID, inputID;
		SerializedObject ob;

		// Determine the object instance ID of the UISprite class
		{
			GameObject go = EditorUtility.CreateGameObjectWithHideFlags("Temp", HideFlags.HideAndDontSave);
			
			UISprite uiSprite = go.AddComponent<UISprite>();
			ob = new SerializedObject(uiSprite);
			spriteID = ob.FindProperty("m_Script").objectReferenceInstanceIDValue;
			
			UIInput uiInput = go.AddComponent<UIInput>();
			ob = new SerializedObject(uiInput);
			inputID = ob.FindProperty("m_Script").objectReferenceInstanceIDValue;
			
			NGUITools.DestroyImmediate(go);
		}

		// Upgrade sliced sprites
		for (int i = 0; i < sliced.Count; ++i)
		{
			UIWidget w = sliced[i];

			if (w != null)
			{
				++count;
				ob = new SerializedObject(w);
				ob.Update();
				ob.FindProperty("m_Script").objectReferenceInstanceIDValue = spriteID;
				ob.ApplyModifiedProperties();

				ob.Update();
				ob.FindProperty("mType").intValue = (int)UISprite.Type.Sliced;
				ob.ApplyModifiedProperties();
			}
		}

		// Upgrade filled sprites
		for (int i = 0; i < filled.Count; ++i)
		{
			UIWidget w = filled[i];

			if (w != null)
			{
				++count;
				ob = new SerializedObject(w);
				ob.Update();
				ob.FindProperty("m_Script").objectReferenceInstanceIDValue = spriteID;
				ob.ApplyModifiedProperties();

				ob.Update();
				ob.FindProperty("mType").intValue = (int)UISprite.Type.Filled;
				ob.ApplyModifiedProperties();
			}
		}

		// Upgrade tiled sprites
		for (int i = 0; i < tiled.Count; ++i)
		{
			UIWidget w = tiled[i];

			if (w != null)
			{
				++count;
				ob = new SerializedObject(w);
				ob.Update();
				ob.FindProperty("m_Script").objectReferenceInstanceIDValue = spriteID;
				ob.ApplyModifiedProperties();

				ob.Update();
				ob.FindProperty("mType").intValue = (int)UISprite.Type.Tiled;
				ob.ApplyModifiedProperties();
			}
		}

		List<UIInputSaved> saved = NGUIEditorTools.FindAll<UIInputSaved>();

		// Upgrade UI inputs
		for (int i = 0; i < saved.Count; ++i)
		{
			UIInputSaved inp = saved[i];

			if (inp != null)
			{
				++count;
				ob = new SerializedObject(inp);
				ob.Update();
				ob.FindProperty("m_Script").objectReferenceInstanceIDValue = inputID;
				ob.ApplyModifiedProperties();
			}
		}

		Debug.Log(count + " widgets upgraded");
	}
}
