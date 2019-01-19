using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScript : MonoBehaviour {


    bool StartCredit;

    private int creditState;
    private float creditTimeExit;
    private Vector2 Direction;

    public GameObject Kronos;
    // Use this for initialization
    void Awake ()

    {
        //bossScript = GetComponent<BossScriptTest>();
        StartCredit = false;
        creditState = 1;

        if(Kronos == null)
        {
           Kronos = FindObjectOfType<KronosScript>().gameObject;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveCredit();

        if (Kronos == null)
            StartCredit = true;

    }



    void MoveCredit()
    {

        if (StartCredit)
        {

            switch (creditState)
            {
                case 1:
                    if (this.gameObject.transform.position.x >= -7.5)
                    {
                        this.Direction = new Vector2(-1, 0); // create new vector based on input combo
                        this.Direction.Normalize(); // normalize so that the direction is consistent
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(Direction * 2f);

                    }
                    else
                    {
                        creditState++;
                    }

                    break;
                case 2:

                    this.Direction = new Vector2(0, 0); // create new vector based on input combo
                    this.Direction.Normalize(); // normalize so that the direction is consistent
                    this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    creditTimeExit = Time.time + 3f;
                    creditState++;

                    break;
                case 3:

                    if (Time.time > creditTimeExit)
                    {
                        this.Direction = new Vector2(1, 0); // create new vector based on input combo
                        this.Direction.Normalize(); // normalize so that the direction is consistent
                        this.gameObject.GetComponent<Rigidbody2D>().AddForce(Direction * 2f);
                        Destroy(this.gameObject, 8f);
                    }

                    break;

            }


        }

    }
}
