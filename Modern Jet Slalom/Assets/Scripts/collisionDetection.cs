using UnityEngine;
using System.Collections;

public class collisionDetection : MonoBehaviour {


	public static bool crashed = false;
	public static float timeElapsed = 0f;
	public static int finalScore = 0;

	private bool GUIEnabled = false;
	private GUIStyle myGUIStyle1 = new GUIStyle();
	private GUIStyle myGUIStyle2 = new GUIStyle();
	private GUIStyle myGUIStyle3;


	void OnGUI() {
		myGUIStyle1.fontSize = 50;
		myGUIStyle1.normal.textColor = Color.white;

		myGUIStyle3 = new GUIStyle(GUI.skin.button);
		myGUIStyle3.fontSize = 20;

		GUI.Label (new Rect (10, 5, 100, 50), "Score: " + Mathf.Floor(timeElapsed), myGUIStyle1);
		if(GUIEnabled) {
			myGUIStyle2.fontSize = 50;
			myGUIStyle2.normal.textColor = Color.white;
			GUI.Label (new Rect (Screen.width/2-90, Screen.height/2, 100, 50), "Score: " + finalScore, myGUIStyle2);

			if(GUI.Button(new Rect(Screen.width/2.5f, Screen.height/3, Screen.width/5, Screen.height/10), "Restart Game", myGUIStyle3)) {
				timeElapsed = 0;
				Application.LoadLevel ("Level 1");
			}
		}
	}


	public GameObject jet;

	void OnTriggerEnter(Collider collisionInfo)
	{
		//Debug.Log ("HELLO");
		//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//cube.transform.localScale = collisionInfo.transform.localScale;
		//cube.transform.position
		//Instantiate (collisionInfo.gameObject, collisionInfo.transform.position + new Vector3(0, 0, 100), collisionInfo.transform.rotation );
		//Destroy (collisionInfo.gameObject);

		//Application.LoadLevel (Application.loadedLevel);
		//OnGUI();
		//Destroy(jet);

		GUIEnabled = true;
		crashed = true;
		finalScore = Mathf.FloorToInt(timeElapsed);

	}


	/*
	void OnCollisionEnter(Collision collisionInfo)
	{
		Debug.Log ("HEllo");
	}
	*/
}
