using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

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

    BulletManager BM;

    bool[] Weapons = new bool[5] { true, true, true, true, false };

    // Use this for initialization
    void Awake()
    {
        Screen.SetResolution(240, 160, false);

        this.bc2D = GetComponent<BoxCollider2D>();
        this.sr = GetComponent<SpriteRenderer>();
        this.BM = GetComponent<BulletManager>();
        this.speed = 0.1f;
        this.Direction = Vector2.zero;

        invulnerabilityOn = false;
        invulnerabilityDuration = 1f;
        health = 3; // will normally start with 3 hp
        weaponSelected = 0; // default weapon selected

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();

        Fire();

        SwitchWeapons();

        TurnOffInvulnerability();

        if (invulnerabilityOn)
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f);
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f);
        }


    }

    private void SwitchWeapons()
    {
        if (Input.GetKeyDown(KeyCode.R))
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
                bc2D.enabled = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!invulnerabilityOn)
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
            BM.Fire(weaponSelected);

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

}
