using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {
	
	public Transform target;
	public float smoothing = 5f;
	
	void FixedUpdate() {
		if (!target) return;
		
		Vector3 direction = (target.position - transform.position);
		transform.position = direction * smoothing * Time.deltaTime;
	}
}
