using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceOffScript : MonoBehaviour
{
    public GameObject fireplaceOnObject; // Set in editor
    PolygonCollider2D pc2D;
    SpriteRenderer sr;

    float nextFireExtinguish;
    public float fireDuration; // in seconds
    bool fireOn;

	// Use this for initialization
	void Awake ()
    {
        pc2D = gameObject.GetComponent<PolygonCollider2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        fireOn = false;
        fireDuration = 10f; 

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerFireBullet")
        {
            nextFireExtinguish = Time.time + fireDuration;
            fireOn = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.time >= nextFireExtinguish)
        {
            fireOn = false;
        }

        FireBehavior();
    }

    private void FireBehavior()
    {
        if (fireOn)
        {
            fireplaceOnObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            fireplaceOnObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
