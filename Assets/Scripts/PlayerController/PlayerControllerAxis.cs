using UnityEngine;
using System.Collections;

public class PlayerControllerAxis : PlayerController {
	
	// We'll assume movement on the X and Z axis, though I would prefer not to hardcode this in the future.
	protected override Vector3 GetMovementForce (float deltaTime) 
	{
		return new Vector3(
			Input.GetAxis("Horizontal") * speed * deltaTime,
			0.0f,
			Input.GetAxis("Vertical") * speed * deltaTime
		);
	}
}
