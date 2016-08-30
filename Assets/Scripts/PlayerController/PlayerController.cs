using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Does not prevent movement, just disables the input.
	public bool canMove = false;

	// This will mean vastly different things to different input types
	public float speed = 1.0f;
	
	protected Rigidbody rigidBody;
	
	void Start () 
	{
		this.rigidBody = this.GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		if (!this.canMove) return;
		this.rigidBody.AddForce(this.GetMovementForce(Time.deltaTime));
	}
	
	protected virtual Vector3 GetMovementForce(float deltaTime) {
		// Override Plz...
		return Vector3.zero;
	}
	
}
