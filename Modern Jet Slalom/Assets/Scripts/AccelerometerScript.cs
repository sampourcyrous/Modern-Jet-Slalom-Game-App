using UnityEngine;
using System.Collections;

public class AccelerometerScript : collisionDetection {

	public GameObject camera;
	public GameObject light;
	private float speed = 2f;
	private int previousTime = 0;

	/*
	void setTransformX(float n) {
		transform.position = new Vector3(n, transform.position.y, transform.position.z);
		camera.transform.position = new Vector3(n, camera.transform.position.y, camera.transform.position.z);
	}
	*/

	void setTransformY(float n) {
		transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + n);
		camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + n);
		light.transform.position = new Vector3(light.transform.position.x, light.transform.position.y, light.transform.position.z + n);
	}

	/*
	// not mapped to accelerometer
	void updateRotationZandPositionX(float n) {
		var rotz = transform.rotation.z;

		if ((rotz > -0.5 || n > 0) && (rotz < 0.5 || n < 0)) {
			transform.Rotate (Vector3.forward * 2 * 75 * Time.deltaTime * n);
		}
	}
	*/


	// mapped to accelerometer
	void updateRotationZandPositionX(float n) {
		if(n < -0.5) {
			n = -0.5f;
		} else if(n > 0.5) {
			n = 0.5f;
		}

		Transform from = transform;
		Quaternion to = from.rotation;
		to.z = n;
		transform.rotation = Quaternion.Lerp(from.rotation, to, Time.deltaTime * 5f);
	}


	/*
	void setRotationX(float n) {
		transform.Rotate(new Vector3(transform.rotation.x, transform.rotation.y, 0));
	}
	*/

	void Start() {
		crashed = false;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (speed);
		if(!crashed) {
			timeElapsed += Time.deltaTime;

			// Move jet at constant speed
			if(Mathf.Floor(timeElapsed) >= previousTime + 10 && speed < 5) {
				speed *= 1.2f;
				previousTime += 10;
			}

			setTransformY(speed);

			// Move jet across terrain at a speed relative to the z rotation
			transform.position = new Vector3(transform.position.x - 4*transform.rotation.z, transform.position.y, transform.position.z);
			camera.transform.position = new Vector3(camera.transform.position.x - 4*transform.rotation.z, camera.transform.position.y, camera.transform.position.z);
			//light.transform.position = new Vector3(light.transform.position.x - 4*transform.rotation.z, light.transform.position.y, light.transform.position.z);

			// Update z rotation and x position of jet and camera
			updateRotationZandPositionX(-Input.acceleration.x);

			// Match camera rotation to jet
			//camera.transform.rotation = transform.rotation;
			camera.transform.rotation = new Quaternion(transform.rotation.x/2, transform.rotation.y/2, transform.rotation.z/2, 1);
		}

	}
}
