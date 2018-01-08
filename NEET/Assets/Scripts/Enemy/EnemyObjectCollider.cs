using UnityEngine;
using System.Collections;

public class EnemyObjectCollider : MonoBehaviour {
    
    public GameObject enemyAttached;
    //references
    private EnemyShot enemyShot;
    private EnemyMovement enemyMovement;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag+"--"+enemyAttached.tag);
        /*if(other.gameObject.tag == Tags.unit)
        {
            enemyShot.unit = other.gameObject;
            enemyShot = enemyAttached.GetComponent<EnemyShot>();
            enemyMovement = enemyAttached.GetComponent<EnemyMovement>();
            enemyMovement.speed = 0f;
            enemyShot.isShooting = true;

        }*/
    }
}
