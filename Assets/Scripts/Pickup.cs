using UnityEngine;
using System.Collections;

// Temporary! Eventually these will be created by code, not the editor.
public class Pickup : MonoBehaviour {

	public static int count = 0;
	//private static Vector3 center = new Vector3(0f, 0.5f, 0.0f);
	
	public int id;
	
	// I know, this is wrong. I'll eventually learn where to place code like this in Unity
	void Awake () {
		this.id = Pickup.count;
		//Pickup.count++;
	}
	
	
	public void CollectedBy(GameObject target) {
		this.collected = true;
		this.target = target;
		
		// We shall now destroy ourselves after a set amount of time
		//Destroy(this.gameObject, destroyTime);
	}
	
	// Will automatically move towards the target until it disappears
	public bool collected = false;
	public GameObject target;
	public float smoothing = 4.0f;
	public float destroyTime = 1.0f; // seconds
	
	public float minDistance = 0.5f;
	
	//void FixedUpdate() {
	void LateUpdate() {
		if (!target) return;
		
		//Debug.DrawLine(this.transform.position, target.transform.position, Color.yellow);
		//return;
		
		// Move towards the target as we slowly die.
		//Vector3 direction = (target.transform.position - this.transform.position);
		//this.transform.position = direction * smoothing * Time.deltaTime;
		
		// This is so wrong!!
		this.transform.position = Vector3.Lerp (this.transform.position, target.transform.position, smoothing * Time.deltaTime);
		
		if (Vector3.Distance(this.transform.position, target.transform.position) < minDistance) {
			target = null;
			Destroy(this.gameObject);
		}
	}
	
}
