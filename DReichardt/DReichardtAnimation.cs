using UnityEngine;
using System.Collections;

public class DReichardtAnimation : MonoBehaviour {

	public float deadZone = 5f;             // The number of degrees for which the rotation isn't controlled by Mecanim.
	public float chaseSpeed = 4f;           // The nav mesh agent's speed when chasing.	
//	public float chaseWaitTime = 2f;        // The amount of time to wait when the last sighting is reached.
	public float chaseStopDistance = 3f;
	
	private Transform player;               // Reference to the player's transform.
	private DReichardtSight dReichardtSight;// Reference to the DReichardtSight script.
	private NavMeshAgent nav;               // Reference to the nav mesh agent.
	private Animator anim;                  // Reference to the Animator.
	private HashIDs hash;                   // Reference to the HashIDs script.
	private AnimatorSetup animSetup;        // An instance of the AnimatorSetup helper class.


	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		dReichardtSight = GetComponent<DReichardtSight>();
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		hash = GameObject.FindGameObjectWithTag("GameController").GetComponent<HashIDs>();

		// Making sure the rotation is controlled by Mecanim.
		nav.updateRotation = false;
		
		// Creating an instance of the AnimatorSetup class and calling it's constructor.
		animSetup = new AnimatorSetup(anim, hash);

		// We need to convert the angle for the deadzone from degrees to radians.
		deadZone *= Mathf.Deg2Rad;
	}
	
	
	void Update () 
	{
		// Calculate the parameters that need to be passed to the animator component.
		NavAnimSetup();
	}
	
	
	void OnAnimatorMove ()
	{
		// Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.
		nav.velocity = anim.deltaPosition / Time.deltaTime;
		
		// The gameobject's rotation is driven by the animation's rotation.
		transform.rotation = anim.rootRotation;
	}
	
	
	void NavAnimSetup ()
	{
		// Create the parameters to pass to the helper function.
		float speed;
		float angle;
		
		// If the player is in sight...
		if(dReichardtSight.playerInSight)
		{

			// Create a vector from the avatar to the last sighting of the player.
			Vector3 sightingDeltaPos = dReichardtSight.personalLastSighting - transform.position;
			
			
			if (sightingDeltaPos.sqrMagnitude < chaseStopDistance) {
				speed = 0;
				angle = 0;

			} else {

				// ... the avatar should run...
				speed = chaseSpeed;
				// ... and the angle to turn through is towards the player.
				angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
			}

		}
		else
		{
			// Otherwise the speed is a projection of desired velocity on to the forward vector...
			speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
			
			// ... and the angle is the angle between forward and the desired velocity.
			angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
			
			// If the angle is within the deadZone...
			if(Mathf.Abs(angle) < deadZone)
			{
				// ... set the direction to be along the desired direction and set the angle to be zero.
				transform.LookAt(transform.position + nav.desiredVelocity);
				angle = 0f;
			}
		}
		
		// Call the Setup function of the helper class with the given parameters.
		animSetup.Setup(speed, angle);
	}
	
	
	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
	{
		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero)
			// ... the angle between them is 0.
			return 0f;
		
		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle(fromVector, toVector);
		
		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross(fromVector, toVector);
		
		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		
		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;
		
		return angle;
	}
}
