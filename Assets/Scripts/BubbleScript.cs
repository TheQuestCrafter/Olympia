using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour {

    Rigidbody2D rb2D;
    CircleCollider2D cc2D;
    public GameObject LightingBall;
    float pushForce;

	// Use this for initialization
	void Awake ()
    {
        rb2D = GetComponent<Rigidbody2D>();
        cc2D = GetComponent<CircleCollider2D>();
        pushForce = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BottomWall")
        {
            rb2D.AddForce(Vector2.up * pushForce, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "LeftTentacle")
        {
            rb2D.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "RightTentacle")
        {
            rb2D.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
        }
        else if (collision.gameObject.tag == "Enemy")
        {
            rb2D.AddForce(Vector2.down * pushForce, ForceMode2D.Impulse);
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerLightingBullet")
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(this.gameObject);
            Destroy(collision.gameObject);

            var LBall = (GameObject)Instantiate(
                LightingBall, this.transform.position, this.transform.rotation);

            Destroy(LBall, 12);

        }
        if (collision.tag == "PlayerBullet" || collision.tag == "PlayerWaterBullet" || collision.tag == "PlayerFireBullet")
        {

            Destroy(collision.gameObject);


        }

    }

}
