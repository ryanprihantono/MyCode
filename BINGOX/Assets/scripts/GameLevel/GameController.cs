using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject activeShapedBlock;
	public GameObject activeBlockButton;
	public GameObject maruPrefab;
	public GameObject batsuPrefab;
	public List<GameObject> staticBlock;
	public GameObject gameClear;

	public Vector3[] blockInitialPosition;

	//references
	private StageLoader stageLoader;

	// Use this for initialization

	void Start () {
		//gameClear = GameObject.Find ("Clear 2");

		stageLoader = gameObject.GetComponent<StageLoader> ();

		// reading file to determine level is clear or not
		//ScoreInfo.Read ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void changeActiveBlock(int blockIndex){
		Destroy (activeShapedBlock);
		stageLoader.initBlocks (blockIndex);
	}
	public void RotateRight(){
		if (activeShapedBlock != null)
			activeShapedBlock.GetComponent<ShapedBlock> ().RotateRight ();
	}
	public void RotateLeft(){
		if (activeShapedBlock != null)
			activeShapedBlock.GetComponent<ShapedBlock> ().RotateLeft ();
	}
	public void ResetLevel(){
		stageLoader.reloadStage ();
	}
	public void BackToLevelSelection(){
		Application.LoadLevel ("LevelSelection");
	}
}
