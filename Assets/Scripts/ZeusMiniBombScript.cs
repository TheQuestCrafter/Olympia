using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusMiniBombScript : MonoBehaviour
{

    public GameObject Bomb;
   
    public float TimeTillBoom;
    Rigidbody2D rb2D;

    System.Random rand = new System.Random();
    public float temp, temp2;
    // Use this for initialization
    void Start ()
    {
        temp = (float)rand.NextDouble();
        temp2 = (float)rand.NextDouble();

        rb2D = GetComponent<Rigidbody2D>();


        SpawnOrb();
       
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Time.time > TimeTillBoom)
        {

            var bomb = (GameObject)Instantiate(Bomb, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    void SpawnOrb()
    {
        TimeTillBoom = Time.time + temp + temp2;
    }
}



