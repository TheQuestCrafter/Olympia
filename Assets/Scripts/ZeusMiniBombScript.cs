using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusMiniBombScript : MonoBehaviour
{

    public GameObject Bomb;
    System.Random rand;
    float TimeTillBoom;
    Rigidbody2D rb2D;
	// Use this for initialization
	void Start ()
    {

        rand = new System.Random();
        float temp = (float)rand.NextDouble();
        rb2D = GetComponent<Rigidbody2D>();
        TimeTillBoom = Time.time + 7f + temp;


       
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
}
