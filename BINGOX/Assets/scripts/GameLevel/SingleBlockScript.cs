using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBlockScript : MonoBehaviour {

	// Use this for initialization
	public bool isInsideBox = false;
	public GameObject collidingSingleBlock;

	public ShapedBlock parent;

	void Awake(){
		parent = gameObject.transform.parent.gameObject.GetComponent<ShapedBlock> ();
	}
	void Start () {
		//parent = gameObject.transform.parent.gameObject.GetComponent<ShapedBlock> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public GameObject setCollidingSingleBlock(){
		if (parent.isThisASnapGameObject) {
			if (collidingSingleBlock == null) {
				GameObject box = GameObject.Find ("Box");
				for (int i = 0; i < box.transform.childCount; i++) {
					//Debug.Log (transform.position - box.transform.GetChild (i).transform.position);

					//ini yang ngilangin bug !!!!
					if (transform.position.x - box.transform.GetChild (i).transform.position.x < 1.5f && transform.position.x - box.transform.GetChild (i).transform.position.x >-1.5f && transform.position.y - box.transform.GetChild (i).transform.position.y < 1.5f && transform.position.y - box.transform.GetChild (i).transform.position.y >-1.5f ) {
						//Debug.Log ("tesstttt");
						collidingSingleBlock = box.transform.GetChild (i).gameObject;
					}
				}
			}
		}
		return collidingSingleBlock;
	}
//	void OnMouseDown(){
//		if(!parent.isThisASnapGameObject)
//			gameObject.transform.parent.GetComponent<ShapedBlock> ().Click ();
//	}
//	void OnMouseDrag(){
//		if(!parent.isThisASnapGameObject)
//			gameObject.transform.parent.GetComponent<ShapedBlock> ().Drag ();
//	}
//	void OnMouseUp(){
//		if(!parent.isThisASnapGameObject)
//			gameObject.transform.parent.GetComponent<ShapedBlock> ().Unclick ();
//	}
	void OnTriggerEnter2D(Collider2D other){
		if (parent.isThisASnapGameObject) {
			//Debug.Log ("qwer");
			parent.isCollisionTriggered = true;
		}
		
		if (other.gameObject.tag == "Maru"|| other.gameObject.tag == "Batsu" || other.tag == "Box") {
			isInsideBox = true;
			collidingSingleBlock = other.gameObject;
			//if (gameObject.transform.parent.GetComponent<ShapedBlock> ().isThisASnapGameObject)
			//Debug.ClearDeveloperConsole();
			//Debug.Log (other.tag);

		} 
		gameObject.transform.parent.GetComponent<ShapedBlock> ().CollisionEnter (other,gameObject);
	}
	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Maru" || other.tag == "Batsu") {
			collidingSingleBlock = other.gameObject;

		}
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Maru" || other.tag == "Batsu") {
			collidingSingleBlock = null;
		}
		if (other.tag == "Box") {
			isInsideBox = false;
		}

		gameObject.transform.parent.GetComponent<ShapedBlock> ().CollisionExit (other);
	}
}
