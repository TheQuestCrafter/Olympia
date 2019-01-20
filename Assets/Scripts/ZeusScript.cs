using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusScript : MonoBehaviour
{
    Animator anim;
    public GameObject LBullet;
    GameObject player;
    public Transform zeusGun;

    float SpawnNextBullet, FireNextBullet;

    float initialAttackDelay; // To allow the scene transition to fade in
    bool bossFightStarted; // True after the initialAttackDelay has passed.

    // Use this for initialization
    void Awake()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        anim = gameObject.GetComponent<Animator>();

        FireNextBullet = 1f;
        SpawnNextBullet = Time.time + 3f;
        initialAttackDelay = 6f;
        bossFightStarted = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!bossFightStarted)
        {
            if(Time.time >= initialAttackDelay)
            {
                bossFightStarted = true;
            }
        }

        if(Time.time > SpawnNextBullet)
            Attack1();
        
    }


    void Attack1()
    {
        anim.SetBool("Firing", true);

        if (Time.time > SpawnNextBullet)
        {
            var proj = Instantiate(LBullet, zeusGun.position, Quaternion.identity);

            SpawnNextBullet = Time.time + FireNextBullet;
            anim.SetBool("Firing", false);
        }


    }
}
