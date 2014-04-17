using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private CharacterController charCtl;

	void Awake ()
	{
		// Setting up the references.
		charCtl = GetComponent<CharacterController>();
	}

	
	
	void Update ()
	{	
		Vector3 horizontalVelocity = charCtl.velocity;
		horizontalVelocity = new Vector3(charCtl.velocity.x, 0, charCtl.velocity.z);
		float horizontalSpeed = horizontalVelocity.magnitude;
//		float verticalSpeed = charCtl.velocity.y;
//		float overallSpeed = charCtl.velocity.magnitude;

		AudioManagement(horizontalSpeed);
	}
	
	
	void AudioManagement (float horizontalSpeed) {


		// If the player is currently in the run state...
		if (horizontalSpeed > 0.5) {
			// ... and if the footsteps are not playing...
			if(!audio.isPlaying)
				audio.Play();

			if (horizontalSpeed > 1) {
				// pitch sound to make it sound faster

			}


		} else
			// Otherwise stop the footsteps.
			audio.Stop();
	}
}
