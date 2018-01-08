using UnityEngine;
using System.Collections;

public class RangeCasting : MonoBehaviour {

    public GameObject rangeSpot;

    //references
    private UnitSelection unitSelection;

	// Use this for initialization
	void Start () {
        unitSelection = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<UnitSelection>();
	}
	
	// Update is called once per frame
	void Update () {
        if (unitSelection.selectedUnit != -1)
        {
            /*float height = 2F/Mathf.Sin(Mathf.Deg2Rad*15F);
            Debug.Log(height);
            Vector3 vec = new Vector3(transform.position.x, height, transform.position.z);
            transform.position = vec;*/
        }
	}
}
