using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SNSUserData : MonoBehaviour {

	public GameObject uiFollower;
    public GameObject uiFollowing;

	// Use this for initialization
	void Start () {
		
		uiFollower.GetComponent<Text>().text = UserData.Follower + "";
        uiFollowing.GetComponent<Text>().text = UserData.Following + "";
		//Debug.Log(UserData.Follower);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
