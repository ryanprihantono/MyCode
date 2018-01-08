using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapedBlock : MonoBehaviour {

	//click and drag variables
	private float dist;
	private Vector3 offset;
	private Transform toDrag;
	private Vector3 v3;

	//scalling
	private bool zoom = false;

	//snapping variables
	public GameObject snapGameObject;
	public bool isSnapping = false;
	public bool isThisASnapGameObject = false;

	//references
	private GameController gameController;
	private BlockButtonUIScript blockButtonUI;
	private GameObject resultUI;
	private StageLoader stageLoader;

	//private variables
	private float xSnapOffset = 0f;
	private float ySnapOffset = 0f;
	private float rotationAngle;
	private float rotationDegree = 0f;

	private int delayResult = 0;

	private Vector3 initialPosition;

	private Color defaultColor;
	//private Vector3 hidePosition;

	//private bool isMoved = false;
	public bool isCollisionTriggered = false;

	// Use this for initialization

	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		blockButtonUI = GameObject.Find ("BlockButtonUI").GetComponent<BlockButtonUIScript> ();
		stageLoader = GameObject.Find ("GameController").GetComponent<StageLoader> ();
		resultUI = GameObject.Find ("ResultUI");
		resultUI.transform.GetChild (0).gameObject.SetActive (false);

		initialPosition = gameObject.transform.position;
		for (int i = 0; i < transform.childCount; i++)
		{
			if (isThisASnapGameObject) {
				transform.GetChild (i).GetComponent<BoxCollider2D> ().enabled = false;
			} else {
				defaultColor = gameObject.transform.GetChild (i).GetComponent<Renderer> ().material.color;
				defaultColor.a -= 0.2f;
				gameObject.transform.GetChild (i).GetComponent<Renderer> ().material.SetColor ("_Color", defaultColor);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (!isThisASnapGameObject) {
			rotationAngle = Mathf.LerpAngle (transform.rotation.z, rotationDegree, Time.deltaTime);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, 0, rotationDegree), Time.deltaTime * 10f);
		}
//		if (isThisASnapGameObject) {
//			
//			if (!isCollisionTriggered) {
//				if (!isMoved) {
//					transform.position = new Vector2 (0, 0);
//					isMoved = true;
//				} else {
//					Debug.Log ("asdf");
//					transform.Translate(initialPosition);
//				}
//			}
//		}
	}

	public void RotateLeft(){
		rotationDegree += 90f;

	}
	public void RotateRight(){
		
		rotationDegree -= 90f;

	}

	void OnMouseDown(){
		
		if (!isThisASnapGameObject)
			Click ();
	}
	void OnMouseDrag(){
		if (!isThisASnapGameObject)
			Drag ();
	}
	void OnMouseUp(){
		if (!isThisASnapGameObject)
			Unclick ();
	}

	public void Click(){
		gameController.activeShapedBlock = gameObject;

		toDrag = transform;
		dist = transform.position.z - Camera.main.transform.position.z;
		v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
		v3 = Camera.main.ScreenToWorldPoint(v3);
		offset = toDrag.position - v3;



		for (int i = 0; i < transform.childCount; i++)
		{
			Color color = gameObject.transform.GetChild(i).GetComponent<Renderer>().material.color;
			color.a -= 0.2f;
			gameObject.transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", color);
		}
	}
	public void Drag(){
		v3 = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		toDrag.position = v3 + offset;
		/*
		if (gameObject.transform.position != hidePosition) {
			setAlpha (defaultColor.a);
		}*/
	}

	bool isAllCollidingSingleBlockFilled(){
		if (snapGameObject != null) {
			for (int i = 0; i < snapGameObject.transform.childCount; i++) {
				if (snapGameObject.transform.GetChild (i).GetComponent<SingleBlockScript> ().setCollidingSingleBlock () == null) {
					return false;
				}
			}
		}
		return true;
	}

	public void Unclick(){
		//Debug.Log (isOneOfChidOutsideTheBox());


		if (!isOneOfChidOutsideTheBox() && isAllCollidingSingleBlockFilled()) {
			
			for (int i = 0; i < snapGameObject.transform.childCount; i++) {
				Color color = snapGameObject.transform.GetChild (i).GetComponent<Renderer> ().material.color;
				color.a += 0.2f;
				snapGameObject.transform.GetChild (i).GetComponent<Renderer> ().material.SetColor ("_Color", color);
				//Debug.Log (gameObject.transform.GetChild(i).GetComponent<SingleBlockScript>().collidingBox);
				GameObject go;
				if (snapGameObject.transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock != null) {
					if (snapGameObject.transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock.tag == "Maru") 
						go = Instantiate (gameController.batsuPrefab);
					else
						go = Instantiate (gameController.maruPrefab);
					go.GetComponent<Animator> ().SetBool ("Spawn", true);
					gameController.staticBlock.Add (go);
					go.transform.position = transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock.transform.position;
					go.transform.parent = transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock.transform.parent;

					transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock.GetComponent<Animator> ().SetBool("Destroy",true);

					Destroy (transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock,1f);

				}
			}
			Destroy (snapGameObject);
			Destroy (gameObject);
			blockButtonUI.blockButtonHolder.Remove (gameController.activeBlockButton);
			Destroy (gameController.activeBlockButton);
			if (blockButtonUI.blockButtonHolder.Count > 0) {
				gameController.activeBlockButton = blockButtonUI.blockButtonHolder [0];

				gameController.GetComponent<StageLoader> ().initBlocks (gameController.activeBlockButton.GetComponent<BlockButtonScript> ().blockButtonIndex);
			}
			if (blockButtonUI.blockButtonHolder.Count == 0) {
				//show result Clear / Failed
				resultUI.GetComponent<ResultUIScript> ().showResult (gameObject);
			}
		} else {
			gameObject.transform.position = initialPosition;
			if (snapGameObject != null) {
				Destroy (snapGameObject);
			}
			if (zoom) {
				zoomOut ();
			} else {
				setAlpha (1f);
			}
		}
	}

	public void CollisionEnter(Collider2D other,GameObject singleBlock){
		
		if (other.gameObject.tag == "Box") {
			if (!zoom && !isThisASnapGameObject) {

				Vector3 scale = transform.localScale;
				scale.x *= 2f;
				scale.y *= 2f;
				transform.localScale = scale;

				if (stageLoader.position.Count > 3) {
					scale = transform.localScale;
					scale.x *= 0.8f;
					scale.y *= 0.8f;
					transform.localScale = scale;
				}

				zoom = true;

			} 

		} else if (other.tag == "Maru" || other.tag == "Batsu") {
			xSnapOffset = singleBlock.transform.position.x - other.transform.position.x;
			ySnapOffset = singleBlock.transform.position.y - other.transform.position.y;
			//Debug.Log ("asdfqwer");
			if (snapGameObject == null && isOneOfChidInsideTheBox ()) {
				snapGameObject = Instantiate (gameObject,new Vector3 (gameObject.transform.position.x - xSnapOffset, gameObject.transform.position.y - ySnapOffset, 0),gameObject.transform.rotation);
				snapGameObject.GetComponent<ShapedBlock> ().setAlpha (defaultColor.a);
				snapGameObject.gameObject.GetComponent<ShapedBlock> ().isThisASnapGameObject = true;
				snapGameObject.GetComponent<ShapedBlock> ().clearCollidingSingleBlock ();

			} else if (snapGameObject != null && isOneOfChidInsideTheBox ()) {
				Destroy (snapGameObject);
				snapGameObject = Instantiate (gameObject,new Vector3 (gameObject.transform.position.x - xSnapOffset, gameObject.transform.position.y - ySnapOffset, 0),gameObject.transform.rotation);
				snapGameObject.GetComponent<ShapedBlock> ().setAlpha (defaultColor.a);
				snapGameObject.gameObject.GetComponent<ShapedBlock> ().isThisASnapGameObject = true;
				snapGameObject.GetComponent<ShapedBlock> ().clearCollidingSingleBlock ();
			}
			//set block alpha to 0
			setAlpha (0f);
			//hidePosition = gameObject.transform.position;
		}
	}
	public void CollisionExit(Collider2D other){
		if (other.gameObject.tag == "Box") {
			if (zoom && !isOneOfChidInsideTheBox()) {
				zoomOut ();
			} 
			if(snapGameObject!=null)
				Destroy (snapGameObject);
		}else if(other.tag == "Maru" || other.tag == "Batsu"){
			
		}
	}
	void zoomOut(){
		
		transform.localScale = stageLoader.blockZoomOutScale;
		zoom = false;
		clearCollidingSingleBlock ();
		setAlpha (defaultColor.a);
	}
	bool isOneOfChidInsideTheBox(){
		for (int i = 0; i < transform.childCount; i++) {
			if (transform.GetChild (i).GetComponent<SingleBlockScript> ().isInsideBox) {
				return true;
			}
		}

		return false;
	}
	bool isOneOfChidOutsideTheBox(){
		for (int i = 0; i < transform.childCount; i++) {
			//Debug.Log (transform.GetChild (i).GetComponent<SingleBlockScript> ().isInsideBox);
			if (!transform.GetChild (i).GetComponent<SingleBlockScript> ().isInsideBox) {
				return true;
			}
			if (transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock == null) {
				return true;
			}
			//Debug.Log (transform.GetChild (i).GetComponent<SingleBlockScript> ().isInsideBox);
		}

		return false;
	}

	public void setAlpha(float alpha){
		for (int i = 0; i < transform.childCount; i++) {
			Color color = gameObject.transform.GetChild (i).GetComponent<Renderer> ().material.color;
			color.a = alpha;
			gameObject.transform.GetChild (i).GetComponent<Renderer> ().material.SetColor ("_Color", color);
		}
	}
	/*
	void setCollidingSingleBlock(){
		clearCollidingSingleBlock ();
		foreach (GameObject item in gameController.staticBlock) {
			for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild(i).gameObject.collider2D.
			}
		}
	}
	*/
	public void clearCollidingSingleBlock(){
		for (int i = 0; i < transform.childCount; i++) {
			transform.GetChild (i).GetComponent<SingleBlockScript> ().collidingSingleBlock = null;
		}
	}

	public bool isStageClear(){
		GameObject box = GameObject.Find ("Box");
		for (int i = 0; i < box.transform.childCount; i++) {
			if (box.transform.GetChild (i).tag == "Batsu") {
				return false;
			}
		}
		return true;
	}
}
