using UnityEngine;
using System.Collections;

public class Randomization : MonoBehaviour {


	private Vector3 position;
	private GameObject trash;
	private string trashObject;

	//public int amountTrashObjects;

	private float minValueX = 1f;
	private float maxValueX = 35f;
	private float minValueY = 1f;
	private float maxValueY = 60f;


	private GameObject[] paperTrashObjects;
	private GameObject[] faucetWaterObjects;

	public AudioClip burblingOfWater;


	void Awake() {


		// randomized trash object placing
		paperTrashObjects = GameObject.FindGameObjectsWithTag ("PaperTrash");
		int i;

		for(i = 0; i < paperTrashObjects.Length; i++) {
			//trashObject = "MuellObjekt" + i;
			//Debug.Log(trashObject);
			//trash = GameObject.Find (trashObject);




			do {
				// calculate a random value for x and z axis, y axis stays 0
				position = new Vector3(Random.Range(minValueX,maxValueX), 0, Random.Range(minValueY, maxValueY));

				NavMeshHit hit;
				NavMesh.SamplePosition(position, out hit, 10, 1);
				position = hit.position;
		
			} while (position.x > 9999 && position.y > 9999);

			position.y = 2.5f;
			Debug.Log(position);
			paperTrashObjects[i].transform.position = position;

		}


		// randomized faucet water turning on
		faucetWaterObjects =  GameObject.FindGameObjectsWithTag("Water");
		int j;

		for(j = 0; j < faucetWaterObjects.Length; j++) {

			if(Random.Range(0, 10) >= 4 ) {
				faucetWaterObjects[j].particleSystem.Play();


				faucetWaterObjects[j].audio.clip = burblingOfWater;
				faucetWaterObjects[j].audio.Play();


			} else {
				faucetWaterObjects[j].particleSystem.Stop();
			}
		}

	}
	
}
