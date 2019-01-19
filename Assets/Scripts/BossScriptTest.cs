using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScriptTest : MonoBehaviour
{
    private bool onFire;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip deathSound;
    [SerializeField]
    AudioClip damageSound;
    [SerializeField]
    AudioClip weaponSound;
    IEnumerator FireDamage(float damageDuration, int damageCount, float damageAmount)
    {
        onFire = true;
        int currentCount = 0;
        while (currentCount < damageCount)
        {
            audioSource.PlayOneShot(damageSound);
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

    float SoundEffectStart, SoundEffectNext;

    System.Random rand;


    
    [SerializeField]
    private Image bar;
    private float barLength;
    private float startingHP;

    private void Start()
    {
        startingHP = hp;

    }

    private void HealthBar()
    {
        barLength = (hp / startingHP);
        bar.fillAmount = barLength;
    }

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

        rand = new System.Random();

        //Random time to start
        SoundEffectStart = rand.Next();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (hp <= 0f)
        {
            //if (this.gameObject.tag == "Zeus")
            //{
            //}
            //if (this.gameObject.tag == "Poseidon")
            //{
            //}s
            //if (this.gameObject.tag == "Hades")
            //{
            
            //}

            if(Time.time > SoundEffectNext)
            {

                //After Sound effect is played, generate new random. 
                SoundEffectStart = rand.Next();
                SoundEffectNext = Time.time + SoundEffectStart;
            }


            audioSource.PlayOneShot(deathSound);
          
            var expldi = (GameObject)Instantiate(explosionEffect, this.transform.position, this.transform.rotation);

            Destroy(this.gameObject);
        }

        HealthBar();

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
