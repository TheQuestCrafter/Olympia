using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    CircleCollider2D cc2D;
    GameObject player;
    int hp;
    Vector2 Direction;
    public float speed;
    bool lockedOn;

	// Use this for initialization
	void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        cc2D = GetComponent<CircleCollider2D>();
        lockedOn = false;
        speed = 0.1f;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if((cc2D.transform.position.x != player.transform.position.x) && !lockedOn)
        {
            if(cc2D.transform.position.x < -3)
            {
                lockedOn = true;
            }

            if(!lockedOn)
            {
                Direction = new Vector2(-1, 0);
                Direction.Normalize();
            }
            else
            {
                Direction = new Vector2(player.transform.position.x - cc2D.transform.position.x, player.transform.position.y - cc2D.transform.position.y);
                Direction.Normalize();
                
            }
            this.gameObject.transform.Translate(Direction * speed);
        }
        else
        {
            this.gameObject.transform.Translate(Direction * speed);
        }
        
	}

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            //if()
            player.GetComponent<PlayerBehavior>().health--;
        }
    }
}
