using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {

    public int speed;
    public int shot;
    public int dead;
    public int direction;

    public void Start()
    {
        speed = Animator.StringToHash("speed");
        shot = Animator.StringToHash("shot");
        dead = Animator.StringToHash("dead");
        direction = Animator.StringToHash("direction");
    }

}
