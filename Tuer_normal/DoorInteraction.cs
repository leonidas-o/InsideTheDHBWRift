using UnityEngine;
using System.Collections;

public class DoorInteraction : MonoBehaviour {


	public AudioClip doorOpen;
	public AudioClip doorClose;
	
	private GameObject player;
	private Properties playerProperties;
	
	
	void Awake() {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		//playerProperties = player.GetComponent<Properties> ();
	}

	void OnTriggerEnter(Collider other) {
		
		// if player enters collider of trash can, and has a object.
		if (other.gameObject == player) {
			
			//open the door automatically
			gameObject.GetComponent<Animator>().SetBool ("Open", true);
				
			// play window sound for opening
			gameObject.audio.clip = doorOpen;
			gameObject.audio.Play ();
				


		}
	}

	void OnTriggerExit(Collider other) {

		if (other.gameObject == player) {
		
			//close the door automatically
			gameObject.GetComponent<Animator> ().SetBool ("Open", false);
		
			// play window sound for closing
			gameObject.audio.clip = doorClose;
			gameObject.audio.Play ();

		}

	}
}
