using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour {

    [SerializeField]
    private Image image;

    private Color tempColor;
	// Use this for initialization
	void Start () {
        tempColor = image.color;
        tempColor.a = 1f;
        image.color = tempColor;
    }

    // Update is called once per frame
    void Update () {
        if (tempColor.a >= 0f)
        {
            tempColor.a -= 0.2f * Time.deltaTime;
        }
        
        image.color = tempColor;
    }
}
