using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SNSUI : MonoBehaviour
{

    private GameObject notice;
    private GameObject backtomenu;
    private GameObject backhome;
    public Canvas tweetWindow;
    public Canvas snsCanvas;
    private bool Changed = false;

    void Start()
    {
        SoundManager.Instance.PlaySE(11);

        if (Changed == false)
        {
            SoundManager.Instance.PlayBGM(2);
            Changed = true;
        }

        notice = GameObject.FindGameObjectWithTag(Tags.UI).transform.GetChild(2).gameObject;
        notice.SetActive(false);
        backtomenu = GameObject.FindGameObjectWithTag(Tags.UI).transform.GetChild(0).gameObject;
        backtomenu.SetActive(true);
        backhome = GameObject.FindGameObjectWithTag(Tags.UI).transform.GetChild(1).gameObject;
        backhome.SetActive(false);
        tweetWindow = GameObject.FindGameObjectWithTag("TweetWindow").GetComponent<Canvas>();

    }

    public void OnStartButtonClicked()
    {
        notice.SetActive(true);
    }

    public void DialogYesClicked()
    {
        CameraFade.StartAlphaFade(Color.black, false, 2f, 0.5f, () => { Application.LoadLevel("battle"); });
        SoundManager.Instance.PlaySE(0);
    }

    public void DialogNoClicked()
    {
        notice.SetActive(false);
    }

    public void BacktoMenu()
    {
        CameraFade.StartAlphaFade(Color.black, false, 2f, 0.5f, () => { Application.LoadLevel("home"); });
        SoundManager.Instance.PlaySE(1);
    }

    public void Backhome()
    {
        backtomenu.SetActive(true);
        backhome.SetActive(false);
        tweetWindow.enabled = false;
    }
     
    public void TalkButtonClicked()
    {
        backtomenu.SetActive(false);
        backhome.SetActive(true);
        if (tweetWindow.enabled == false)
        {
            tweetWindow.enabled = true;
        }
    }

    public void Unload()
    {
        //Destroy(notice);
        //Destroy(tweetWindow);
        //Destroy(snsCanvas);
        notice = null;
        tweetWindow = null;
        snsCanvas = null;
    }
}
