using UnityEngine;
using System.Collections;

public class UnitAnimation : MonoBehaviour {

    //References
    private Animator anim;
    private HashIDs hash;
    private BattleUI battleUI;
    private GameObject range;
	
	void Start () {
        anim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
        battleUI = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<BattleUI>();
        range = transform.GetChild(3).gameObject;
        range.GetComponent<SphereCollider>().enabled = false;
    }
	
	void Update () {
        if (battleUI.isEnemyMoving)
        {
            range.GetComponent<SphereCollider>().enabled = true;
        }
	}
}
