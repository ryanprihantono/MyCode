using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIScript : MonoBehaviour {

	// Use this for initialization

	//references
	private GameObject clear;
	private GameObject failed;
	private GameController gameController;
	private ShapedBlock shapedBlock;
	private StageLoader stageLoader;

	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
		clear = GameObject.Find ("Clear");
		failed = GameObject.Find ("Failed");
		stageLoader = GameObject.Find ("GameController").GetComponent<StageLoader> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void show(){
		transform.GetChild (0).gameObject.SetActive (true);
		clear = GameObject.Find ("Clear");
		failed = GameObject.Find ("Failed");
		if (shapedBlock.isStageClear()) {
			//ScoreInfo.Scores [stageLoader.stageNumber - 1].isClear = true;
			//ScoreInfo.Save ();
			clear.SetActive (true);
			failed.SetActive (false);
			//gameController.gameClear.SetActive (true);
		} else {

			clear.SetActive (false);
			failed.SetActive (true);
		}
	}

	public void nextStage(){
		clear.SetActive (true);
		failed.SetActive (true);
		stageLoader.nextStage ();
		transform.GetChild (0).gameObject.SetActive (false);
	}
	public void retryStage(){
		stageLoader.reloadStage ();
		clear.SetActive (true);
		failed.SetActive (true);
		transform.GetChild (0).gameObject.SetActive (false);
	}

	public void showResult(GameObject go){
		shapedBlock = go.GetComponent<ShapedBlock> ();
		Invoke ("show", 1.6f);
	}
}
