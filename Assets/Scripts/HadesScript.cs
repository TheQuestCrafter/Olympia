using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesScript : MonoBehaviour
{

    float Attack1FR, Attack1NF;
    float Attack1_2FR, Attack1_2NF;
    float Attack2FR, Attack2NF;
    GameObject player;
    public GameObject fireBallPrefab;
    public GameObject hadesBombPrefab;
    public GameObject lavaWavePrefab;
    public Transform torch1, torch2;
    public GameObject LeftTorchPrefab, RightTorchPrefab;
    public Transform hadesGun;
    GameObject LeftTorch, RightTorch;
    float BigBombSpeed;

    bool moveRight;
    Vector2 Direction;
    float speed;

    float lavaSpeed;

    BossScriptTest bossScript;

    // Use this for initialization
    void Awake()
    {
        moveRight = true;
        Direction = new Vector2(1, 0);
        speed = 0.08f;
        BigBombSpeed = 120f;

        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        Attack1FR = 16f;
        Attack1NF = Time.time + Attack1FR;
        Attack1_2FR = 15f;
        Attack1_2NF = Time.time + Attack1_2FR;
        Attack2FR = 6f;
        Attack2NF = Time.time + Attack2FR;

        bossScript = GetComponent<BossScriptTest>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();

        if (bossScript.hp <= 0)
            Attack2();
        if (Time.time > Attack1NF && bossScript.hp <= 0)
        {
            SpawnTorches();
            LeftTorch = GameObject.FindGameObjectWithTag("LeftTorch");
            RightTorch = GameObject.FindGameObjectWithTag("RightTorch");

        }

        if (Time.time > Attack1_2NF && bossScript.hp <= 0)
        {

            if (LeftTorch != null && RightTorch != null)
            {
                Attack1(true, true);
                Destroy(LeftTorch);
                Destroy(RightTorch);
            }
            else if (LeftTorch == null && RightTorch != null)
            {
                Attack1(false, true);
                Destroy(RightTorch);
            }
            else if (LeftTorch != null && RightTorch == null)
            {
                Attack1(true, false);
                Destroy(LeftTorch);
            }
            else if (LeftTorch == null && RightTorch == null)
            {
                Attack1(false, false);
            }

        }
    }


    void Attack1(bool torch1On, bool torch2On)
    {
        if (torch1On || torch2On)
        {
            var lavawave = (GameObject)Instantiate(
            lavaWavePrefab,
            this.transform.position,
            this.transform.rotation);

            Attack1_2NF = Time.time + Attack1_2FR;
        }
        else
        {
            Attack1_2NF = Time.time + Attack1_2FR;
        }
    }

    void SpawnTorches()
    {

        var torch_1 = (GameObject)Instantiate(LeftTorchPrefab, torch1.transform.position, torch1.transform.rotation);
        var torch_2 = (GameObject)Instantiate(RightTorchPrefab, torch2.transform.position, torch2.transform.rotation);


        Attack1NF = Time.time + Attack1FR;


    }

    void Attack2()
    {
        if (Time.time >= Attack2NF)
        {

            var BigBomb = (GameObject)Instantiate(
            hadesBombPrefab,
            hadesGun.transform.position,
            this.gameObject.transform.rotation);

            Attack2NF = Time.time + Attack2FR;
        }
    }

    void Movement()
    {
        if (bossScript.hp <= 0)
        {
            this.transform.Translate(new Vector2(0, 0));
        }
        else if (moveRight)
            this.transform.Translate(Direction * speed);
        else
            this.transform.Translate(-Direction * speed);

        if (this.transform.position.x >= 3)
            moveRight = false;
        else if (this.transform.position.x <= -3)
            moveRight = true;


    }


}
