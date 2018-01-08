using UnityEngine;
using System.Collections;

public class Dialog : MonoBehaviour {

    private GameObject dialog;

    void start()
    {
        dialog = GameObject.FindGameObjectWithTag(Tags.UI);
        dialog.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
