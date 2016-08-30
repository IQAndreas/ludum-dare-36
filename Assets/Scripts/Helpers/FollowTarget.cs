using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {

	private GameObject target;
	private Vector3 offset;
	
	public void SetTarget(GameObject target, float fixedY = 0.5f) {
		this.target = target;
		float offsetX = transform.position.x - target.transform.position.x;
		float offsetY = transform.position.y - fixedY;
		float offsetZ = transform.position.z - target.transform.position.z;
		offset = new Vector3(offsetX, offsetY, offsetZ);
	}
	
	void LateUpdate () {
		if (target) {
			//transform.position = target.transform.position + offset;
			//Fix the Y value, only move on the X and Z axis
			Vector3 v = new Vector3(target.transform.position.x, 0.0f, target.transform.position.z);
			transform.position = v + offset;
		}
	}
}
