﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScriptTest : MonoBehaviour
{
    private bool onFire;

    IEnumerator FireDamage(float damageDuration, int damageCount, float damageAmount)
    {
        onFire = true;
        int currentCount = 0;
        while (currentCount < damageCount)
        {
            hp -= damageAmount;
            yield return new WaitForSeconds(damageDuration);
            currentCount++;
        }
        onFire = false;
    }
    

   // CircleCollider2D cc2D;
    PolygonCollider2D pc2D;
    GameObject player;
    GameObject bullet;
    public float hp;
    public GameObject explosionEffect;

    // Use this for initialization
    void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        //cc2D = GetComponent<CircleCollider2D>();
        pc2D = GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (hp < 0f)
        {

            if (this.gameObject.tag == "Zeus")
            {
                player.GetComponent<PlayerBehavior>().Weapons[1] = true;
            }
            if (this.gameObject.tag == "Poseidon")
            {
                player.GetComponent<PlayerBehavior>().Weapons[2] = true;
            }
            if (this.gameObject.tag == "Hades")
            {
                player.GetComponent<PlayerBehavior>().Weapons[3] = true;
            }


            Destroy(this.gameObject);
            var expldi = (GameObject)Instantiate(explosionEffect, this.transform.position, this.transform.rotation);


           
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);

        }
        if (collision.tag == "PlayerFireBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);
            StartCoroutine(FireDamage(1f, 5, 1f));

        }
        if (collision.tag == "PlayerWaterBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);

        }
        if (collision.tag == "PlayerLightingBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);

        }
    }

}
