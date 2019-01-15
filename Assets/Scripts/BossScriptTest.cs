using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScriptTest : MonoBehaviour
{
    private bool onFire;

    IEnumerator FireDamage(float damageDuration, int damageCount, int damageAmount)
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
    

    CircleCollider2D cc2D;
    GameObject player;
    public int hp;

    // Use this for initialization
    void Awake ()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        cc2D = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (hp < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            hp -= player.GetComponent<PlayerBehavior>().weaponDamage;
            Destroy(collision.gameObject);

        }
        if (collision.tag == "PlayerFireBullet")
        {
            hp -= player.GetComponent<PlayerBehavior>().weaponDamage;
            Destroy(collision.gameObject);
            StartCoroutine(FireDamage(1f, 5, 1));

        }
        if (collision.tag == "PlayerLightingBullet")
        {
            hp -= player.GetComponent<PlayerBehavior>().weaponDamage;
            Destroy(collision.gameObject);
            StartCoroutine(FireDamage(1f, 5, 1));

        }
    }

}
