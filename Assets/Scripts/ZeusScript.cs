using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusScript : MonoBehaviour
{
    Animator anim;
    public GameObject LBullet;
    GameObject player;
    public Transform zeusGun;
    float radius;
    bool LockedOn;
    float SpawnNextBullet, FireNextBullet;
    System.Random rand;

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

        radius = 5f;

        FireNextBullet = 1f;
        rand = new System.Random();
        LockedOn = false;

        initialAttackDelay = 3.5f;
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

        if(bossFightStarted)
            Attack1();
        
    }

    void Attack1()
    {
        anim.SetBool("Firing", true);

        if (Time.time > SpawnNextBullet)
        {
            //int temp = rand.Next(230, 300);

            //float angle = (float)temp;

            //float projectileDirXposition = zeusGun.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            //float projectileDirYposition = zeusGun.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            //Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
            //Vector3 projectileMoveDirection = (projectileVector - zeusGun.position).normalized * 10;

            var proj = Instantiate(LBullet, zeusGun.position, Quaternion.identity);
            //proj.GetComponent<Rigidbody2D>().velocity =
            //    new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);
            


            SpawnNextBullet = Time.time + FireNextBullet;
            anim.SetBool("Firing", false);
        }


    }
}
