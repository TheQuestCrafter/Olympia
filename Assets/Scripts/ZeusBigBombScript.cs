using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusBigBombScript : MonoBehaviour {

    GameObject player;

	// Use this for initialization
	void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
            Destroy(this.gameObject, 3f);
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.GetComponent<PlayerBehavior>().health--;
    }
}
