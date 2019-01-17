using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusHandScript : MonoBehaviour {

    public Transform gunLocation;
    public GameObject bulletPrefab;
    public float fireRate;
    float nextFire; // the time in-game when the player can fire again.
    public float bulletSpeed;
    public float bulletLife; // in seconds

    // Use this for initialization
    void Awake ()
    {
        
        fireRate = 3f;
        bulletSpeed = 500;
        bulletLife = 2.8f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Attack();
	}

    void Attack()
    {
        if (Time.time > nextFire)
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(
            bulletPrefab,
            gunLocation.position,
            gunLocation.rotation);

            // *** nextFire = time right now + the constant cooldown of 0.3f seconds. AKA you're stating the next time you can fire is at that time or later, not earlier.
            nextFire = Time.time + fireRate;

            // Add velocity to the bullet
         
            bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 0) * bulletSpeed);
      




            // Destroy the bullet after bulletLife seconds
            Destroy(bullet, bulletLife);
        }
    }
}
