using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class ObjectCreator : MonoBehaviour
{



    public GameObject takashiRoom;
    public GameObject unit;
    public GameObject block;
    public GameObject enemy;

    public ArrayList path = new ArrayList();

    public int enemiesSpawn = 1;

    public List<int> enemyCurrentSpawnIdxs = new List<int>();


    //
    public List<GameObject> unitBase = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    //map object
    public Map map;

    private int pathCounter = 0;

    bool start = false;

    bool m_loadstart = false;

    // Use this for initialization
    void Start()
    {

        //map object initializations
        map = gameObject.AddComponent<Map>();


        //enemy.active = false;
        /*
        unit = (GameObject)Instantiate(Resources.Load("Prefabs/Unit"));
        unit.name = "unit";
        unit.transform.position = map.getPosition(2, 1, 0.25F);
        */


        //object initialization

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(start);
        if (map != null && map.IsReady() && !m_loadstart)
        {
            initEnemies();
            initTakashiRoom();

            initBlock();
            //initTakashiGuard();
            m_loadstart = true;
        }

    }


    public void initEnemies()
    {
        for (int i = 0; i < map.enemyPopUpPosition.Count; i++)
        {
            Vector3 v = map.enemyPopUpPosition[i];
            //GameObject enemy = (GameObject)Instantiate(Resources.Load("Prefabs/Enemy"));
            GameObject obj = (GameObject)Instantiate(enemy);

            enemy.name = "enemy_" + i + "_" + 0 + "_";
            enemy.transform.position = v;

            enemyCurrentSpawnIdxs.Add(0);

            enemies.Add(enemy);
        }
    }

    public void SpawnEnemy(int enemyPopUpPositionIdx, int enemyCurrentIndex)
    {
        if (enemyCurrentSpawnIdxs[enemyPopUpPositionIdx] == enemyCurrentIndex)
        {
            enemyCurrentSpawnIdxs[enemyPopUpPositionIdx] += 1;
            if (enemyCurrentSpawnIdxs[enemyPopUpPositionIdx] < enemiesSpawn)
            {
                Vector3 v = map.enemyPopUpPosition[enemyPopUpPositionIdx];
                GameObject obj = (GameObject)Instantiate(enemy);

                enemy.name = "enemy_" + enemyPopUpPositionIdx + "_" + enemyCurrentSpawnIdxs[enemyPopUpPositionIdx] + "_";
                enemy.transform.position = v;

                enemies.Add(enemy);
            }
        }
    }

    public void Test()
    {
        start = true;
    }

    public void initTakashiRoom()
    {
        //Object room = Resources.Load("Prefabs/TakashiRoom");

        //takashiRoom = (GameObject)Instantiate(room);

        //takashiRoom.transform.position = map.takashiRoomPosition;
        //takashiRoom.name = "takashiRoom";

        GameObject obj = (GameObject)Instantiate(takashiRoom);
        obj.transform.position = map.takashiRoomPosition;
        obj.name = takashiRoom.name;
    }
    /*
    private void initTakashiGuard()
    {
        GameObject unit = (GameObject)Instantiate(Resources.Load("Prefabs/Unit"));
        unit.name = "takashiGuard";
        unit.tag = "takashiGuard";

        unit.transform.position = new Vector3(takashiRoom.transform.position.x, takashiRoom.transform.position.y + 0.25F, takashiRoom.transform.position.z);
    }
    */
    public void initBlock()
    {
        for (int i = 0; i < map.blockPosition.Count; i++)
        {
            Vector3 unitPosition = map.blockPosition[i];
            //GameObject go = (GameObject)Instantiate(Resources.Load("Prefabs/Block"));
            GameObject go = (GameObject)Instantiate(block);
            go.name = "base" + i;
            //go.transform.localScale = new Vector3(1, 0.25F, 1);
            go.transform.position = unitPosition;
            //go.AddComponent<Rigidbody>();
            unitBase.Add(go);
        }

    }
}
