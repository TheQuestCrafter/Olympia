using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusScript : MonoBehaviour
{

    public GameObject LBullet;
    GameObject player;
    public Transform zeusGun;
    float radius;
    bool LockedOn;
    float SpawnNextBullet, FireNextBullet;
    System.Random rand;

    // Use this for initialization
    void Awake()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        radius = 5f;

        FireNextBullet = 1f;
        rand = new System.Random();
        LockedOn = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Attack1();
    }

    void Attack1()
    {


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

        }


    }
}
