using UnityEngine;
using System.Collections;

public class TrashPickup : MonoBehaviour {


	public AudioClip trashGrab;
	public AudioClip throwAway;

	private GameObject player;
	private Properties playerProperties;
	private GameObject trashObjectInactive;
	
	public Transform HoldPosition; //This is the point where the "hands" of the player would be
//	public float ThrowForce = 2f; //How strong the throw is. This assumes the picked object has a rigidbody component attached
	
	private Vector3 holdPositionHands;
	private Quaternion holdPositionHandsRotation;

	private GameObject currentObject;

	
	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerProperties = player.GetComponent<Properties> ();
		trashObjectInactive = GameObject.FindGameObjectWithTag ("PaperTrashInactive");



	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject == player && playerProperties.hasObject == false) {

			// ... play the clip at the position of the key...
			//AudioSource.PlayClipAtPoint(trashGrab, transform.position);
			audio.clip = trashGrab;
			audio.Play();

			playerProperties.carriedObject = gameObject;
			playerProperties.hasObject = true;
		}
	}
	
	void Update()
	{

		if (playerProperties.hasObject) {

			currentObject = playerProperties.carriedObject;

			holdPositionHands = HoldPosition.transform.position;
			holdPositionHands.y = 1.3f;
			holdPositionHandsRotation = HoldPosition.transform.rotation;
			//holdPositionHandsRotation = Quaternion.identity;


			currentObject.transform.position = holdPositionHands;
			currentObject.transform.rotation = holdPositionHandsRotation;

			if (Input.GetMouseButtonDown(0))
			{
				playerProperties.hasObject = false;
				audio.clip = throwAway;
				audio.Play();

				// if we are inside the trash can collider area
				if (playerProperties.currentPossibleAction.ToString() == Properties.currentPossibleActionEnum.ThrowToTrashCan.ToString()) {

					// Destroy the trash object, instatiate a inactive object with no trigger zone
					// transform it at position of "MuellPlatzierer" of the given trash can object.
					Destroy(currentObject);
					Instantiate(trashObjectInactive, playerProperties.currentTrashCanPos, holdPositionHandsRotation);
					//clone.rigidbody.AddRelativeForce(transform.forward * 0.1f, ForceMode.Impulse);
					playerProperties.score += 10;
				}
				//gameObject.rigidbody.AddRelativeForce(transform.forward * ThrowForce, ForceMode.Impulse);

			}
		}
	}
}
