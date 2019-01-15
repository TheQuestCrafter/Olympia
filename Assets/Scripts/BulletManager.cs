using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletManager : MonoBehaviour 
{
    private bool onFire;

    IEnumerator FireDamage(float damageDuration, int damageCount, int damageAmount)
    {
        onFire = true;
        int currentCount = 0;
        while (currentCount < damageCount)
        {
          // damageAmount;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
        onFire = false;
    }


    public int weaponDamage;
    public float fireRate; // time between firing
    float nextFire; // the time in-game when the player can fire again.
    public float bulletSpeed;
    public float bulletLife; // in seconds
    public float zeusLargeBulletLife; // in seconds

    public Transform bulletSpawn;
    public GameObject defaultBulletPrefab;
    public GameObject zeusBulletPrefab;
    public GameObject zeusLargeBulletPrefab;
    public GameObject poseidonBulletPrefab;
    public GameObject hadesBulletPrefab;

    // Use this for initialization
    void Awake ()
    {
        weaponDamage = 5;
        fireRate = 0.2f;
        bulletSpeed = 500;
        bulletLife = 2.8f;
        zeusLargeBulletLife = 2f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}

    public void Fire(int weapon)
    {
        switch (weapon)
        {
            case 0:
                weaponDamage = 5;
                fireRate = 0.2f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    defaultBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);


                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);
                }
                break;
            case 1:
                weaponDamage = 5;
                fireRate = 0.2f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    zeusBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);


                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);

                }
                break;
            case 2:
                weaponDamage = 1;
                fireRate = 0.01f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    poseidonBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);


                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);

                }
                break;
            case 3:
                weaponDamage = 5;
                fireRate = 0.2f;
                if (Time.time > nextFire)
                {
                    // Create the Bullet from the Bullet Prefab
                    var bullet = (GameObject)Instantiate(
                    hadesBulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

                    // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
                    nextFire = Time.time + fireRate;

                    // Add velocity to the bullet
                    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0) * bulletSpeed);

                    

                    // Destroy the bullet after bulletLife seconds
                    Destroy(bullet, bulletLife);

                }
                break;
                
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "PlayerLightingBullet" && collision.gameObject.tag == "Enemy")
        {
            var largeBullet = (GameObject)Instantiate(zeusLargeBulletPrefab, this.transform);



            Destroy(largeBullet, zeusLargeBulletLife);

        }
    }
}
