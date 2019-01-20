using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, yMin, yMax;
}
public class PlayerBehavior : MonoBehaviour
{
    Rigidbody2D rb2D;
    PolygonCollider2D pc2D;
    SpriteRenderer sr;
    public float speed;
    public float moveHorizontal, moveVertical;
    Vector2 Direction; // made with moveHor and moveVer

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip damage;
    private int healthTracker;

    public bool Vertical;

    public bool invulnerabilityOn;
    public float invulnerabilityDuration;
    public float invulnerabilityOffTime;
    public float health;
    public int weaponSelected;

    BulletManager BM;
    SceneManager sceneManager = new SceneManager();
    public Boundary boundary;
    public bool[] Weapons = new bool[5] { true, false, false, false, false };

    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(1200, 800, false);
        this.rb2D = gameObject.GetComponent<Rigidbody2D>();
        this.pc2D = GetComponent<PolygonCollider2D>();
        this.sr = GetComponent<SpriteRenderer>();
        this.BM = GetComponent<BulletManager>();
        this.speed = 0.1f;
        this.Direction =  Vector2.zero;
        invulnerabilityOn = false;
        invulnerabilityDuration = 1f;
        health = 3; // will normally start with 3 hp
        weaponSelected = 0; // default weapon selected
        healthTracker = 3;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();

        Fire();

        

        TurnOffInvulnerability();

        if (healthTracker > health)
        {
            audioSource.PlayOneShot(damage);
            healthTracker--;
        }
        if (invulnerabilityOn)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
            Respawn();
        }

        rb2D.position = new Vector2
        (
            Mathf.Clamp(rb2D.position.x, boundary.xMin, boundary.xMax),
            Mathf.Clamp(rb2D.position.y, boundary.yMin, boundary.yMax)
        );

        UnlockWeapons();


    }

    public void Update()
    {
        SwitchWeapons();

    }

    private void UnlockWeapons()
    {
       if(SceneManager.GetActiveScene().buildIndex >= 3)
        {
            Weapons[1] = true;
        }
       if(SceneManager.GetActiveScene().buildIndex >= 5)
        {
            Weapons[2] = true;
        }
        if (SceneManager.GetActiveScene().buildIndex >= 7)
        {
            Weapons[3] = true;
        }
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SwitchWeapons()
    {
       // if (Input.GetKeyDown(KeyCode.R)
       if(Input.GetKeyDown(KeyCode.R))
        {
            if (weaponSelected == 0 && Weapons[1] == true)
            {
                weaponSelected = 1;
            }
            else if (weaponSelected == 1 && Weapons[2] == true)
            {
                weaponSelected = 2;
            }
            else if (weaponSelected == 2 && Weapons[3] == true)
            {
                weaponSelected = 3;
            }
            else
            {
                weaponSelected = 0;
            }
        }
    }

    private void TurnOffInvulnerability()
    {
        if (invulnerabilityOn)
        {
            if (Time.time >= invulnerabilityOffTime)
            {
                invulnerabilityOn = false;
                pc2D.enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TurnOnInvuln();


        
    }

    public void TurnOnInvuln()
    {
        if (!invulnerabilityOn)
        {
            //if(health % 1f != 0)
            //{
            //    health = (float)Math.Floor((double)health);
                
            //}
            invulnerabilityOn = true;
            invulnerabilityOffTime = Time.time + invulnerabilityDuration;
            pc2D.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            health--;
            Destroy(collision.gameObject);
            TurnOnInvuln();
        }
    }
    private void Fire()
    {
        if (Input.GetKey("space"))
        {
            BM.Fire(weaponSelected, Vertical);
        }
    }

    private void Movement()
    {
        this.Direction.x = this.Direction.y = 0; // reset the direction to 0
        moveHorizontal = Input.GetAxis("Horizontal"); // check horizontal input
        moveVertical = Input.GetAxis("Vertical"); // check vertical input



        if (Vertical)
        {
            this.Direction = new Vector2(moveVertical, -moveHorizontal); // create new vector based on input combo
            this.Direction.Normalize(); // normalize so that the direction is consistent
            this.transform.Translate(Direction.x * speed, Direction.y * speed, 0); // move player
           //rb2D.MovePosition((rb2D.position + Velocity) * Time.fixedDeltaTime);
           
        }
        else
        {
            this.Direction = new Vector2(moveHorizontal, moveVertical); // create new vector based on input combo
            this.Direction.Normalize(); // normalize so that the direction is consistent
            this.transform.Translate(Direction.x * speed, Direction.y * speed, 0); // move player
            //rb2D.MovePosition((rb2D.position + Velocity) * Time.fixedDeltaTime);
            
        }

    }

}
