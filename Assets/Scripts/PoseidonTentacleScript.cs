﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonTentacleScript : MonoBehaviour
{
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
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerBehavior>().health--;
        }
    }
}
