using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleUI : MonoBehaviour {

    public bool isEnemyMoving = false;
    private bool Lose;
	public GameObject confirmationPop;
	public GameObject uiMoney;
	public GameObject uiFollower;
    public GameObject uiFollowing;

    //references
    private GameObject pauseButton;
    private GameObject startButton;
    private GameObject timer;
    private GameObject backtosns;
    private GameObject pop;
	private GameObject []unitRangeSpots;

    public ShutterScript Shutter;
    public SceneLoadChange SceneLoad;

    public Canvas batlleCanvas;
    public GameObject battleObj;
    private Object battleCanvas;

    void Start()
    {
        
        SoundManager.Instance.PlaySE(11);
        SoundManager.Instance.PlayBGM(5);

        pauseButton = GameObject.FindGameObjectWithTag(Tags.UI).transform.GetChild(3).gameObject;
        pauseButton.SetActive(false);
        startButton = GameObject.FindGameObjectWithTag(Tags.UI).transform.GetChild(4).gameObject;
        startButton.SetActive(true);
        timer = GameObject.FindGameObjectWithTag(Tags.UI).transform.GetChild(1).gameObject;
        timer.SetActive(false);

        backtosns = GameObject.FindGameObjectWithTag(Tags.Pop).transform.GetChild(0).gameObject;
        backtosns.SetActive(true);
        pop = GameObject.FindGameObjectWithTag(Tags.Pop).transform.GetChild(1).gameObject;
        pop.SetActive(false);
		confirmationPop.SetActive(false);

		uiMoney.GetComponent<Text>().text = UserData.Money + "";
		uiFollower.GetComponent<Text>().text = UserData.Follower +"";
        uiFollowing.GetComponent<Text>().text = UserData.Following + "";

        battleObj = GameObject.FindGameObjectWithTag(Tags.Enemy);

        
    }

    public void BackToSNS()
    {
        Time.timeScale = 1.0f;
        // CameraFade.StartAlphaFade(Color.black, false, 2f, 0.5f, () => { Application.LoadLevel("sns"); });
    }

    public void StartMove()
    {
		RemoveRangeSpot();
        isEnemyMoving = true;
        pauseButton.SetActive(true);
        startButton.SetActive(false);
        timer.SetActive(true);
        backtosns.SetActive(false);
        SoundManager.Instance.PlayBGM(6);

    }
	void RemoveRangeSpot()
	{
		unitRangeSpots = GameObject.FindGameObjectsWithTag(Tags.UnitRangeSpot);
		foreach(GameObject unitRangeSpot in unitRangeSpots)
		{
			//unitRangeSpot.SetActive(false);
			Destroy(unitRangeSpot);
		}
	}
    public void StopMove()
    {
        isEnemyMoving = false;
        pauseButton.SetActive(false);
        pop.SetActive(true);
        Time.timeScale = 0;
    }
	
    public void Restart()
    {
        isEnemyMoving = true;
        pauseButton.SetActive(true);
        pop.SetActive(false);
        Time.timeScale = 1;
    }

    public void Losed()
    {
        if (Lose == false)
        {
            isEnemyMoving = false;
            SoundManager.Instance.PlaySE(6);
            //StartCoroutine(wait());
            //Invoke("SecneLoad", 1.0f);
            Shutter.Close();
            SceneLoad.LoadSceneBtn();
            Lose = true;
        }
    }

    public void Unload()
    {
        Destroy(battleCanvas);
        Destroy(battleObj);
        battleCanvas = null;
        battleObj = null;
    }
}











