using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesScript : MonoBehaviour
{

    //public bool Torch1On, Torch2On;
    //float Attack1FR, Attack1NF;
    float Attack2FR, Attack2NF;
    GameObject player;
    public GameObject fireBallPrefab;
    public GameObject hadesBombPrefab;
    public GameObject lavaWavePrefab;
    public GameObject torch1, torch2;
    HadesTorches hadesTorchScript;
    public Transform hadesGun;

    float BigBombSpeed;

    bool moveRight;
    Vector2 Direction;
    float speed;

    float lavaSpeed;

	// Use this for initialization
	void Awake ()
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


        hadesTorchScript = GetComponent<HadesTorches>();
        //Torch1On = false;
        //Torch2On = false;
        //Attack1FR = 20f;
        //Attack1NF = Time.time + Attack1FR;
        Attack2FR = 6f;
        Attack2NF = Time.time + Attack2FR;
        lavaSpeed = 450f;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Movement();
        Attack2();
	}


    

    void Attack2()
    {
        if(Time.time >= Attack2NF)
        {

            var BigBomb = (GameObject)Instantiate(
            hadesBombPrefab,
            hadesGun.transform.position,
            this.gameObject.transform.rotation);


            //BigBomb.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * BigBombSpeed);

            Attack2NF = Time.time + Attack2FR;
        }
    }

    void Movement()
    {
        if (moveRight)
            this.transform.Translate(Direction * speed);
        else
            this.transform.Translate(-Direction * speed);

        if (this.transform.position.x >= 3)
            moveRight = false;
        else if(this.transform.position.x <= -3)
            moveRight = true;


    }

   
}
