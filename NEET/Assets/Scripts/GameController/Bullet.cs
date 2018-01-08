using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    //references
    private UnitShot unitShot;
    private GameObject enemy;

	// Use this for initialization
	void Start () {
        unitShot = GameObject.FindGameObjectWithTag(Tags.Unit).GetComponent<UnitShot>();
        enemy = unitShot.enemy;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.gameObject == enemy)
        {
            unitShot.isBulletOut = false;
        }
    }
}
