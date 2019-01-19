using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KronosScript : MonoBehaviour
{

    float HadesATK1FR, HadesATK1NF;
    float HadesATK1_2FR, HadesATK1_2NF;
    float ZeusATKNF, ZeusATKFR;
    public float ZeusBulletSpeed;
    public float ZeusBulletLife;

    public bool Vertical;

    System.Random rand;

    GameObject player;
    public GameObject lavaWavePrefab;
    public Transform torch1, torch2;
    public GameObject LeftTorchPrefab, RightTorchPrefab;
    GameObject LeftTorch, RightTorch;
    public GameObject LightingBallPrefab;
    public Transform KronosGunLocation;

    public Transform CreditsLocation;
    public GameObject Credits;
    BossScriptTest bossScript;
    private bool isCreated;
    private Vector2 Direction;

    GameObject credit;

    public bool StartCredit;

    // Use this for initialization
    void Awake()
    {
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        HadesATK1FR = 1f;
        HadesATK1NF = Time.time + HadesATK1FR;
        HadesATK1_2FR = 10f;
        HadesATK1_2NF = Time.time + HadesATK1_2FR;
        ZeusATKFR = 2f;

        ZeusBulletSpeed = 400;
        ZeusBulletLife = 3f;

        rand = new System.Random();


        credit = (GameObject)Instantiate(Credits, CreditsLocation.transform.position, CreditsLocation.transform.rotation);

        StartCredit = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > HadesATK1NF)
        {
            SpawnTorches();
            LeftTorch = GameObject.FindGameObjectWithTag("LeftTorch");
            RightTorch = GameObject.FindGameObjectWithTag("RightTorch");

        }

        if (Time.time > HadesATK1_2NF)
        {

            if (LeftTorch != null && RightTorch != null)
            {
                Attack1(true, true);
                Destroy(LeftTorch);
                Destroy(RightTorch);
            }
            else if (LeftTorch == null && RightTorch != null)
            {
                Attack1(false, true);
                Destroy(RightTorch);
            }
            else if (LeftTorch != null && RightTorch == null)
            {
                Attack1(true, false);
                Destroy(LeftTorch);
            }
            else if (LeftTorch == null && RightTorch == null)
            {
                Attack1(false, false);
            }

        }
        Attack2();

    }


    void Attack1(bool torch1On, bool torch2On)
    {
        if (torch1On || torch2On)
        {
            var lavawave = (GameObject)Instantiate(
            lavaWavePrefab,
            this.transform.position,
            this.transform.rotation);

            HadesATK1_2NF = Time.time + HadesATK1_2FR;
        }
        else
        {
            HadesATK1_2NF = Time.time + HadesATK1_2FR;
        }
    }

    void SpawnTorches()
    {

        var torch_1 = (GameObject)Instantiate(LeftTorchPrefab, torch1.transform.position, torch1.transform.rotation);
        var torch_2 = (GameObject)Instantiate(RightTorchPrefab, torch2.transform.position, torch2.transform.rotation);


        HadesATK1NF = Time.time + HadesATK1FR + 20f;


    }

    void Attack2()
    {
        if (Vertical)
        {
            if (Time.time > ZeusATKNF)
            {
                int temp = rand.Next(120, 250);

                float angle = (float)temp;

                float projectileDirXposition = KronosGunLocation.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5f;
                float projectileDirYposition = KronosGunLocation.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5f;

                Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
                Vector3 projectileMoveDirection = (projectileVector - KronosGunLocation.transform.position).normalized * 4f;

                var proj = Instantiate(LightingBallPrefab, KronosGunLocation.transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                // We should alter this time for balancing later
                ZeusATKNF = Time.time + ZeusATKFR + 3;
            }
        }
        else
        {
            if (Time.time > ZeusATKNF)
            {
                int temp = rand.Next(240, 300);

                float angle = (float)temp;

                float projectileDirXposition = KronosGunLocation.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5f;
                float projectileDirYposition = KronosGunLocation.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5f;

                Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
                Vector3 projectileMoveDirection = (projectileVector - KronosGunLocation.transform.position).normalized * 7f;

                var proj = Instantiate(LightingBallPrefab, KronosGunLocation.transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                // We should alter this time for balancing later
                ZeusATKNF = Time.time + ZeusATKFR + 3;
            }

        }



    }

    void Attack3()
    {

    }
}
