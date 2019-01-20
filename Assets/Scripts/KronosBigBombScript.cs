using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KronosBigBombScript : MonoBehaviour
{

    // Use this for initialization
    GameObject player;
    public GameObject projectile;
    CircleCollider2D cc2D;
    Vector2 Direction;
    float speed;

    int numberOfProjectiles;
    Vector2 startPoint;
    float radius, moveSpeed;
    System.Random rnd;
    float explodeTime;


    // Use this for initialization
    void Awake()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        cc2D = GetComponent<CircleCollider2D>();
        speed = 0.05f;
        rnd = new System.Random();
        explodeTime = rnd.Next(1);
        explodeTime += (float)rnd.NextDouble();

        numberOfProjectiles = 8;

        radius = 5f;
        moveSpeed = 5f;

    }

    private void FixedUpdate()
    {
        Direction = new Vector2(0, -1);
        Direction.Normalize();
        this.gameObject.transform.Translate(Direction * speed);


        if (Time.time >= explodeTime)
        {
            startPoint = this.transform.position; //Camera.main.ScreenToWorldPoint(Input.mousePosition);
            SpawnProjectiles(numberOfProjectiles);
        }
    }


    void SpawnProjectiles(int numberOfProjectiles)
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= numberOfProjectiles - 1; i++)
        {

            float projectileDirXposition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - startPoint).normalized * moveSpeed;

            var proj = Instantiate(projectile, startPoint, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            Destroy(proj, 2f);
            angle += angleStep;
        }
        explodeTime = Time.time + 10;
    }


    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
