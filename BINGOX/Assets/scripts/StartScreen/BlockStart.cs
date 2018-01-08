using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStart : MonoBehaviour {
	
	//click and drag variables
	private float dist;
	private Vector3 offset;
	private Transform toDrag;
	private Vector3 v3;

	//initial block position
	private Vector2 initialPosition;

	//selected menu start or setting
	private GameObject selectedMenu;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown(){

		//click and drag
		toDrag = transform;
		dist = transform.position.z - Camera.main.transform.position.z;
		v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
		v3 = Camera.main.ScreenToWorldPoint(v3);
		offset = toDrag.position - v3;
	}

	void OnMouseDrag(){
		//click and drag
		v3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		toDrag.position = v3 + offset;
	}
	void OnMouseUp(){
		if (selectedMenu != null) {
			if (selectedMenu.tag == "Maru") {
			} else if (selectedMenu.tag == "Batsu") {
				Application.LoadLevel ("LevelSelection");
			}
		} else {
			transform.position = initialPosition;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		selectedMenu = other.gameObject;
	}
	void OnTriggerExit2D(Collider2D other){
		selectedMenu = null;
	}
}
