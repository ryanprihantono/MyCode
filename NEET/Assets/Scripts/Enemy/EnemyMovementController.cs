using UnityEngine;
using System.Collections;

public class EnemyMovementController : MonoBehaviour {

	//references
	private EnemyMovement enemyMovement;
    private EnemyShot otherEnemyShot;

    GameObject loadscreen;
    private LoseScript loseScript;

	// Use this for initialization
	void Start () {
		enemyMovement = gameObject.transform.parent.GetComponent<EnemyMovement>();
        loadscreen = GameObject.Find("GameController");
        loseScript = loadscreen.transform.GetComponent<LoseScript>();
        


	}
	
	// Update is called once per frame
	void Update () {
	    
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.EnemyBody)
        {
            //Debug.Log("enter-" + other.tag + "-" + other.name);
            //Physics.IgnoreCollision(other.GetComponent<Collider>(), other);
            
            if (other.transform.parent.GetChild(3).GetComponent<EnemyShot>().isShooting)
            {
            
                //Debug.Log("enter-" + other.tag + "-" + other.name);
                //gameObject.transform.parent.GetComponent<EnemyMovement>().StopMove();
                /*if (other.transform.parent.GetChild(3).GetComponent<EnemyShot>().isShooting == false)
                {
                    other.transform.parent.GetComponent<EnemyMovement>().StartMove();
                }*/
            }/*
            if (other.transform.parent.GetComponent<EnemyMovement>().isMoving)
            {
                gameObject.transform.parent.GetComponent<EnemyMovement>().StopMove();
            }*/
        }
		if (other.tag == Tags.TakashiRoom)
		{
            // Application.LoadLevel("result");
            loseScript.Losed();
		}
		
	}
	void OnTriggerExit(Collider other)
	{
        if (other.tag == Tags.EnemyBody)
        {
            if (!gameObject.transform.parent.GetChild(3).GetComponent<EnemyShot>().isShooting)
            {
            
               // Debug.Log("exit-" + other.tag + "-" + other.name);
               // Debug.Log("name-" + other.transform.parent.name+"-"+ other.transform.parent.GetChild(3).GetComponent<EnemyShot>().isShooting);
            
            
                //other.transform.parent.GetComponent<EnemyMovement>().StartMove();
                
			    
			    /*if (other.transform.parent.GetChild(3).GetComponent<EnemyShot>().isShooting == true)
			    {
				    other.transform.parent.GetComponent<EnemyMovement>().StopMove();
			    }*/
		    }
            /*if (other.transform.parent.GetComponent<EnemyMovement>().isMoving)
            {
                other.transform.parent.GetComponent<EnemyMovement>().StartMove();
            }*/
        }

	}
}
