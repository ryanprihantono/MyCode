using UnityEngine;
using System.Collections;

public class TitleUI : MonoBehaviour {

    public Canvas titleCanvas;

    void Start()
    {
        
        // Destroy(GameObject.Find("SoundManager"));
        SoundManager.Instance.PlayBGM(0);
    }
   
    public void Unload()
    {
        Destroy(titleCanvas);
        titleCanvas = null; 
        // Resources.UnloadUnusedAssets();
    }
   
}
