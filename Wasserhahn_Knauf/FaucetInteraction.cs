using UnityEngine;
using System.Collections;

public class FaucetInteraction : MonoBehaviour {
	

	private GameObject player;
	private Properties playerProperties;
//	private Transform water;
	private bool waterFlowing;

	private GameObject faucetWaterObject;



	void Awake() {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerProperties = player.GetComponent<Properties> ();

		//faucetWaterObject =  GameObject.FindGameObjectWithTag("Water");

	}

	void OnTriggerStay(Collider other) {

		// if player enters collider of trash can, and has a object.
		if (other.gameObject == player && playerProperties.hasObject == false) {
			// set current action to 
			playerProperties.currentPossibleAction = Properties.currentPossibleActionEnum.InteractWithFaucet.ToString ();
			playerProperties.currentFaucetWater = gameObject.transform.parent.Find ("Wasserhahn_Wasser");
			waterFlowing = playerProperties.currentFaucetWater.particleSystem.isPlaying;
		}
	}
	
	void OnTriggerExit(Collider other) {
		playerProperties.currentPossibleAction = "";
	}
	
	void Update() {

		if (playerProperties.currentPossibleAction.ToString () == Properties.currentPossibleActionEnum.InteractWithFaucet.ToString () && Input.GetMouseButtonDown(0)) {

				if (waterFlowing) {
						//rotate the knob

						// stop the water
						playerProperties.currentFaucetWater.particleSystem.Stop ();
						

						playerProperties.currentFaucetWater.audio.Stop();

						Debug.Log(playerProperties.currentFaucetWater);
						waterFlowing = false;
						playerProperties.score += 10;


				} else {
						playerProperties.currentFaucetWater.particleSystem.Play ();
						
						playerProperties.currentFaucetWater.audio.Play();

						Debug.Log(playerProperties.currentFaucetWater);
						waterFlowing = true;
						playerProperties.score -= 10;
				}

		}
	}
}	