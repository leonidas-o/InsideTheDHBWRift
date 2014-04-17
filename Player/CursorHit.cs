using UnityEngine;
using System.Collections;

public class CursorHit : MonoBehaviour {
	
	public HeadLookController headLook;
	private float offset = 1.5f;
	private float eyeHeight;
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetKey(KeyCode.UpArrow))
			offset += Time.deltaTime;
		if (Input.GetKey(KeyCode.DownArrow))
			offset -= Time.deltaTime;
		
		Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(cursorRay, out hit)) {
			//transform.position = hit.point + offset * Vector3.up;
		}
		
		headLook.target = transform.position;
		// modify y variable to fix head tracking position
		eyeHeight = (float)headLook.target.y + 0.8f;
		//eyeHeight = eyeHeight + 1;
		//Debug.Log (headLook.target.y);
		//Debug.Log (eyeHeight);
		headLook.target.y = eyeHeight;
	}
}
