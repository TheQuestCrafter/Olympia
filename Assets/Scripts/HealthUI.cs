using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    Image heart1;
    [SerializeField]
    Image heart2;
    [SerializeField]
    Image heart3;
	// Use this for initialization
	void Start () {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (player == null)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehavior>().health == 1)
        {
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehavior>().health == 2)
        {
            heart3.gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehavior>().health == 3)
        {
            heart1.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart3.gameObject.SetActive(true);
        }
	}
}
