using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseidonLightingBallScript : MonoBehaviour {

    CircleCollider2D cc2D;
	// Use this for initialization
	void Awake ()
    {
        cc2D = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
        }
          
    }
   

}
