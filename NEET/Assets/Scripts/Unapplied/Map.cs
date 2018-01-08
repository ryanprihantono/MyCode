using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


public class Map : MonoBehaviour
{

    public int rect = 50;
    public int padding = 10;
    public bool ready = false;

    //blocks position in Vector3
    public List<Vector3> blockPosition = new List<Vector3>();

    //number coordinate in index array
    private ArrayList properties = new ArrayList();

    //paths 1 or more than 1 , and the coordinate / position
    public ArrayList paths = new ArrayList();

    //way points
    public ArrayList waypoints = new ArrayList();

    //enemies pop up location
    public List<Vector3> enemyPopUpPosition = new List<Vector3>();

    public Vector3 takashiRoomPosition;
    /*
     * property status 
     * 0 = empty            ->gray
     * 1 = unit base        ->orange
     * 2 = path             ->blue
     * 3 = takashi's room   ->red
     * 4 = enemy pop up     ->green
     */

    public Map()
    {
        ready = false;

        takashiRoomPosition[0] = -1;
        takashiRoomPosition[1] = -1;

        //  読み込み
        StartCoroutine(ReadConfig());

        //  配置
    }

    public bool IsReady()
    {
        return ready;
    }

    public Vector3 getPosition(int x, int z)
    {

        Vector3 v = new Vector3();
        v.z = (z - (properties.Count / 2) + 1) * -1;
        List<int> column = properties[0] as List<int>;

        v.x = x - (column.Count / 2) - 1;
        v.y = 0F;
        return v;
    }
    public Vector3 getPosition(int x, int z, int y)
    {

        Vector3 v = new Vector3();
        v.z = (z - (properties.Count / 2) + 1) * -1;
        List<int> column = properties[0] as List<int>;

        v.x = x - (column.Count / 2) - 1;
        v.y = y;
        return v;
    }
    public Vector3 getPosition(int x, int z, float y)
    {

        Vector3 v = new Vector3();
        v.z = (z - (properties.Count / 2) + 1) * -1;
        List<int> column = properties[0] as List<int>;

        v.x = x - (column.Count / 2) - 1;
        v.y = y;
        return v;
    }

    IEnumerator ReadConfig()
    {
        string line;

        string filename = "blocks.txt";
        string path = "";

#if UNITY_EDITOR
        path = Application.streamingAssetsPath + "\\" + filename;
        FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        if (stream == null) { Debug.Log("ERROR"); }
        System.IO.TextReader file = new System.IO.StreamReader(stream);

        while ((line = file.ReadLine()) != null)
        {
            //Debug.Log(line);

            List<int> property = new List<int>();

            string[] str = line.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                property.Add(int.Parse(str[i]));
            }

            properties.Add(property);
        }
        file.Close();

#elif UNITY_ANDROID

        path = "jar:file://" + Application.dataPath + "!/assets" + "/" + filename;
        WWW www = new WWW(path);
        yield return www;
        System.IO.TextReader file = new StringReader(www.text);

        while ((line = file.ReadLine()) != null)
        {
            List<int> property = new List<int>();

            string[] str = line.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                property.Add(int.Parse(str[i]));
            }
            properties.Add(property);
        }
        file.Close();
#endif

        yield return StartCoroutine(ReadPath());

        initPositions();
        sortEnemyPath();

        yield return null;

        ready = true;
    }


    IEnumerator ReadPath()
    {
        string line;

        string filename = "paths.txt";
        string filepath = "";

#if UNITY_EDITOR
        filepath = Application.streamingAssetsPath + "\\" + filename;
        FileStream stream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
        System.IO.TextReader file = new System.IO.StreamReader(stream);

        while ((line = file.ReadLine()) != null)
        {

            List<Vector3> path = new List<Vector3>();
            string[] points = line.Split('>');

            for (int i = 0; i < points.Length; i++)
            {
                string[] strpoint = points[i].Split(',');

                int[] point = { int.Parse(strpoint[1]), int.Parse(strpoint[0]) };

                path.Add(getPosition(point[0], point[1]));

            }
            paths.Add(path);



        }
        file.Close();

#elif UNITY_ANDROID

        filepath = "jar:file://" + Application.dataPath + "!/assets" + "/" + filename;
        WWW www = new WWW(filepath);
        yield return www;
        System.IO.TextReader file = new StringReader(www.text);

        while ((line = file.ReadLine()) != null)
        {
            
            List<Vector3> path = new List<Vector3>();
            string[] points = line.Split('>');

            for (int i = 0; i < points.Length; i++)
            {
                string[] strpoint = points[i].Split(',');
                
                int[] point = { int.Parse(strpoint[1]), int.Parse(strpoint[0]) };

                path.Add(getPosition(point[0], point[1]));
                
            }
            paths.Add(path);



        }
        file.Close();
#endif
        yield return null;
    }

    void initPositions()
    {
        /*
         *      property status 
         *      0 = empty            ->gray
         *      1 = unit base        ->orange
         *      2 = path             ->blue
         *      3 = takashi's room   ->red
         *      4 = enemy pop up     ->green
         */
        for (int i = 0; i < properties.Count; i++)
        {


            List<int> columns = (List<int>)properties[i];

            for (int j = 0; j < columns.Count; j++)
            {
                if (columns[j] == 1)
                {
                    blockPosition.Add(getPosition(j, i));
                }
                else if (columns[j] == 3)
                {
                    takashiRoomPosition = getPosition(j, i);
                }
                else if (columns[j] == 4)
                {

                    enemyPopUpPosition.Add(getPosition(j, i, 0F));
                }
            }
        }
    }


    void sortEnemyPath()
    {

        for (int i = 0; i < enemyPopUpPosition.Count; i++)
        {
            for (int j = 0; j < paths.Count; j++)
            {

                List<Vector3> p = paths[j] as List<Vector3>;

                if (p[0].x == enemyPopUpPosition[i].x && p[0].z == enemyPopUpPosition[i].z)
                {
                    paths[j] = paths[i];
                    paths[i] = p;
                }
            }
        }

    }
}