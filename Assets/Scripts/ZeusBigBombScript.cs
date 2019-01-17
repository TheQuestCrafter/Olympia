using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeusBigBombScript : MonoBehaviour {


	// Use this for initialization
	void Awake ()
    {
        
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
       
            Destroy(this.gameObject, 3f);
    
    }
}
