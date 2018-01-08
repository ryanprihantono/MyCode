using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Collections;


public class SceneLoadChange : MonoBehaviour {

	public enum SCENE_LIST
	{
		NULL = -1,
        title,
        home,
        sns,
        battle,
        result_win,
        result_lose,
		TEST,
		NUM
	}

	[SerializeField]
	private SCENE_LIST _sceneList;

	//[SerializeField]
	//private Text _loadingText;
    
	[SerializeField]
	private Image _loadingBar;

    [SerializeField]
    private float _waitTime;

    public Canvas Loadscreen;



	IEnumerator LoadScene(SCENE_LIST _scene)
	{
		var async = SceneManager.LoadSceneAsync(Enum.GetName(typeof(SCENE_LIST), (int)_scene));
		async.allowSceneActivation = false;

		while (async.progress < 0.9f)
		{
			//Debug.Log(async.progress);
			_loadingBar.fillAmount = async.progress;
           // _loadingText.text = async.progress * 100 + "%";
			yield return new WaitForEndOfFrame();
		}
		
		_loadingBar.fillAmount = 1;
      //  _loadingText.text = "100%";
        yield return new WaitForSeconds(1);
        //  _loadingText.text = "100%";

        async.allowSceneActivation = true;
	}


    void Start()
    {
        Loadscreen.enabled = false;
        // StartCoroutine(wait());
        // Loadscreen.sortingOrder = -10;
    }

    public IEnumerator wait()
    {
        yield return new WaitForSeconds(_waitTime);
        Load();
    } 

    public void LoadSceneBtn()
	{
        //Loadscreen.enabled = true;
        //Loadscreen.sortingOrder = 1000; 
        //StartCoroutine(LoadScene(_sceneList));
        StartCoroutine(wait());
    }

    public void Load()
    {
        Resources.UnloadUnusedAssets();
        Loadscreen.enabled = true;
        Loadscreen.sortingOrder = 1000;
        StartCoroutine(LoadScene(_sceneList));
    }
}
