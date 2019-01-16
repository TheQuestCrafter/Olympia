using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HadesLavaWaveScript : MonoBehaviour
{
    GameObject player;
    BoxCollider2D bc2D;
    Vector2 Direction;
    float speed;
    // Use this for initialization
    void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        bc2D = GetComponent<BoxCollider2D>();
        speed = 0.3f;
    }

    private void FixedUpdate()
    {
        Direction = new Vector2(0, -1);
        Direction.Normalize();
        this.gameObject.transform.Translate(Direction * speed);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<PlayerBehavior>().health--;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerBehavior>().health-= 0.5f;
            player.GetComponent<PlayerBehavior>().TurnOnInvuln();
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
