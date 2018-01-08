using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{

    public static float defaultSpeed = 0.5f;

    public float speed = 0.5f;
    public Transform[] wayPoints;
    public GameObject unit;
    public UnitShot unitShot;
	public float timeToSpawn = 3f;
    public bool isMoving = true;

    //references
    private UnityEngine.AI.NavMeshAgent nav;
    private Map map;
    private int wayPointIndex = 1;
    private Animator anim;
    private HashIDs hash;
    Button playButton;
    private BattleUI battleUI;
    private GameObject range;

    private int direction;
    private int initialState;
    
    private Transform lastPosition;
    private Vector3 initialPosition;
	private float spawnTimer = 0f;
	


    void Start()
    {

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        map = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<ObjectCreator>().map;
        anim = transform.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        //transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();
        battleUI = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<BattleUI>();
        range = transform.GetChild(3).gameObject;
        range.GetComponent<SphereCollider>().enabled = false;
        //playButton = GameObject.FindGameObjectWithTag(Tags.canvas).transform.GetChild(4).GetComponent<Button>();
        //playButton.
        //anim.SetLayerWeight(1, 1f);

        InitWayPoints();
        initialState = anim.GetCurrentAnimatorStateInfo(0).fullPathHash;
        anim.SetInteger(hash.direction, GetDirection(nav.transform.position, wayPoints[wayPointIndex + 1].position));

        initialPosition = gameObject.transform.position;
    }

    void Update()
    {

        //if (anim.GetCurrentAnimatorStateInfo(0).fullPathHash != initialState)
        //{
        //isMoving = true;
        //transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        if (battleUI.isEnemyMoving)
        {
			if (isMoving) 
			{ 
				range.GetComponent<SphereCollider>().enabled = true;
				Move();
			}
        }


        //}
        anim.SetFloat(hash.speed, speed);
    }


    void InitWayPoints()
    {
        int index = int.Parse(gameObject.name.Split('_')[1]);
        GameObject goWayPoints = new GameObject();
        goWayPoints.name = gameObject.name + "_wayPoints";
        List<Vector3> wayPointsVec = (List<Vector3>)map.paths[index];

        wayPoints = new Transform[wayPointsVec.Count];

        for (int i = 0; i < wayPointsVec.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.parent = goWayPoints.transform;
            go.transform.position = wayPointsVec[i];
            go.name = "wayPoint_" + i;
            wayPoints[i] = go.transform;
            //Debug.Log(i+":"+go.transform.position);
        }
        //Debug.Log(wayPoints[1].position-wayPoints[0].position);
    }

    void Move()
    {
        //Debug.Log(wayPointIndex);

        nav.speed = speed;
        //Debug.Log(gameObject.name +":"+ nav.remainingDistance + ":" + nav.stoppingDistance);
        if (Difference(nav.remainingDistance, nav.stoppingDistance) <= 0.15)
        {
            nav.transform.position = nav.destination;
        }
        if (nav.remainingDistance == nav.stoppingDistance)
        {
            if (wayPointIndex < wayPoints.Length - 1)
            {
                wayPointIndex++;
            }
        }

        
        nav.destination = wayPoints[wayPointIndex].position;
        //Debug.Log("name : " + name + " - destination : " + nav.destination + " - position :" + nav.transform.position + " waypointindex : " + wayPointIndex + " - difference : " + Difference(nav.remainingDistance, nav.stoppingDistance));
        anim.SetInteger(hash.direction, GetDirection(nav.transform.position, nav.destination));
        nav.updateRotation = false;

        Vector3 difference = gameObject.transform.position - initialPosition;
        //Debug.Log(difference.x+" - "+ difference.y + " - "+ difference.z);
        //if (Mathf.Floor(difference.x) == 2 || Mathf.Floor(difference.x) == -2 || Mathf.Floor(difference.y) == 2 || Mathf.Floor(difference.y) == -2 || Mathf.Floor(difference.z) == 2 || Mathf.Floor(difference.z) == -2)
        if(spawnTimer >= timeToSpawn)
		{
            //Debug.Log(difference);
            int popUpIdx = int.Parse(gameObject.name.Split('_')[1]);
            int currentIdx = int.Parse(gameObject.name.Split('_')[2]);
            GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<ObjectCreator>().SpawnEnemy(popUpIdx, currentIdx);
			spawnTimer = 0f;
        }
		spawnTimer += Time.deltaTime;
        //Debug.Log(initialPosition-gameObject.transform.position);
        //enemyCollider.transform.position = gameObject.transform.position;
    }

    private float Difference(float a,float b)
    {
        if (a < b)
        {
            return b - a;
        }
        else
        {
            return a - b;
        }
    }

    int GetDirection(Vector3 src, Vector3 dest)
    {
        Vector3 dir = dest - src;
        //Debug.Log(dest + "" + src);
        //Debug.Log(dir);
        if (dir.x != 0)
        {
            if (dir.x > 0)
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = true;
            else
                transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = false;
            return 0;
        }
        else if (dir.z > 0)
        {
            return 2;
        }
        else if (dir.z < 0)
        {
            return 1;
        }
        return 0;
    }

    public void StartMove()
    {

        isMoving = true;
        //Debug.Log(isMoving);
    }

	public void StopMove()
	{
		isMoving = false;
	}

    //blink
    void BlinkPlayer(int numBlinks, GameObject go)
    {
        StartCoroutine(DoBlinks(numBlinks, 0.1f, go));
    }

	

    IEnumerator DoBlinks(int numBlinks, float seconds, GameObject go)
    {

        for (int i = 0; i < numBlinks * 2; i++)
        {

            //toggle renderer
            go.GetComponent<Renderer>().enabled = !go.GetComponent<Renderer>().enabled;

            //wait for a bit
            yield return new WaitForSeconds(seconds);
        }

        //make sure renderer is enabled when we exit
        go.GetComponent<Renderer>().enabled = true;
    }
}
