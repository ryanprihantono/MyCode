using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelScroll : MonoBehaviour {

	private float dist;
	private Vector3 offset;
	private Transform toDrag;
	private Vector3 v3;

	private GameObject subPanel;
	private Vector2 initialPosition;
	private Vector2 endPosition;

	// Use this for initialization
	void Start () {
		subPanel = GameObject.Find ("SubPanel");
		initialPosition = subPanel.transform.position;
		endPosition = new Vector2 (subPanel.transform.position.x, subPanel.transform.position.y + 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnMouseDown(){	
		toDrag = subPanel.transform;
		dist = subPanel.transform.position.z - Camera.main.transform.position.z;
		v3 = new Vector3(subPanel.transform.position.x, Input.mousePosition.y, dist);
		v3 = Camera.main.ScreenToWorldPoint(v3);
		offset = toDrag.position - v3;
	}
	void OnMouseDrag(){
		v3 = new Vector3 (subPanel.transform.position.x, Input.mousePosition.y, dist);
		v3 = Camera.main.ScreenToWorldPoint (v3);
		toDrag.position = v3 + offset;
		if (toDrag.position.y < initialPosition.y) {
			toDrag.position = new Vector2 (toDrag.position.x, initialPosition.y);
		}
		if (toDrag.position.y > endPosition.y) {
			toDrag.position = new Vector2 (toDrag.position.x, endPosition.y);
		}
		/*
		if (gameObject.transform.position != hidePosition) {
			setAlpha (defaultColor.a);
		}*/
	}
}
