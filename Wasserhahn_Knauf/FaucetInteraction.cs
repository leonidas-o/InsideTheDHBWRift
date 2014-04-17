using UnityEngine;
using System.Collections;

public class FaucetInteraction : MonoBehaviour {
	

	private GameObject player;
	private Properties playerProperties;
//	private bool waterFlowing;
	private Transform currentFaucetWater;
	private GameObject currentFaucetKnob;




	void Awake() {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerProperties = player.GetComponent<Properties> ();

	}

	void OnTriggerEnter(Collider other) {

		// if player enters collider of trash can, and has a object.
		if (other.gameObject == player && playerProperties.hasObject == false) {
			// set current action to 
			playerProperties.currentPossibleAction = Properties.currentPossibleActionEnum.InteractWithFaucet.ToString ();

			playerProperties.currentFaucet = gameObject;
//			waterFlowing = playerProperties.currentFaucet.transform.parent.Find("Wasserhahn_Wasser").particleSystem.isPlaying;

		}
	}

	void OnTriggerStay (Collider other) {
		// if player triggers collider renew playerProperties.currentPossibleAction to avoid problems with entering and exiting triggerzone in the same time
		playerProperties.currentPossibleAction = Properties.currentPossibleActionEnum.InteractWithFaucet.ToString ();


	}
	
	void OnTriggerExit(Collider other) {
		playerProperties.currentPossibleAction = "";
	}
	
	void Update() {

		if (playerProperties.currentPossibleAction.ToString () == Properties.currentPossibleActionEnum.InteractWithFaucet.ToString () && Input.GetMouseButtonDown(0)) {


			if (playerProperties.currentFaucet.GetComponent<Animator>().GetBool ("Open")) {
				// stop the water
				playerProperties.currentFaucet.GetComponent<Animator>().SetBool ("Open", false);
				playerProperties.currentFaucet.transform.parent.Find("Wasserhahn_Wasser").particleSystem.Stop ();
				playerProperties.currentFaucet.transform.parent.Find("Wasserhahn_Wasser").audio.Stop();
//				waterFlowing = false;

				playerProperties.score += 10;
			
			} else {

				playerProperties.currentFaucet.GetComponent<Animator>().SetBool ("Open", true);
				playerProperties.currentFaucet.transform.parent.Find("Wasserhahn_Wasser").particleSystem.Play ();
				playerProperties.currentFaucet.transform.parent.Find("Wasserhahn_Wasser").audio.Play();
//				waterFlowing = true;

				playerProperties.score -= 10;
			}
		}
	}
}	