using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour 
{
    
    public float weaponDamage;
    public float fireRate; // time between firing
    float nextFire; // the time in-game when the player can fire again.
    public float bulletSpeed;
    public float bulletLife; // in seconds

    int numberofProjectiles;
    float radius, movespeed;


    public Transform bulletSpawn;
    public GameObject defaultBulletPrefab;
    public GameObject zeusBulletPrefab;
    public GameObject poseidonBulletPrefab;
    public GameObject hadesBulletPrefab;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip firing;
    [SerializeField]
    private AudioClip firingBeam;
    private int shotTracker;
    private int beamTracker;


    // Use this for initialization
    void Awake ()
    {
        weaponDamage = 5f;
        fireRate = 0.2f;
        bulletSpeed = 500;
        bulletLife = 2.8f;
        radius = 5f;
        movespeed = 5f;
        numberofProjectiles = 3;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    public void Fire(int weapon, bool vertical)
    {
        switch (weapon)
        {
            case 0:
                weaponDamage = 5f;
                fireRate = 0.2f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    defaultBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);
                    FiringSound(weapon);
                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet

                    if(vertical)
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed);
                    }
                    else
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);
                    }

                    


                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);
                }
                break;
            case 1:
                weaponDamage = 5f;
                fireRate = 0.55f;
                if (Time.time > nextFire)
                {
                    FiringSound(weapon);

                    if (vertical)
                    {
                        float angleStep = -15f;
                        float angle = 15f;


                        for (int i = 0; i <= numberofProjectiles - 1; i++)
                        {

                            float projectileDirXposition = bulletSpawn.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                            float projectileDirYposition = bulletSpawn.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                            Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
                            Vector3 projectileMoveDirection = (projectileVector - bulletSpawn.position).normalized * movespeed;

                            var proj = Instantiate(zeusBulletPrefab, bulletSpawn.position, Quaternion.identity);
                            proj.GetComponent<Rigidbody2D>().velocity =
                                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                            angle += angleStep;
                            Destroy(proj, bulletLife);

                            nextFire = Time.time + fireRate;
                        }


                    }
                    else
                    {
                        float angleStep = 15f;
                        float angle = 75f;

                        for (int i = 0; i <= numberofProjectiles - 1; i++)
                        {

                            float projectileDirXposition = bulletSpawn.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
                            float projectileDirYposition = bulletSpawn.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

                            Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
                            Vector3 projectileMoveDirection = (projectileVector - bulletSpawn.position).normalized * movespeed;

                            var proj = Instantiate(zeusBulletPrefab, bulletSpawn.position, Quaternion.identity);
                            proj.GetComponent<Rigidbody2D>().velocity =
                                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                            angle += angleStep;
                            Destroy(proj, bulletLife);
                        }

                        nextFire = Time.time + fireRate;
                    }
                }
               
                break;
            case 2:
                weaponDamage = 0.42f;
                fireRate = 0.01f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    poseidonBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);
                    FiringSound(weapon);

                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet
                    if (vertical)
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed);
                    }
                    else
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);
                    }


                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);

                }
                break;
            case 3:
                weaponDamage = 3f;
                fireRate = 0.3f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    hadesBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);
                    FiringSound(weapon);

                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet
                    if (vertical)
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1) * bulletSpeed);
                    }
                    else
                    {
                        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);
                    }




                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);

                }
                break;
         
                
        }

    }

    private void FiringSound(int shotType)
    {
        
        if (shotType == 2)
        {
            beamTracker++;
            if (beamTracker > 10)
            {
                audioSource.PlayOneShot(firingBeam);
            }
        }
        else
        {
                audioSource.PlayOneShot(firing);
        }
    }

}
