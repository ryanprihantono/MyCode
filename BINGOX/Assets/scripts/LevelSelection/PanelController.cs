using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PanelController : MonoBehaviour {

	// Use this for initialization

	public GameObject levelButtonPrefab;


	//level paging variables
	private int currentPage = 0;
	private int levelPerPage = 25;
	private float horizontalOffset = 1f;
	private float verticalOffset = 1f;


	//references
	//panel that holding level for a page (25 level per page)
	GameObject panel;
	GameObject subPanel;

	void Start () {
		//Reading Score Info file to determnine a level already Clear or Not
		//ScoreInfo.Read ();

		panel = GameObject.Find ("Panel");
		subPanel = GameObject.Find ("SubPanel");
		initPanel ();
	}
	
	void initPanel(){
		//getting file count

		float firstX = -1.95f;
		float firstY = 3.3f;
		float positionX = firstX;
		float positionY = firstY;

		//for (int i = 0; i < ScoreInfo.Scores.Count; i++) {
		for (int i = 0; i < 50; i++) {
			//if (i >= currentPage * levelPerPage && i < (currentPage + 1) * levelPerPage ) {
			GameObject go = Instantiate (levelButtonPrefab);
			go.transform.SetParent(subPanel.transform);
			go.transform.position = new Vector2 (positionX, positionY);
			go.GetComponent<LevelButton> ().stageNumber = i;
			//go.GetComponent<LevelButton> ().isClear = ScoreInfo.Scores [i].isClear;


			if ((i+1)  % 5 == 0) {
				positionY -= verticalOffset;
				positionX = firstX;
			} else {
				positionX += horizontalOffset;
			}

			//}
		}
	}

	public void BackToStartMenu(){
		Application.LoadLevel ("Start");
	}

}
