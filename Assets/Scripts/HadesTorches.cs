using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesTorches : MonoBehaviour
{
    CapsuleCollider2D cc2D;
    GameObject player;
    GameObject Hades;
    GameObject Kronos;
    public SpriteRenderer sr;
    public GameObject lavaWavePrefab;

    System.Random rnd;

    public float HP;
    public bool Alive;
    float Timer;
    public float TimeEnd;
    // Use this for initialization
    void Awake()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        //if (this.Hades == null)
        //{
        //    HadesScript temp2 = FindObjectOfType<HadesScript>();
        //    this.Hades = temp2.gameObject;
        //}
        cc2D = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        HP = 10f;
        Alive = true;
        Timer = 10f;
        TimeEnd = Time.time + Timer;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HP <= 0 || Time.time > TimeEnd)
        {
            Alive = false;
            Destroy(this.gameObject);

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
