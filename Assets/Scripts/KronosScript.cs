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
    public GameObject HadesBigBomb;
    public Transform KronosGunLocation;

    public Transform CreditsLocation;
    public GameObject Credits;
    BossScriptTest bossScript;
    private bool isCreated;
    private Vector2 Direction;

    GameObject credit;

    public bool StartCredit;
    private int KronosMoveState;
    private float KronosMoveTime;

    public GameObject KronosParent;
    public GameObject KronosTentacle;
    private float KronosTongueATKNF, KronosTongueATKFR;

    private bool AttackZeus;

    float initialAttackDelay;

    // Use this for initialization
    void Awake()
    {
        // INITIAL ATTACK DELAY DIRECTLY BELOW
        initialAttackDelay = 3.75f;

        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }

        if (this.bossScript == null)
        {
            this.bossScript = FindObjectOfType<BossScriptTest>();
        }

        HadesATK1FR = 8f;
        HadesATK1NF = Time.time + HadesATK1FR;
        HadesATK1_2FR = 13f;
        HadesATK1_2NF = Time.time + HadesATK1_2FR;
        ZeusATKFR = 2f;
        ZeusATKNF = Time.time + ZeusATKFR;

        KronosTongueATKFR = 3f;
        KronosTongueATKNF = Time.time + KronosTongueATKFR;

        ZeusBulletSpeed = 400;
        ZeusBulletLife = 3f;

        rand = new System.Random();

        KronosMoveState = 1;

        credit = (GameObject)Instantiate(Credits, CreditsLocation.transform.position, CreditsLocation.transform.rotation);

        StartCredit = false;

        AttackZeus = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > HadesATK1NF && bossScript.hp >= 0)
        {
            SpawnTorches();
            LeftTorch = GameObject.FindGameObjectWithTag("LeftTorch");
            RightTorch = GameObject.FindGameObjectWithTag("RightTorch");

        }

        if (Time.time > HadesATK1_2NF && bossScript.hp >= 0)
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

        if (AttackZeus && Time.time > ZeusATKNF && bossScript.hp >= 0)
        {
            Attack2();
        }

        if (Time.time > KronosTongueATKNF && !AttackZeus && bossScript.hp >= 0)
        {
            Attack3();

        }
              

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


        HadesATK1NF = Time.time + HadesATK1FR + 15f;


    }

    void Attack2()
    {
        if (Vertical)
        {
            if (Time.time > ZeusATKNF)
            {
                int temp = rand.Next(135, 225);
                int temp2 = rand.Next(135, 225);


                float angle = (float)temp;

                float projectileDirXposition = KronosGunLocation.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * 5f;
                float projectileDirYposition = KronosGunLocation.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * 5f;

                Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
                Vector3 projectileMoveDirection = (projectileVector - KronosGunLocation.transform.position).normalized * 4f;

                var proj = Instantiate(LightingBallPrefab, KronosGunLocation.transform.position, Quaternion.identity);
                proj.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

                

                float angle2 = (float)temp2;

                float projectileDirXposition2 = KronosGunLocation.transform.position.x + Mathf.Sin((angle2 * Mathf.PI) / 180) * 5f;
                float projectileDirYposition2 = KronosGunLocation.transform.position.y + Mathf.Cos((angle2 * Mathf.PI) / 180) * 5f;

                Vector3 projectileVector2 = new Vector3(projectileDirXposition2, projectileDirYposition2);
                Vector3 projectileMoveDirection2 = (projectileVector2 - KronosGunLocation.transform.position).normalized * 2f;

                var proj2 = Instantiate(HadesBigBomb, KronosGunLocation.transform.position, Quaternion.identity);
                proj2.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(projectileMoveDirection2.x, projectileMoveDirection2.y);


                AttackZeus = false;


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

                //int temp2 = rand.Next(240, 300);

                //float angle2 = (float)temp2;

                //float projectileDirXposition2 = KronosGunLocation.transform.position.x + Mathf.Sin((angle2 * Mathf.PI) / 180) * 5f;
                //float projectileDirYposition2 = KronosGunLocation.transform.position.y + Mathf.Cos((angle2 * Mathf.PI) / 180) * 5f;

                //Vector3 projectileVector2 = new Vector3(projectileDirXposition2, projectileDirYposition2);
                //Vector3 projectileMoveDirection2 = (projectileVector2 - KronosGunLocation.transform.position).normalized * 2f;

                //var proj2 = Instantiate(HadesBigBomb, KronosGunLocation.transform.position, Quaternion.identity);
                //proj2.GetComponent<Rigidbody2D>().velocity =
                //    new Vector2(projectileMoveDirection2.x, projectileMoveDirection2.y);

                AttackZeus = false;

                // We should alter this time for balancing later
                ZeusATKNF = Time.time + ZeusATKFR + 3;
            }

        }



    }

    void Attack3()
    {

        if(!Vertical)
        {
            switch (KronosMoveState)
            {
                case 1:
                    if (KronosParent.gameObject.transform.position.x >= 0)
                    {
                        this.Direction = new Vector2(0, -0.05f); // create new vector based on input combo
                        this.Direction.Normalize(); // normalize so that the direction is consistent

                        //this.gameObject.transform.Translate(Direction * 2f);

                        //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                        KronosParent.gameObject.transform.Translate(Direction);


                    }
                    else
                    {
                        KronosMoveState++;
                    }

                    break;
                case 2:

                    this.Direction = new Vector2(0, 0); // create new vector based on input combo
                    this.Direction.Normalize(); // normalize so that the direction is consistent
                                                //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                    KronosParent.gameObject.transform.Translate(Direction);
                    KronosTentacle.gameObject.GetComponent<KronosTentacleScript>().StartAnimation();
                    KronosMoveTime = Time.time + 2f;

                    KronosMoveState++;

                    break;
                case 3:

                    if (Time.time > KronosMoveTime)
                    {
                        KronosTentacle.gameObject.GetComponent<KronosTentacleScript>().EndAnimation();

                        if (KronosParent.gameObject.transform.position.x <= 5.4)
                        {
                            this.Direction = new Vector2(0, 0.1f); // create new vector based on input combo
                            this.Direction.Normalize(); // normalize so that the direction is consistent
                                                        //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                            KronosParent.gameObject.transform.Translate(Direction);
                        }
                        else
                        {
                            KronosMoveState++;
                        }
                    }


                    break;
                case 4:

                    this.Direction = new Vector2(0, 0); // create new vector based on input combo
                    this.Direction.Normalize(); // normalize so that the direction is consistent
                                                //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                    KronosParent.gameObject.transform.Translate(Direction);

                    KronosMoveState = 1;

                   

                    KronosTongueATKNF = Time.time + KronosTongueATKFR;
                    AttackZeus = true;
                    break;

            }
        }
        else
        {
            switch (KronosMoveState)
            {
                case 1:
                    if (KronosParent.gameObject.transform.position.y >= 0.60)
                    {
                        this.Direction = new Vector2(0, -0.05f); // create new vector based on input combo
                        this.Direction.Normalize(); // normalize so that the direction is consistent

                        //this.gameObject.transform.Translate(Direction * 2f);

                        //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                        KronosParent.gameObject.transform.Translate(Direction);


                    }
                    else
                    {
                        KronosMoveState++;
                    }

                    break;
                case 2:

                    this.Direction = new Vector2(0, 0); // create new vector based on input combo
                    this.Direction.Normalize(); // normalize so that the direction is consistent
                                                //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                    KronosParent.gameObject.transform.Translate(Direction);
                    KronosTentacle.gameObject.GetComponent<KronosTentacleScript>().StartAnimation();
                    KronosMoveTime = Time.time + 1f;

                    KronosMoveState++;

                    break;
                case 3:

                    if (Time.time > KronosMoveTime)
                    {
                        KronosTentacle.gameObject.GetComponent<KronosTentacleScript>().EndAnimation();

                        if (KronosParent.gameObject.transform.position.y <= 3.48)
                        {
                            this.Direction = new Vector2(0, 0.1f); // create new vector based on input combo
                            this.Direction.Normalize(); // normalize so that the direction is consistent
                                                        //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                            KronosParent.gameObject.transform.Translate(Direction);
                        }
                        else
                        {
                            KronosMoveState++;
                        }
                    }


                    break;
                case 4:

                    this.Direction = new Vector2(0, 0); // create new vector based on input combo
                    this.Direction.Normalize(); // normalize so that the direction is consistent
                                                //this.gameObject.GetComponentInParent<GameObject>().transform.Translate(Direction * 2f);

                    KronosParent.gameObject.transform.Translate(Direction);

                    KronosMoveState = 1;

                    KronosTongueATKNF = Time.time + KronosTongueATKFR;
                    AttackZeus = true;
                    break;

            }
        }


       
    }
}
