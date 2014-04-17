using UnityEngine;
using System.Collections;

public class WindowInteraction : MonoBehaviour {


	public AudioClip windowOpen;
	public AudioClip windowClose;


	private GameObject player;
	private Properties playerProperties;
	//private bool windowOpened;
	private Animator anim;

	void Awake() {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerProperties = player.GetComponent<Properties> ();

		//anim = gameObject.GetComponent<Animator> ();


	}
	


	void OnTriggerEnter(Collider other) {

		// if player enters collider of trash can, and has a object.
		if (other.gameObject == player && playerProperties.hasObject == false) {

			// set current action to 
			playerProperties.currentPossibleAction = Properties.currentPossibleActionEnum.InteractWithWindow.ToString ();
			playerProperties.currentWindow = gameObject;

			Debug.Log(gameObject.transform.position);
		}

	}

	void OnTriggerExit(Collider other) {
		playerProperties.currentPossibleAction = "";
	}


	void Update() {

		if (playerProperties.currentPossibleAction.ToString () == Properties.currentPossibleActionEnum.InteractWithWindow.ToString () && Input.GetMouseButtonDown (0) && !playerProperties.currentWindow.audio.isPlaying) {
			if (playerProperties.currentWindow.GetComponent<Animator>().GetBool ("Open") == true) {

				playerProperties.currentWindow.GetComponent<Animator>().SetBool ("Open", false);
				//anim.SetBool ("Open", false);
				playerProperties.score += 10;


				playerProperties.currentWindow.audio.clip = windowClose;
				playerProperties.currentWindow.audio.Play ();
			} else {
				playerProperties.currentWindow.GetComponent<Animator>().SetBool ("Open", true);
				//anim.SetBool ("Open", true);
				playerProperties.score -= 10;
				playerProperties.currentWindow.audio.clip = windowOpen;
				playerProperties.currentWindow.audio.Play ();
			}
		}
	}
	
}
