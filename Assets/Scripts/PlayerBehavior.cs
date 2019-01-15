using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    IEnumerator LightingDamage(float damageDuration, int damageCount)
    {
        lightingOn = true;
        int currentCount = 0;
        while (currentCount < damageCount)
        {
            weaponDamage = 5;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
        lightingOn = false;
    }

    private bool lightingOn;

    BoxCollider2D bc2D;
    SpriteRenderer sr;
    public float speed;
    public float moveHorizontal, moveVertical;
    Vector2 Direction; // made with moveHor and moveVer

    public bool invulnerabilityOn;
    public float invulnerabilityDuration;
    public float invulnerabilityOffTime;
    public int health;
    public int weaponSelected;

    public int weaponDamage;
    public float fireRate; // time between firing
    float nextFire; // the time in-game when the player can fire again.
    public float bulletSpeed;
    public float bulletLife; // in seconds

    public Transform bulletSpawn;
    public GameObject defaultBulletPrefab;
    public GameObject zeusBulletPrefab;
    public GameObject poseidonBulletPrefab;
    public GameObject hadesBulletPrefab;

    // Use this for initialization
    void Awake ()
    {
        Screen.SetResolution(240, 160, false);

        this.bc2D = GetComponent<BoxCollider2D>();
        this.sr = GetComponent<SpriteRenderer>();

        this.speed = 0.1f;
        this.Direction = Vector2.zero;

        invulnerabilityOn = false;
        invulnerabilityDuration = 1f;
        health = 3; // will normally start with 3 hp
        //weaponSelected = 3; // default weapon selected
        weaponDamage = 5;
        fireRate = 0.2f;
        bulletSpeed = 500;
        bulletLife = 2.8f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Movement();
        Fire();

        TurnOffInvulnerability();

        if(invulnerabilityOn)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    private void TurnOffInvulnerability()
    {
        if (invulnerabilityOn)
        {
            if (Time.time >= invulnerabilityOffTime)
            {
                invulnerabilityOn = false;
                bc2D.enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!invulnerabilityOn)
        {
            invulnerabilityOn = true;
            invulnerabilityOffTime = Time.time + invulnerabilityDuration;
            bc2D.enabled = false;
        }
    }

    private void Fire()
    {
        if (Input.GetKey("space"))
        {
            CheckForShot();
        }
    }

    private void Movement()
    {
        this.Direction.x = this.Direction.y = 0; // reset the direction to 0
        moveHorizontal = Input.GetAxis("Horizontal"); // check horizontal input
        moveVertical = Input.GetAxis("Vertical"); // check vertical input

        this.Direction = new Vector2(moveHorizontal, moveVertical); // create new vector based on input combo
        this.Direction.Normalize(); // normalize so that the direction is consistent
        this.transform.Translate(Direction.x * speed, Direction.y * speed, 0); // move player
    }

    void CheckForShot()
    {
        switch(weaponSelected)
        {
            case 0: 
                {
                    // default
                    FireDefault();
                    break;
                }
            case 1:
                {
                    // Zeus
                    FireZeus();
                    break;
                }
            case 2:
                {
                    // Poseidon
                    FirePoseidon();
                    break;
                }
            case 3:
                {
                    // Hades
                    FireHades();
                    break;
                }
        }
    }

    void FireDefault()
    {
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
    }

    void FireZeus()
    {
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
    }
    void FirePoseidon()
    {
        weaponDamage = 1;
        fireRate = 0.01f;
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
    }
    void FireHades()
    {
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
    }
}
