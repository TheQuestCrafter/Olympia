using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusHandScript : MonoBehaviour
{

    public Transform gunLocation;
    public GameObject bulletPrefab;
    public GameObject Bomb;
    public float fireRate;
    float nextFire; // the time in-game when the player can fire again.
    public float bulletSpeed;
    public float bulletLife; // in seconds

    float initialAttackDelay; // To allow the scene transition to fade in
    bool bossFightStarted; // True after the initialAttackDelay has passed.


    // Use this for initialization
    void Awake()
    {

        fireRate = 8f;
        bulletSpeed = 400;
        bulletLife = 3f;

        initialAttackDelay = 3.5f;
        bossFightStarted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!bossFightStarted)
        {
            if (Time.time >= initialAttackDelay)
            {
                bossFightStarted = true;
            }
        }

        if (bossFightStarted)
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
