using UnityEngine;
using System.Collections;

public class AccerlerometerScriptMenu : MonoBehaviour {

	void setTransformY(float n) {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + n);
	}

	// Update is called once per frame
	void Update () {
		// Move jet at constant speed
		float speed = 2f;
		setTransformY(speed);

	}
}
