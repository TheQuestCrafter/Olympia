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
    AudioClip idleSound;
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

    private bool isCreated;
    
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
        SoundEffectStart = rand.Next(4,12);
        SoundEffectNext = Time.time + SoundEffectStart;

        isCreated = false;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Time.time > SoundEffectNext && idleSound != null)
        {
            audioSource.PlayOneShot(idleSound);
            //After Sound effect is played, generate new random. 
            SoundEffectStart = rand.Next(4,12);
            SoundEffectNext = Time.time + SoundEffectStart;
        }
        if (hp <= 0f)
        {
            //if (this.gameObject.tag == "Zeus")
            //{
            //}
            //if (this.gameObject.tag == "Poseidon")
            //{
            //}
            //if (this.gameObject.tag == "Hades")
            //{
            //}

            audioSource.PlayOneShot(deathSound);


            if (!isCreated)
            {
                var expldi = (GameObject)Instantiate(explosionEffect, this.transform.position, this.transform.rotation);

                Destroy(expldi, 2f);
                isCreated = true;
            }
          

            Destroy(this.gameObject,deathSound.length);
        }

        HealthBar();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(damageSound);
        }
        if (collision.tag == "PlayerFireBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);
            StartCoroutine(FireDamage(1f, 5, 1f));
            audioSource.PlayOneShot(damageSound);
        }
        if (collision.tag == "PlayerWaterBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(damageSound);
        }
        if (collision.tag == "PlayerLightingBullet")
        {
            hp -= player.GetComponent<BulletManager>().weaponDamage;
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(damageSound);
        }
    }

}
