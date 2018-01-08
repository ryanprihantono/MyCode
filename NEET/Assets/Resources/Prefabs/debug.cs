using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class debug : MonoBehaviour {

    public Text text = null;

    // Use this for initialization
	void Start () {
        StartCoroutine(log());
	}
	
    IEnumerator log()
    {
        text = GameObject.FindWithTag("debugText").GetComponent<Text>();
        Debug.Log(text.text);
        yield return new WaitForSeconds(3.0f);
        text.text = this.transform.position.ToString();
    }
}
