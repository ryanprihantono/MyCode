using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CreateChapterNode : MonoBehaviour
{
	public GameObject _chapterParentNodeObj;
	
	public GameObject _chapterChildNodeObj;
	
	public GameObject _nodeParentObj;

	[SerializeField]
	private List<ChapterParentData> _nodeParentList = new List<ChapterParentData>();



	void Start()
	{
		_nodeParentObj = GameObject.FindGameObjectWithTag("NodeParent");
		
		foreach(var _parent in _nodeParentList)
		{
			var parentObj = (GameObject)Instantiate(_chapterParentNodeObj);

			parentObj.transform.SetParent(_nodeParentObj.transform, false);
			_parent.setData(parentObj.GetComponent<ChapterParentNode>());

			foreach (var _child in _parent._chapterChildList)
			{
				var childObj = (GameObject)Instantiate(_chapterChildNodeObj);
				childObj.transform.SetParent(_nodeParentObj.transform, false);

				_child.setData(childObj.GetComponent<ChapterChildNode>());

				
			}
			
		}
	}
}

[System.Serializable]
public class ChapterParentData
{
	public string _chapterName;
	
	public string _chapterComment;
	
	public Sprite _iconSprite;

	[SerializeField]
	public List<ChapterChildData> _chapterChildList = new List<ChapterChildData>();
	
	public void setData(ChapterParentNode _node)
	{
		
		_node._chapterName.text	= _chapterName;
		_node._comment.text		= _chapterComment;
		_node._icon.sprite		= _iconSprite;
	}

}

[System.Serializable]
public class ChapterChildData
{
	
	public string _battleName;
	
	public string _battleAdress;
	
	public string _battleComment;

	public Sprite _iconSprite;

	public bool _isFavorite;

	[SerializeField]
	public List<TweetData> _tweetData = new List<TweetData>();

	public void setData(ChapterChildNode _node)
	{
		_node._battleName.text = _battleName;
		_node._battleAdress.text = _battleAdress;
		_node._comment.text = _battleComment;
		_node._icon.sprite = _iconSprite;
		_node._talkBtn.GetComponent<TalkBtn>()._tweetData = _tweetData;
		
	}
}