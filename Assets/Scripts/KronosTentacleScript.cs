using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KronosTentacleScript : MonoBehaviour {

    GameObject player;
    Animator Tentacle;

    public BoxCollider2D bb2D;

	// Use this for initialization
	void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        Tentacle = GetComponent<Animator>();
        bb2D = this.GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		
	}   
    public void StartAnimation()
    {
        Tentacle.SetBool("Extended", true);
    }
    public void EndAnimation()
    {
        Tentacle.SetBool("Extended", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerBehavior>().health--;
        }
    }
}
