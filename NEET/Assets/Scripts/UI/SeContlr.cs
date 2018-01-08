using UnityEngine;
using System.Collections;

public class SeContlr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SEGoto()
    {
        SoundManager.Instance.PlaySE(0);
    }

    public void SEBackto()
    {
        SoundManager.Instance.PlaySE(1);
    }

    public void Incorrect()
    {
        SoundManager.Instance.PlaySE(3);
    }

    public void Click()
    {
        SoundManager.Instance.PlaySE(11);
    }

    public void Cansel()
    {
        SoundManager.Instance.PlaySE(2);
    }

    public void Pop()
    {
        SoundManager.Instance.PlaySE(7);
    }

    public void BattleStart()
    {
        SoundManager.Instance.PlaySE(5);
    }

    public void BattleEnd()
    {
        SoundManager.Instance.PlaySE(6);
    }

    public void Pyn()
    {
        SoundManager.Instance.PlaySE(4);
    }

}
