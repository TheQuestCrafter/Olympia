using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusBulletScript : MonoBehaviour
{


    CircleCollider2D cc2D;
    GameObject player;
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
        if ((cc2D.transform.position.x != player.transform.position.x) && !lockedOn)
        {
            if (cc2D.transform.position.x < 3)
            {
                lockedOn = true;
            }

            if (!lockedOn)
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
}
