using UnityEngine;
using System.Collections;

public class UnitShot : MonoBehaviour {

    public float bulletFowardForce;
    public GameObject bulletEmitter;
    public GameObject bullet;
    public bool isBulletOut = false;
    public GameObject enemy;
    public float shotInterval = 0.5f;
    public float damage;

    //references
    private EnemyMovement enemyMovement;
    private float shotTimer = 0f;
    private Health enemyHealth;
    private ArrayList enemies = new ArrayList();
	private Health unitHealth;
    private Animator unitAnim;
    private HashIDs hash;
    private Animator enemyAnim;

    //shot direction towards target
    private Vector3 shotDir;

    private bool isShooting = false;
    


	void Start () {
        unitHealth = gameObject.transform.parent.gameObject.GetComponent<Health>();
        unitAnim = transform.parent.GetChild(0).transform.GetChild(0).GetComponent<Animator>();
        
        hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();

    }
	
	void Update () {
        if (isShooting)
        {
            if(enemy!=null)
                ShotEffect();
            else
            {
                //set back animation to idle with the current direction
                unitAnim.SetBool(hash.shot, false);

                isShooting = false;
                isBulletOut = false;
            }
        }
		if (enemy != null)
		{
			if (enemyHealth.health < 0 || enemyHealth.health == 0)
			{
				Destroy(enemy,0.9f);
				enemies.Remove(0);
				if (enemies.Count > 0)
				{
					ShotNextEnemyTarget((GameObject)enemies[0]);
				}
			}
		}
	}

    void ShotEffect()
    {
        
        shotTimer += Time.deltaTime;
        
        if(shotTimer >= shotInterval)
        {
            shotTimer = 0;
            isBulletOut = false;
        }
        if (!isBulletOut && unitHealth.health > 0)
        {
            enemyHealth.TakeDamage(damage);
            if (enemyHealth.health > 0)
            {
                GameObject temporaryBullet = (GameObject)Instantiate(bullet, bulletEmitter.transform.position, bulletEmitter.transform.rotation);
                shotDir = enemy.transform.position - temporaryBullet.transform.position + Vector3.up * 0.5f;

                Rigidbody tempRidgidbody = temporaryBullet.GetComponent<Rigidbody>();
                tempRidgidbody.AddForce(shotDir * bulletFowardForce);
                Destroy(temporaryBullet, 0.5f);
                isBulletOut = true;

                //enemy taking damage


                //set to shot animation according to direction
                unitAnim.SetBool(hash.shot, true);

                float angle = Vector3.Angle(shotDir, transform.parent.forward);
                //Debug.Log(angle);
                unitAnim.SetInteger(hash.direction, GetDirection(angle));
            }
            else
            {
                enemyAnim.SetBool(hash.dead, true);
            }
        }
        
    }

    int GetDirection(float angle)
    {
        //angle += 90;
        if (gameObject.transform.position.x < enemy.transform.position.x)
        {
            if (angle <= 22.5)
            {
                return 0;
            }
            else if (angle > 22.5 && angle <= 67.5)
            {
                return 1;
            }
            else if (angle > 67.5 && angle <= 112.5)
            {
                return 2;
            }
            else if (angle > 112.5 && angle <= 157.5)
            {
                return 3;
            }
            else if (angle > 157.5)
            {
                return 4;
            }
        }
        else
        {
            if (angle <= 22.5)
            {
                return 0;
            }
            else if (angle > 22.5 && angle <= 67.5)
            {
                return 7;
            }
            else if (angle > 67.5 && angle <= 112.5)
            {
                return 6;
            }
            else if (angle > 112.5 && angle <= 157.5)
            {
                return 5;
            }
            else if (angle > 157.5)
            {
                return 4;
            }
        }
        return 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.EnemyBody && !isShooting)
        {
            enemy = other.gameObject.transform.parent.gameObject;
			enemies.Add(enemy);
			ShotNextEnemyTarget(enemy);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.EnemyBody)
        {
            //Debug.Log(other.tag);
            //set back to idle with current direction
            unitAnim.SetBool(hash.shot, false);

            isShooting = false;
        }
    }
	private void ShotNextEnemyTarget(GameObject enemy)
	{
		enemyMovement = enemy.GetComponent<EnemyMovement>();
		enemyHealth = enemy.GetComponent<Health>();
		enemyAnim = enemy.transform.GetChild(0).GetChild(0).GetComponent<Animator>();
		enemyMovement.unit = gameObject;
		enemyMovement.unitShot = this;
		isShooting = true;
	}
}
