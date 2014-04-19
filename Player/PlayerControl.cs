using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	private CharacterController charCtl;
	private Properties playerProperties;


	void Awake ()
	{
		// Setting up the references.
		charCtl = GetComponent<CharacterController>();
		playerProperties = gameObject.GetComponent<Properties> ();
	}

	
	
	void Update ()
	{	
		Vector3 horizontalVelocity = charCtl.velocity;
		horizontalVelocity = new Vector3(charCtl.velocity.x, 0, charCtl.velocity.z);
		float horizontalSpeed = horizontalVelocity.magnitude;
//		float verticalSpeed = charCtl.velocity.y;
//		float overallSpeed = charCtl.velocity.magnitude;

		AudioManagement(horizontalSpeed);




		// if interacting with windows
		if (playerProperties.currentPossibleAction.ToString () == Properties.currentPossibleActionEnum.InteractWithWindow.ToString () && Input.GetMouseButtonDown (0) && !playerProperties.currentWindow.audio.isPlaying) {
			playerProperties.currentWindow.GetComponent<WindowInteraction>().InteractWithWindow();
		}

		// if interacting with faucets
		if (playerProperties.currentPossibleAction.ToString () == Properties.currentPossibleActionEnum.InteractWithFaucet.ToString () && Input.GetMouseButtonDown (0) && !playerProperties.currentFaucet.audio.isPlaying) {
			playerProperties.currentFaucet.GetComponent<FaucetInteraction>().InteractWithFaucet();
		}
	}
	
	
	void AudioManagement (float horizontalSpeed) {

		// If the player is currently in the run state...
		if (horizontalSpeed > 0.5) {
			// ... and if the footsteps are not playing...
			if(!audio.isPlaying)
				audio.Play();

		} else
			// Otherwise stop the footsteps.
			audio.Stop();
	}
}
