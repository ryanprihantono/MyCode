using UnityEngine;
using System.Collections;

public class testhoge : MonoBehaviour {

	public Color color;
//	public Material material;

	void Start () {
		color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
		//material.color = color;
		this.GetComponent<MeshRenderer>().material.color= color;
	}
	
}
