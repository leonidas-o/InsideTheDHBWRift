using UnityEngine;
using System.Collections;

public class Properties : MonoBehaviour {
	
	private bool _hasObject = false;
	private GameObject _carriedObject = null;
	private string _currentPossibleAction = "";
	private Vector3 _currentTrashCanPos;
	private Transform _currentFaucetWater;

	private GameObject _currentWindow;
	private GameObject _currentFaucet;

	private int _score = 0;


	public enum currentPossibleActionEnum {
		ThrowToTrashCan,
		InteractWithFaucet,
		InteractWithWindow,
	}
		




	// hasObject properties if player already carrys an object
	public bool hasObject {
		get { return _hasObject; }
		set { _hasObject = value; }
	}

	public GameObject carriedObject {
		get { return _carriedObject; }
		set { _carriedObject = value; }
	}

	public string currentPossibleAction {
		get { return _currentPossibleAction; }
		set { _currentPossibleAction = value; }
	}

	public Vector3 currentTrashCanPos {
		get { return _currentTrashCanPos; }
		set { _currentTrashCanPos = value; }
	}

	public Transform currentFaucetWater {
		get { return _currentFaucetWater; }
		set { _currentFaucetWater = value; }
	}

	public GameObject currentFaucet {
		get { return _currentFaucet; }
		set { _currentFaucet = value; }
	}


	public GameObject currentWindow {
		get { return _currentWindow; }
		set { _currentWindow = value; }
	}




	public int score {
		get { return _score; }
		set { _score = value; }
	}


}
