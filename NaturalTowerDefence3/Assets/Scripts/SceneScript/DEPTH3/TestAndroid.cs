using UnityEngine;
using System.Collections;

public class TestAndroid : MonoBehaviour {
	
	public csUserSaveNLoad user;
	public Material testM;

	public static int switchCount = 0;
	
	void Start () {
	}

	void OnClick()
	{
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("PhotoAlbum");
            }
        }
	}


    public void InsertPicturePath (string path)
    {
		StartCoroutine(InsertPicturePathFile(path));
	}
	
	IEnumerator InsertPicturePathFile(string path)
	{
		
		if(csUserSaveNLoad.pictureFlag == false)
		{	
			//Debug.Log ("b");
			WWW www = new WWW(path);
			yield return www;
			
			testM.mainTexture = www.texture;
			user.userInfo.picturePath = path;
			user.SaveTest();
		}
		else
		{
			//Debug.Log ("a");
			WWW www = new WWW(user.userInfo.picturePath);
			yield return www;
			if(user.userInfo.picturePath == "" || user.userInfo.picturePath == null)
			{
				//Debug.Log ("c");
				testM.mainTexture = Resources.Load("TestImage/character_test") as Texture;
			}
			else
			{
				testM.mainTexture = www.texture;
				//Debug.Log ("d");
			}
			switchCount++;
			if(switchCount == 2)
				csUserSaveNLoad.pictureFlag = false;
		}
	}

	// Update is called once per frame
	void Update () {
		if(csUserSaveNLoad.pictureFlag == true)
			StartCoroutine(InsertPicturePathFile(user.userInfo.picturePath));
	}
}
