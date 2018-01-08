using UnityEngine;
using System.Collections;

public class ResultUI : MonoBehaviour
{

    public Canvas resultCanvas;
    public GameObject anim;

    void Start()
    {
        Invoke("PlaySE", 0.7f);
        SoundManager.Instance.PlaySE(11);
        anim = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    }

    void PlaySE()
    {
        if (Application.loadedLevelName == "result_lose")
        {
            SoundManager.Instance.PlaySE(8);
        }

        else if (Application.loadedLevelName == "result_win")
        {
            SoundManager.Instance.PlaySE(9);
        }
    }

    public void Unload()
    {
        Destroy(anim);
        anim = null;
        resultCanvas = null;
        // Resources.UnloadUnusedAssets();

    }
    
}
