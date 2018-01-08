using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer
     : MonoBehaviour
{

    public Text timeGUI;
    
    public gameOverScript gameOverScript;

    // public BattleUI isEnemyMoving;

    private float time = 991.0f;

    private int countBorder = 2000;
    private int count = 0;

    public float sceneChangeCount = 0f;
    private float timedelta;
    public float sceneChangeTime;
    public float GameTime;

    public ShutterScript Shutter;
    public SceneLoadChange SceneLoad;
    public BattleUI BattleCntr;

    private bool Gong = false;

    void Start()
    {
        timedelta = Time.deltaTime;
        timeGUI.text = time.ToString();
    }

    public void ClickStart()
    {
        time = GameTime;
    }

    void Update()
    {
        if (time > 0f)
        {
            time -= 1f * Time.deltaTime;
            timeGUI.text = ((float)time).ToString("f1");
        }

        if (time < 0.0f)
        {
            time = 0;
            // sceneChangeCount += timedelta;
            // GameOver();
        }

        if (time == 0.0f)
        {
            if (Gong == false)
            {
                SoundManager.Instance.PlaySE(6);
                Gong = true;
                Shutter.Close();
                BattleCntr.Unload();
                // SceneManager.LoadSceneAsync("result_win");
                SceneLoad.LoadSceneBtn();
            }

        }
        
        //if(sceneChangeCount > (sceneChangeTime))
        //{
        //    
        //   
        //}
        //Debug.Log(Time.deltaTime);
    }


    void GameOver()
    {
        gameOverScript.SendMessage("Lose");
        Time.timeScale = 0;
    }

    
}