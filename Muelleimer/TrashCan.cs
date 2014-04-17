using UnityEngine;
using System.Collections;

public class TrashCan : MonoBehaviour {


	private GameObject player;
	private Properties playerProperties;
	private Transform trashPositioner;

	void Awake() {
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerProperties = player.GetComponent<Properties> ();
		trashPositioner = transform.Find ("MuellPlatzierer");
		//GameObject.FindGameObjectWithTag("TrashPositioner");
	}

	void OnTriggerEnter(Collider other) {
		// if player enters collider of trash can, and has a object.
		if (other.gameObject == player && playerProperties.hasObject == true) {
			// set current action to 
			playerProperties.currentPossibleAction = Properties.currentPossibleActionEnum.ThrowToTrashCan.ToString();
			//playerProperties.currentTrashCanPos = gameObject.transform.position;
			playerProperties.currentTrashCanPos = trashPositioner.transform.position;


			Debug.Log ("throw to trash state at position " + playerProperties.currentTrashCanPos);
		}
	}

	void OnTriggerExit(Collider other) {
		playerProperties.currentPossibleAction = "";
	}

}
