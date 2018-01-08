using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour
{

    private GameObject MainCam;
    private GameObject SubCam;

    void Start()
    {
        MainCam = GameObject.Find("Main Camera");
        SubCam = GameObject.Find("Sub Camera");

        SubCam.SetActive(false);
    }

    void ButtonClick()
    {
         if (MainCam.activeSelf)
         {
             MainCam.SetActive(false);
             SubCam.SetActive(true);
         }
         else
         {
             MainCam.SetActive(true);
             SubCam.SetActive(false);
         }
    }

}
