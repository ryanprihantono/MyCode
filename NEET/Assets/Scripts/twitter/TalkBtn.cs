using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TalkBtn : MonoBehaviour {

	public GameObject _chapter;
	public List<TweetData> _tweetData = new List<TweetData>();
	public CreateTweetNode _creator;
	public Canvas _tweetWindow;

    void Start()
	{
		_chapter = this.transform.parent.gameObject;
		_creator = GameObject.FindGameObjectWithTag("Edit").GetComponent<CreateTweetNode>();
		_tweetWindow = GameObject.FindGameObjectWithTag("TweetWindow").GetComponent<Canvas>();
    }

	public void push()
	{
		_tweetWindow.sortingOrder = 50;
		_creator.tweetSet(_chapter ,_tweetData);
        
    }

}
