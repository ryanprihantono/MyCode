using UnityEngine;
using System.Collections;

public class ShutterScript : MonoBehaviour
{

    public Animator animator;
    public Canvas Shutter;

    // void Awake()
    // {
    //     Invoke("Sort", 0.5f);
    // }


    void Start()
    {
        // Shutter = GameObject.Find("Shutter").GetComponent<canvas>();
        animator = GetComponent<Animator>();
        animator.SetBool("close", false);
        // Invoke("Sort", 0.5f);
        // Shtr.sortingOrder = -1000;
    }

    // void Sort()
    // {
    //     Shutter.sortingOrder = -1000;
    // }

    public void Close()
    {
        Shutter.sortingOrder = 800;
        animator.SetBool("close", true);
        SoundManager.Instance.PlaySE(10);
    }

   
}