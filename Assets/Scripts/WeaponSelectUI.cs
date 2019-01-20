using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WeaponSelectUI : MonoBehaviour
{
    GameObject player;
    [SerializeField]
    Image basicShot;
    [SerializeField]
    Image zeusShot;
    [SerializeField]
    Image poseidonShot;
    [SerializeField]
    Image hadesShot;
    [SerializeField]
    Image R;
    // Use this for initialization
    void Start()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player == null)
        {
            basicShot.gameObject.SetActive(false);
            zeusShot.gameObject.SetActive(false);
            poseidonShot.gameObject.SetActive(false);
            hadesShot.gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehavior>().weaponSelected == 0)
        {
            basicShot.gameObject.SetActive(true);
            zeusShot.gameObject.SetActive(false);
            poseidonShot.gameObject.SetActive(false);
            hadesShot.gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehavior>().weaponSelected == 1)
        {
            basicShot.gameObject.SetActive(false);
            zeusShot.gameObject.SetActive(true);
            poseidonShot.gameObject.SetActive(false);
            hadesShot.gameObject.SetActive(false);
        }
        else if (player.GetComponent<PlayerBehavior>().weaponSelected == 2)
        {
            basicShot.gameObject.SetActive(false);
            zeusShot.gameObject.SetActive(false);
            poseidonShot.gameObject.SetActive(true);
            hadesShot.gameObject.SetActive(false);
        }
        else if(player.GetComponent<PlayerBehavior>().weaponSelected == 3)
        {
            basicShot.gameObject.SetActive(false);
            zeusShot.gameObject.SetActive(false);
            poseidonShot.gameObject.SetActive(false);
            hadesShot.gameObject.SetActive(true);
        }
        if (SceneManager.GetActiveScene().buildIndex >= 3)
        {
            R.gameObject.SetActive(true);
        }
    }
 
}
