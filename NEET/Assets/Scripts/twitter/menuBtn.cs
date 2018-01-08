using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menuBtn : MonoBehaviour {
	public Scrollbar scrollBar = null;

	public enum ScrollNum{
		TWEET	= 0,
		FAV,
		MISSION,
		NUM
	};

	public ScrollNum scrollNum;

	public void Btn(){
		scrollBar.value = scrolling_delta (scrollNum);
	}

	private float scrolling_delta(ScrollNum _scrollNum){
		return (float)_scrollNum / ((float)ScrollNum.NUM - 1);
	}

}
