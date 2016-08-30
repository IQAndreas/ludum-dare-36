using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerTilt : PlayerController {

	// Three times as fast to make up for the fact that you can only tilt it 1/3 of the way.
	//public float speed = 3.0f;

	public float maxTiltX = 0.35f;
	public float maxTiltY = 0.35f;
	
	// In my testing, X and Y meant you could hold the device flat to control the ball
	//  but what is counter-intuative is that you tilt in Y to control Z 
	//public float maxTiltZ = 0.35f; 
	
	// We'll assume movement on the X and Z axis, though I would prefer not to hardcode this in the future.
	protected override Vector3 GetMovementForce (float deltaTime) 
	{
		return new Vector3(
			Mathf.Clamp(Input.acceleration.x, -maxTiltX, maxTiltX) * speed * Time.deltaTime,
			0.0f,
			Mathf.Clamp(Input.acceleration.y, -maxTiltY, maxTiltY) * speed * Time.deltaTime
		);
	}
	
	// -------- OLD STUFF ---------
	
	//public Text debugText;
	
	/*void FixedUpdate (){
	
		/if (debugText != null)
		{
			string d = "Screen: " + Screen.width + " x " + Screen.height + " " + (Screen.fullScreen ? "(fullscreen)" : "(windowed)") + "\n";
			d += "Orientation: " + Screen.orientation + ";  DPI: " + Screen.dpi + "\n";
			d += "Framerate: " + Screen.orientation + ";  DeltaTime: " + Time.deltaTime + "\n";
			d += "\n";
			d += "Acceleration X: " + System.Math.Round(Input.acceleration.x, 2) + "\n";
			d += "Acceleration Y: " + System.Math.Round(Input.acceleration.y, 2) + "\n";
			d += "Acceleration Z: " + System.Math.Round(Input.acceleration.z, 2) + "\n";
			
			debugText.text = d;
		}
	
		/*Vector3 movement = new Vector3(
			Mathf.Clamp(Input.acceleration.x, -maxTiltX, maxTiltX) * speed * Time.deltaTime,
			Mathf.Clamp(Input.acceleration.y, -maxTiltY, maxTiltY) * speed * Time.deltaTime,
			Mathf.Clamp(Input.acceleration.z, -maxTiltZ, maxTiltZ) * speed * Time.deltaTime
		);* /
		
		// Tilt in X to control X (duh) 
		// Tilt in Y to control Z (wat)
		Vector3 movement = new Vector3(
			Mathf.Clamp(Input.acceleration.x, -maxTiltX, maxTiltX) * speed * Time.deltaTime,
			0.0f,
			Mathf.Clamp(Input.acceleration.y, -maxTiltY, maxTiltY) * speed * Time.deltaTime
		);
		
		//Debug.Log(Input.acceleration);
		//Debug.Log(movement);
		
		rb.AddForce(movement);
	}*/
}
