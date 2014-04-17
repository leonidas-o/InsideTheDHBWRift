using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {


	private int points;
	private TextMesh textMesh;

	
	void Awake() {
		textMesh = gameObject.GetComponent<TextMesh>();
	}
	
	void Update() {
		// update points variable with score from player properties
		points = gameObject.transform.parent.transform.parent.transform.parent.GetComponent<Properties> ().score;
	}
	
	void OnGUI() {
		textMesh.text = points.ToString ();
	}
}
