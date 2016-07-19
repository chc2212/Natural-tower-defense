using UnityEngine;
using System.Collections;

public class csPlayerPrefsTest : MonoBehaviour {

    void Start () {

        // this array should be filled before you can use EncryptedPlayerPrefs :

        EncryptedPlayerPrefs.keys=new string[5];

        EncryptedPlayerPrefs.keys[0]="23Wrudre";
        EncryptedPlayerPrefs.keys[1]="SP9DupHa";
        EncryptedPlayerPrefs.keys[2]="frA5rAS3";
        EncryptedPlayerPrefs.keys[3]="tHat2epr";
        EncryptedPlayerPrefs.keys[4]="jaw3eDAs";

		
        EncryptedPlayerPrefs.SetInt("test_int", 130);
		int a = EncryptedPlayerPrefs.GetInt("test_int");
		Debug.Log (a);
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
