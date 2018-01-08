using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class StageLoader : MonoBehaviour {

	// Use this for initialization

	//current selected stage number
	public int stageNumber;

	//stored (click & drag) from prefabs
	public GameObject[] blockPrefabs;

    //current Active Block
    public int currentActiveBlock = 0;

	//determine maru or batsu from reading txt file
	public List<List<string>> position;
	//which block number chosen in prefabs by reading txt file 
	public List<int> blockNumbers;

	//holding blocks gameobject to do action later (destroy all blocks GameObject)
	public GameObject blockHolder;

	public int zoomDimension;

	public Vector3 blockZoomOutScale;


	//offset of the maru batsu block
	const float OFFSET = 2.56f;

	//references
	//GameController script from GameController GameObject reference
	private GameController gameController;
	//Box GameObject reference
	private GameObject box;
	//reference for stageNumber Text Label
	private Text stageNumberTxt;
    //reference to blockButton
	private BlockButtonUIScript blockButtonUIScript;

	private Vector2 initialBoxPosition;
	private Vector3 initialScale;
	private Vector2 box4x4Position;
	private Vector3 box4x4Scale;
	private Vector2 boxColliderInitialOffset;
	private Vector2 boxColliderInitialSize;
	private Vector2 boxCollider4x4Offset;
	private Vector2 boxCollider4x4Size;

	void Start () {
		position = new List<List<string>>();
		zoomDimension = 3;
		blockNumbers = new List<int> ();
		
		//initialize references
		gameController = GetComponent<GameController>();
		stageNumberTxt = GameObject.Find ("StageNumberTxt").GetComponent<Text> ();
        blockButtonUIScript = GameObject.Find("BlockButtonUI").GetComponent<BlockButtonUIScript>();
		box = GameObject.FindGameObjectWithTag ("Box");

		//getting stageNumber from Stage Selection
		stageNumber = PlayerPrefs.GetInt ("stageNumber") + 1;
		stageNumberTxt.text = stageNumber + "";

		initialScale = box.transform.localScale;
		initialBoxPosition = box.transform.position;

		Vector3 scale = box.transform.localScale;
		scale.x *= 0.8f;
		scale.y *= 0.8f;
		box4x4Scale = scale;
		box4x4Position = new Vector2 (box.transform.position.x - 1f, box.transform.position.y + 1.55f);

		boxColliderInitialSize = box.GetComponent<BoxCollider2D> ().size;
		boxColliderInitialOffset = box.GetComponent<BoxCollider2D> ().offset;

		boxCollider4x4Size = new Vector2 (box.GetComponent<BoxCollider2D> ().size.x + 2.56f, box.GetComponent<BoxCollider2D> ().size.y + 2.56f);
		boxCollider4x4Offset = new Vector2 (box.GetComponent<BoxCollider2D> ().offset.x + (2.56f/2), box.GetComponent<BoxCollider2D> ().offset.y - (2.56f/2));

		//  読み込み
		StartCoroutine(ReadConfig());
	}



	// Update is called once per frame
	void Update () {
		
	}
	IEnumerator ReadConfig()
	{
		clearBlocks ();
		clearBox ();
		string line;

		//filename to be read
		string filename = stageNumber+".txt";
		//filepath in the project assets
		string path = "";



#if UNITY_EDITOR
		path = Application.streamingAssetsPath + "/stages/" + filename;
		FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
		if (stream == null) { Debug.Log("ERROR"); }
		System.IO.TextReader file = new System.IO.StreamReader(stream);

#elif UNITY_ANDROID

		path = "jar:file://" + Application.dataPath + "!/assets" + "/stages/" + filename;
		WWW www = new WWW(path);
		yield return www;
		System.IO.TextReader file = new StringReader(www.text);

#endif

        while ((line = file.ReadLine()) != null)
		{
			//Debug.Log(line);

			List<string> row = new List<string>();

			string[] str = line.Split(',');

			if(str[0] == "blocks"){
				for(int i=1;i<str.Length;i++){
					blockNumbers.Add(int.Parse(str[i]));
				}
			}
			else{
				for (int i = 0; i < str.Length; i++)
				{
					row.Add(str[i]);
				}
				position.Add(row);
			}
		}
		file.Close();


		// show / hide Clear Stamp
		/*
		if (!ScoreInfo.Scores [stageNumber - 1].isClear) {
			
			gameController.gameClear.SetActive (false);

		} else {
			gameController.gameClear.SetActive (true);

		}*/
		

		//initiation of maru & batsu
		initPositions();

		//initiation of blocks
		initBlocks (blockNumbers[0]);

        blockButtonUIScript.initBlockButton(blockNumbers);

		yield return null;

		//ready = true;
	}
	void initPositions(){
		//statiblock is maru & batsu, reference from GameController

		gameController.staticBlock = new List<GameObject> ();
		box.transform.localScale = initialScale;
		box.transform.position = initialBoxPosition;
		float x = -OFFSET;
		float y = OFFSET+2;
		for (int i = 0; i < position.Count; i++)
		{
			x = -OFFSET;
			List<string> row = position [i];
			for (int j = 0; j < row.Count; j++)
			{

				if (row[j] == "O")
				{
					GameObject maru = (GameObject)Instantiate(gameController.maruPrefab);
					maru.transform.parent = box.transform;
					maru.transform.position = new Vector2(x, y);
					//maru.GetComponent<Animator> ().SetBool ("Spawn", true);
					gameController.staticBlock.Add (maru);
				}
				else
				{
					GameObject batsu = (GameObject)Instantiate(gameController.batsuPrefab);
					batsu.transform.parent = box.transform;
					batsu.transform.position = new Vector2(x, y);
					//batsu.GetComponent<Animator> ().SetBool ("Spawn", true);
					gameController.staticBlock.Add (batsu);
				}
				x += OFFSET;
			}
			y -= OFFSET;
		}
		if (position.Count > 0 && position.Count <= 3) {
			box.transform.localScale = initialScale;
			box.transform.position = initialBoxPosition;
			box.GetComponent<BoxCollider2D> ().offset = boxColliderInitialOffset;
			box.GetComponent<BoxCollider2D> ().size = boxColliderInitialSize;
			zoomDimension = 3;
		}
		else if (position.Count > 3) {
			box.transform.localScale = box4x4Scale;
			box.transform.position = box4x4Position;
			box.GetComponent<BoxCollider2D> ().offset = boxCollider4x4Offset;
			box.GetComponent<BoxCollider2D> ().size = boxCollider4x4Size;
			zoomDimension = 4;
		} 
	}
	public void initBlocks(int blockIndex){
		

		//for(int i=0;i<blockNumbers.Count;i++){
			
		gameController.activeShapedBlock = (GameObject)Instantiate(blockPrefabs[blockIndex]);
		Vector3 scale = transform.localScale;
		scale.x *= 0.5f;
		scale.y *= 0.5f;
		gameController.activeShapedBlock.transform.localScale = scale;
		blockZoomOutScale = scale;
		gameController.activeShapedBlock.transform.position = gameController.blockInitialPosition [0];


		//}
	}
	public void nextStage(){
		//clearBlocks ();
		//clearBox ();
		stageNumber++;
		stageNumberTxt.text = stageNumber + "";
		StartCoroutine (ReadConfig());
	}
	public void reloadStage(){
		stageNumberTxt.text = stageNumber + "";
		StartCoroutine (ReadConfig());
	}
	public void prevStage(){
		stageNumber--;
		if (stageNumber < 1) {
			stageNumber = 1;
		}
		else {
			
			StartCoroutine (ReadConfig ());
			stageNumberTxt.text = stageNumber + "";
		}
	}
	public void clearBlocks(){
		
		
		Destroy(gameController.activeShapedBlock);
		
		blockNumbers.Clear ();

        blockButtonUIScript.clearBlockButtonHolder();
	}
	public void clearBox(){
		//Debug.Log (gameController.staticBlock.Count);
		for (int i = 0; i < gameController.staticBlock.Count; i++) {
			GameObject item = gameController.staticBlock[i];
			//Debug.Log (item.name +"-"+i);
			//gameController.staticBlock.Remove (item);
			Destroy (item);
		}
		position.Clear ();
		gameController.staticBlock.Clear ();
		//Debug.Log (gameController.staticBlock.Count);
	}
}
