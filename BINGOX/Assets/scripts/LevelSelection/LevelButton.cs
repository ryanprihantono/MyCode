using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {

	// Use this for initialization
	public int stageNumber;
	public bool isClear;
	//references

	void Start () {
		transform.GetChild (0).GetComponent<Text> ().text = (stageNumber + 1) + "";
		transform.GetChild (1).gameObject.SetActive (isClear);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OpenLevel(){
		PlayerPrefs.SetInt ("stageNumber", stageNumber);
		Application.LoadLevel ("stage");
	}
}
