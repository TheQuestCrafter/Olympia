using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {
    [SerializeField]
    private float speed;
    private Vector3 startPOS;

	// Use this for initialization
	void Start () {
        startPOS = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate((new Vector3(-1, 0, 0)) * speed * Time.deltaTime);

        if (transform.position.x < -(3.2 * 7)) //multiple child's x offest by parent's scale
        {
            transform.position = startPOS;
        }
    }
}
