using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;

public static class ScoreInfo {

	[SerializeField]
    public static List<Score> Scores = new List<Score>();
    
    
    //ファイルを書き込み
    public static void Save()
    {

		string filename = "stageStatus.json";
		string filePath = "";

		//convert List<Score> to json data
		ScoreContainer scoreContainer = new ScoreContainer (ScoreInfo.Scores);
		string dataAsJson = JsonUtility.ToJson (scoreContainer);

		#if UNITY_EDITOR
		filePath = Application.streamingAssetsPath + "/" +filename;

		#elif UNITY_ANDROID
		filePath = "jar:file://" + Application.dataPath + "!/assets" + "/" + filename;

		#endif
		File.WriteAllText (filePath, dataAsJson);
		//Debug.Log ("write ");
		//Debug.Log (dataAsJson);
    }
    //ファイルを読み込み
    public static void Read()
    {
		string filename = "stageStatus.json";
		string filePath = "";

		#if UNITY_EDITOR
		filePath = Application.streamingAssetsPath + "/" +filename;

		#elif UNITY_ANDROID
		filePath = "jar:file://" + Application.dataPath + "!/assets" + "/" + filename;

		#endif
        
		if (System.IO.File.Exists(filePath)) {
			string dataAsJson = "";
#if UNITY_EDITOR
			dataAsJson = File.ReadAllText (filePath);
#elif UNITY_ANDROID
			WWW www = new WWW (filePath);
            
		    System.IO.TextReader file = new StringReader(www.text);
			string line;
            
            while((line = file.ReadLine()) != null){
                dataAsJson += line;
            }
#endif

            ScoreContainer scoreConttainer = JsonUtility.FromJson<ScoreContainer> (dataAsJson);
			ScoreInfo.Scores = scoreConttainer.scores;
			//Debug.Log ("Read");
		} else {
			string stageDirPath;
			#if UNITY_EDITOR
			stageDirPath = Application.streamingAssetsPath + "/stages/";

			#elif UNITY_ANDROID
			stageDirPath = "jar:file://" + Application.dataPath + "!/assets" + "/stages/";

			#endif
			string[] files = Directory.GetFiles (stageDirPath, "*.txt");
			for (int i = 0; i < files.Length; i++) {
				Score score = new Score (System.DateTime.Now.ToString (), i, false);
				ScoreInfo.Scores.Add (score);
			}
			ScoreContainer scoreContainer = new ScoreContainer (ScoreInfo.Scores);
			string dataAsJson = JsonUtility.ToJson (scoreContainer);
			File.WriteAllText (filePath, dataAsJson);
		}
    }
    public static void ResetHighScore()
    {
        Scores = new List<Score>();
        Save();
    }
}
[System.Serializable]
public class ScoreContainer{
	public List<Score> scores;
	public ScoreContainer(List<Score> scores){
		this.scores = scores;
	}
}
