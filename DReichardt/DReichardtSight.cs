﻿using UnityEngine;
using System.Collections;

public class DReichardtSight : MonoBehaviour {

	public float fieldOfViewAngle = 170f;           // Number of degrees, centred on forward, for the avatar see.
	public bool playerInSight;                      // Whether or not the player is currently sighted.
	public Vector3 personalLastSighting;            // Last place this enemy spotted the player.

	private NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
	private SphereCollider col;                     // Reference to the sphere collider trigger component.
	private Animator anim;                          // Reference to the Animator.
	private LastPlayerSighting lastPlayerSighting;  // Reference to last global sighting of the player.
	private GameObject player;                      // Reference to the player.
//	private Animator playerAnim;                    // Reference to the player's animator component.
//	private PlayerHealth playerHealth;              // Reference to the player's health script.
	private HashIDs hash;                           // Reference to the HashIDs.
	private Vector3 previousSighting;               // Where the player was sighted last frame.


	void Awake() {
		// Setting up the references.
		nav = GetComponent<NavMeshAgent>();
		col = GetComponent<SphereCollider>();
		anim = GetComponent<Animator>();
		lastPlayerSighting = GameObject.FindGameObjectWithTag("GameController").GetComponent<LastPlayerSighting>();
		player = GameObject.FindGameObjectWithTag("Player");
//		playerAnim = player.GetComponent<Animator>();
//		playerHealth = player.GetComponent<PlayerHealth>();
		hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();


		// Set the personal sighting and the previous sighting to the reset position.
		personalLastSighting = lastPlayerSighting.resetPosition;
		previousSighting = lastPlayerSighting.resetPosition;
	}


	void Update () {
		// If the last global sighting of the player has changed...
		if(lastPlayerSighting.position != previousSighting)
			// ... then update the personal sighting to be the same as the global sighting.
			personalLastSighting = lastPlayerSighting.position;
		
		// Set the previous sighting to the be the sighting from this frame.
		previousSighting = lastPlayerSighting.position;
		
	}


	void OnTriggerStay (Collider other)	{
		// If the player has entered the trigger sphere...
		if(other.gameObject == player) {
			// By default the player is not in sight.
			playerInSight = false;
			
			// Create a vector from the enemy to the player and store the angle between it and forward.
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			
			// If the angle between forward and where the player is, is less than half the angle of view...
			if(angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;

				// ... and if a raycast towards the player hits something...
				if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)) {

					// ... and if the raycast hits the player...
					if(hit.collider.gameObject == player) {
						// ... the player is in sight.
						playerInSight = true;
						
						// Set the last global sighting is the players current position.
						lastPlayerSighting.position = player.transform.position;
					}
				}
			}

			// Store the name hashes of the current states.
//			int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).nameHash;
//			int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).nameHash;
			


		}
	}


	void OnTriggerExit (Collider other) {
		// If the player leaves the trigger zone...
		if(other.gameObject == player)
			// ... the player is not in sight.
			playerInSight = false;
	}

	

}
