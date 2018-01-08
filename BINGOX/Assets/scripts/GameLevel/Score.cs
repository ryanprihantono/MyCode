using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Score {
    public string Name;
	public int stageNumber;
	public bool isClear;
	public Score(string Name,int stageNumber,bool isClear){
		this.Name = Name;
		this.stageNumber = stageNumber;
		this.isClear = isClear;
	}
}
