using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockButtonScript : MonoBehaviour {

	public int blockButtonIndex;

	//references
	private GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeActiveBlock(){
		gameController.activeBlockButton = gameObject;
		gameController.changeActiveBlock (blockButtonIndex);
	}
}
