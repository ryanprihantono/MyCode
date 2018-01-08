using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour {

	// Use this for initialization
	void Start () {
        CameraFade.StartAlphaFade(Color.black, true, 2f, 0f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
