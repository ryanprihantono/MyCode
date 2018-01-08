using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockButtonUIScript : MonoBehaviour {

	public Vector2[] blockButtonPosition;
    public GameObject[] blockButtonPrefabs;

    public List<GameObject> blockButtonHolder;
	//references
	GameController gameController;

	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController").GetComponent<GameController> ();
        blockButtonHolder = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void initBlockButton(List<int> blockNumbers)
    {
        for (int i = 0; i < blockNumbers.Count; i++)
        {
            
            GameObject go = (GameObject)Instantiate(blockButtonPrefabs[blockNumbers[i]]);
			//go.transform.parent = gameObject.transform;
			go.transform.SetParent (gameObject.transform);
            /*Vector3 scale = transform.localScale;
            scale.x *= 0.5f;
            scale.y *= 0.5f;*/
            //go.transform.localScale = scale;
            go.transform.position = blockButtonPosition[i];
            //go.GetComponent<>
            blockButtonHolder.Add(go);
        }
		gameController.activeBlockButton = blockButtonHolder [0];
    }
    
    public void clearBlockButtonHolder()
    {
        for (int i = 0; i < blockButtonHolder.Count; i++)
        {
            Destroy(blockButtonHolder[i]);
        }
        blockButtonHolder.Clear();
    }
}
