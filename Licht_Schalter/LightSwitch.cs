using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {



	public AudioClip switchOn;
	public AudioClip switchOff;


	private GameObject player;
	private Properties playerProperties;
	
	
	void Awake() {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerProperties = player.GetComponent<Properties> ();
	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject == player && playerProperties.hasObject == false) {
			
			// set current action to 
			playerProperties.currentPossibleAction = Properties.currentPossibleActionEnum.InteractWithLight.ToString ();
			playerProperties.currentLight = gameObject;
		}
	}

	public void SwitchLights() {
		Debug.Log ("Switch lights now");


		if (playerProperties.currentLight.transform.parent.light.enabled) {
			playerProperties.currentLight.audio.clip = switchOff;
			playerProperties.score += 10;
		} else {
			playerProperties.currentLight.audio.clip = switchOn;
			playerProperties.score -= 10;
		}
		playerProperties.currentLight.audio.Play ();
		playerProperties.currentLight.transform.parent.light.enabled = !playerProperties.currentLight.transform.parent.light.enabled;

	}
}
