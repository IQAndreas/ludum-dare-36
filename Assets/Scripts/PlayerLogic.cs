using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLogic : MonoBehaviour 
{
	public int lives = 0;	
	public int pickupCount = 0;
	public string pickupTag = "Pickup";
	public bool outOfBounds = false;
	public bool hitEnemy = false;
	
	public Transform startLocation;
	public Collider bounds;
	
	public PlayerController controller;
	
	// Use this for initialization
	void Start () 
	{
		Debug.Log("Start");
		this.controller = this.GetComponent<PlayerController>();
		if (!this.controller) {
			Debug.Log("WARNING: There is no controller attached to the player. We will add a PlayerControllerAxis for now.");
			this.controller = this.gameObject.AddComponent<PlayerControllerAxis>() as PlayerController;
			this.controller.speed = 150.0f;
		}
		
		//count = 0;
		this.pickupCount = 0;
		this.outOfBounds = false;
		this.hitEnemy = false;
		
		// Wait for "ResetPlayer"
		this.canMove = false;
		this.visible = false;
	}
	
	public bool visible {
		get { return this.GetComponent<MeshRenderer>().enabled; }
		set { this.GetComponent<MeshRenderer>().enabled = value; }
	}
	public bool canMove {
		get { 
			if (!this.controller) return false;
			return this.controller.canMove;
		}
		set {
			Debug.Log("canMove=" + value);
			if (this.controller)
				this.controller.canMove = value;
		}
	}
	
	public void Init(Transform startLocation, Collider bounds) {
		this.startLocation = startLocation;
		this.bounds = bounds;
		//this.ResetPlayer();
	}
	
	public void ResetPlayer() {
		this.transform.position = startLocation.position;
		this.transform.rotation = startLocation.rotation;
		
		Rigidbody rigidbody = this.GetComponent<Rigidbody>();
		rigidbody.velocity = Vector3.zero;
    	rigidbody.angularVelocity = Vector3.zero;
    	
    	this.canMove = true;
    	this.visible = true;
	}
	
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log(other.gameObject);
		//Destroy(other.gameObject);
		
		// I'd prefer this logic elsewhere...
		if (other.gameObject.CompareTag(pickupTag));
		{
			Pickup pickup = other.gameObject.GetComponent<Pickup>() as Pickup;
			if (pickup && !pickup.collected) {
				//other.gameObject.SetActive(false);
				pickup.CollectedBy(this.gameObject);
				this.pickupCount++;
			}
		}
		
	}
	
	void OnTriggerExit( Collider other ) {
		if ( other == bounds ) {
			Debug.Log("Out of bounds");
			this.outOfBounds = true;
			// Disable input until we are back in bounds
			this.canMove = false;
		}
	}
	
	
	
}
