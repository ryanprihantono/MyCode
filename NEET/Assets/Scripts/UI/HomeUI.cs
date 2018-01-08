using UnityEngine;
using System.Collections;

public class HomeUI : MonoBehaviour
{
    public Canvas homeCanvas;

    void Start()
    {
        SoundManager.Instance.PlaySE(11);
        SoundManager.Instance.PlayBGM(1);
        
    }

    public void Unload()
    {
        // Destroy(homeCanvas);
        homeCanvas = null;
        // Resources.UnloadUnusedAssets();
    }

}
