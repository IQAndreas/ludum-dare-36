using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	public bool running = true;

	public float rotateX = 0.0f;
	public float rotateY = 0.0f;
	public float rotateZ = 0.0f;

	// Update is called once per frame
	void Update () {
		if (this.running) {
			transform.Rotate(new Vector3(rotateX, rotateY, rotateZ) * Time.deltaTime);
		}
	}
}
