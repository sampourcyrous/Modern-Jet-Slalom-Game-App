using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	void OnGUI() {
		GUIStyle myGUIStyle1 = new GUIStyle(GUI.skin.button);
		myGUIStyle1.fontSize = 25;

		if(GUI.Button(new Rect(Screen.width/2.5f, Screen.height/3, Screen.width/5, Screen.height/10), "Start Game", myGUIStyle1)) {
			Application.LoadLevel ("Level 1");
		}
	}

}
