using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{

    public float health;
    public GameObject healthBar;
    public GameObject healthBarCanvas;

    private float fullHealth;
    private GameObject mainCamera;

    void Start()
    {
        fullHealth = health;
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera);
    }

    void Update()
    {
        if (healthBarCanvas != null)
        {
            healthBarCanvas.transform.LookAt(mainCamera.transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            healthBar.transform.localScale = new Vector3(health / fullHealth, 1, 1);
        }
        else
        {
			/*
            Debug.Log(gameObject.tag);
            if (gameObject.tag == Tags.Unit || gameObject.tag == Tags.TakashiGuard)
            {
                Destroy(gameObject, 0.9f);
            }
            else if (gameObject.tag == Tags.Enemy)
            {
                Destroy(gameObject, 0.90f);
            }
			 * */
        }
    }

}
