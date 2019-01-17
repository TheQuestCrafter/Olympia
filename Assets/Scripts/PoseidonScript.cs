using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonScript : MonoBehaviour
{



    GameObject player;
    public GameObject BubblePrefab;
    public GameObject WaterBeam;
    public GameObject RightTentacle, LeftTentacle;
    PolygonCollider2D pc2D;
    public Transform poseidonGun;
    float Speed;
    float NextFireBubble, FireRateBubble;
    float NextFireLaser, FireRateLaser;
    bool PivotRight;
    float radius;
    Vector3 Direction;
    System.Random rand;
    float angle;

    // Use this for initialization
    void Awake()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        FireRateBubble = 2f;
        NextFireBubble = Time.time + FireRateBubble;
        FireRateLaser = 0.01f;
        NextFireLaser = Time.time + FireRateLaser;
        rand = new System.Random();
        Speed = 3f;
        angle = 220;
        Direction = new Vector3(0, 0, 1);
        pc2D = GetComponent<PolygonCollider2D>();

        radius = 15f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Attack1();
        Attack2();
        if (Time.time > 10)
            Attack3();


    }

    void Attack1()
    {
        //int temp = rand.Next(2);
        //if (RightTentacle.transform.rotation.z >= 11.30)
        //    PivotRight = false;
        //else if (RightTentacle.transform.rotation.z <= -49)
        //    PivotRight = true;


        if (RightTentacle.transform.eulerAngles.z >= -49)
        {
            RightTentacle.transform.Rotate(-Direction * Speed);
            //if (RightTentacle.transform.eulerAngles.z >= 11.30)
            //{
            //    PivotRight = false;
            //}
            //RightTentacle.transform.Rotate(new Vector3(0, 0, -1) * Speed * Time.deltaTime);
            //LeftTentacle.transform.Rotate(new Vector3(0, 0, 1) * Speed * Time.deltaTime);
        }
        //else if (RightTentacle.transform.eulerAngles.z <= 11.30)
        //{
        //    RightTentacle.transform.Rotate(Direction * Speed);
        //    if (RightTentacle.transform.eulerAngles.z <= -49)
        //    {
        //        PivotRight = true;
        //    }


        //    RightTentacle.transform.Rotate(new Vector3(0, 0, -1) * Speed * Time.deltaTime);
        //}



    }
    void Attack2()
    {
        if (Time.time > NextFireBubble)
        {
            int temp = rand.Next(90, 270);

            float angle = (float)temp;


            float projectileDirXposition = poseidonGun.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = poseidonGun.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
            Vector3 projectileMoveDirection = (projectileVector - poseidonGun.position).normalized * Speed;

            var proj = Instantiate(BubblePrefab, poseidonGun.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            NextFireBubble = Time.time + FireRateBubble + 20;
            
        }
        

    }
    void Attack3()
    {
       
            if (Time.time > NextFireLaser)
            {

            float projectileDirXposition = poseidonGun.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = poseidonGun.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector3 projectileMoveDirection = (projectileVector - poseidonGun.position).normalized * Speed;

            var proj = Instantiate(WaterBeam, poseidonGun.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            NextFireLaser = Time.time + FireRateLaser;
            angle -= 1f;

            Destroy(proj, 5);
        }
    }

}
