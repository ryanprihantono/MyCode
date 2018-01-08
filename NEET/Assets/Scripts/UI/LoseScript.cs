using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoseScript : MonoBehaviour
{

    
    private bool Lose;

    public ShutterScript Shutter;
    public SceneLoadChange SceneLoad;

   

    public void Losed()
    {
        if (Lose == false)
        {
            SoundManager.Instance.PlaySE(6);
            //StartCoroutine(wait());
            //Invoke("SecneLoad", 1.0f);
            Shutter.Close();
            SceneLoad.LoadSceneBtn();
            Lose = true;
        }
    }
}
