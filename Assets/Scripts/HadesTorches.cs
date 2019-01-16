using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesTorches : MonoBehaviour
{
    CapsuleCollider2D cc2D;
    GameObject player;
    GameObject Hades;
    public SpriteRenderer sr;
    public GameObject lavaWavePrefab;

    System.Random rnd;

    float HP;
    public bool Alive;
    float Timer;
    public float TimeEnd;
	// Use this for initialization
	void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        if (this.Hades == null)
        {
            HadesScript temp2 = FindObjectOfType<HadesScript>();
            this.Hades = temp2.gameObject;
        }
        cc2D = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        HP = 50f;
        Alive = true;
        Timer = 10f;
        TimeEnd = Time.time + Timer;
        sr.enabled = false;
        rnd = new System.Random();
        //gameObject.SetActive(false);

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(HP <= 0)
        {
            Alive = false;
            sr.enabled = false;
            
        }
        if(!Alive)
        {

            gameObject.SetActive(false);
        }

        if (Time.time > TimeEnd)
        {
            TorchSetup();
            if (Time.time >= TimeEnd)
                Attack1();
        }

    }
    public void TorchSetup()
    {
        
        sr.enabled = true;
        Alive = true;
    }
    void Attack1()
    {


        if (Alive)
        {
            float temp = (float)rnd.NextDouble();
            Vector3 temploc = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - (2.5f - temp), -5f);
            var lavawave = (GameObject)Instantiate(
            lavaWavePrefab,
            Hades.transform.position,
            this.gameObject.transform.rotation);

            //lavawave.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1) * lavaSpeed);


            TimeEnd = Time.time + Timer;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerWaterBullet")
        {
            HP -= player.GetComponent<BulletManager>().weaponDamage;

            Destroy(collision.gameObject);
        }
    }
}
