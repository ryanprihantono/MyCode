using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateTweetNode : MonoBehaviour {

	[SerializeField]
	private GameObject _nodeParentObj;

	[SerializeField]
	private GameObject _tweetNodeObj;
	

	public void tweetSet(GameObject _chapterNode, List<TweetData> _data)
	{
		deleteTweet();

		var chapter = Instantiate(_chapterNode);

		chapter.transform.SetParent(_nodeParentObj.transform, false);

		foreach (var _tweet in _data)
		{
			var tweetObj = (GameObject)Instantiate(_tweetNodeObj);

			tweetObj.transform.SetParent(_nodeParentObj.transform, false);
			
			_tweet.setData(tweetObj.GetComponent<TweetNode>());
			
		}
	}

	public void deleteTweet()
	{
		foreach (Transform n in _nodeParentObj.transform)
		{
			GameObject.Destroy(n.gameObject);
		}
	}

}

[System.Serializable]
public class TweetData
{
	public string _userName;
	public string _tweet;
	public Sprite _iconSprite;

	public void setData(TweetNode _node)
	{
		_node._userName.text	= _userName;
		_node._tweet.text		= _tweet;
		_node._icon.sprite		= _iconSprite;
	}
}