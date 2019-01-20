using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// will be used to keep state of tentacles so that they can act appropriately
public enum TentacleState { Stop, Attack, Reset} 

public class PoseidonScript : MonoBehaviour
{
    GameObject player;
    public GameObject BubblePrefab;
    public GameObject WaterBeam;
    public GameObject RightTentacle, LeftTentacle;
    Rigidbody2D RTentacleRB2D, LTentacleRB2D;
    public TentacleState RTentacleState, LTentacleState; // The current state of the tentacles
    public float RTentacleNF, LTentacleNF; // Tentacle next fires for time.
    bool RTentacleReady, LTentacleReady;
    public float RTentacleFR, LTentacleFR; // Tentacle fire rate, how fast it attacks with the tentacles

    PolygonCollider2D pc2D;
    public Transform poseidonGun;
    float Speed;
    float NextFireBubble, FireRateBubble, fireBubbleTimeBeforeLaser;
    float NextFireLaser, FireRateLaser, FireIntervalLaser; // FireRate is time between individual bullets. FireInterval is time between the waves of attack
    bool PivotRight;
    float radius;
    Vector3 Direction;
    System.Random rand;
    float currentAngle, startAngle, endAngle;

    bool ShootingBubble;
    float initialAttackDelay; // To allow the scene transition to fade in. One delay for all attack Types

    // Use this for initialization
    void Awake()
    {
        // INITIAL ATTACK DELAY DIRECTLY BELOW
        initialAttackDelay = 3.5f;

        RTentacleRB2D = RightTentacle.GetComponent<Rigidbody2D>();
        LTentacleRB2D = LeftTentacle.GetComponent<Rigidbody2D>();
        if (this.player == null)
        {
            PlayerBehavior temp = FindObjectOfType<PlayerBehavior>();
            this.player = temp.gameObject;
        }
        LTentacleState = RTentacleState = TentacleState.Stop;
        RTentacleFR = 7.1f;
        LTentacleFR = 11.3f;
        RTentacleNF = Time.time + RTentacleFR + initialAttackDelay;
        LTentacleNF = Time.time + LTentacleFR + initialAttackDelay;
        RTentacleReady = LTentacleReady = true;

        FireIntervalLaser = 12f;
        FireRateLaser = 0.15f;
        NextFireLaser = Time.time + FireRateLaser + initialAttackDelay;
        rand = new System.Random();
        Speed = 3f;
        startAngle = currentAngle = 260;
        endAngle = 100; 
        Direction = new Vector3(0, 0, 1);
        pc2D = GetComponent<PolygonCollider2D>();

        radius = 15f;

        fireBubbleTimeBeforeLaser = 6;
        FireRateBubble = 2f;
        NextFireBubble = NextFireLaser - fireBubbleTimeBeforeLaser;

        ShootingBubble = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Time.time > NextFireBubble)
            Attack2();

        if (Time.time > 10)
            Attack3();

        if (RTentacleReady && Time.time > RTentacleNF)
        {
            RTentacleState = TentacleState.Attack;
        }
        else if (!RTentacleReady && Time.time > RTentacleNF && RTentacleState == TentacleState.Stop)
        {
            RTentacleState = TentacleState.Reset;
            ShootingBubble = true;
        }

        if (LTentacleReady && Time.time > LTentacleNF)
        {
            LTentacleState = TentacleState.Attack;
        }
        else if (!LTentacleReady && Time.time > LTentacleNF && LTentacleState == TentacleState.Stop)
        {
            LTentacleState = TentacleState.Reset;
            ShootingBubble = true;
        }
        
            determineTentacleStateAction(RTentacleState, true);
            determineTentacleStateAction(LTentacleState, false);

    }

    void determineTentacleStateAction(TentacleState tentacleState, bool right) // right is the right tentacle. if false, it is the left tentacle
    {
        if (RightTentacle.transform.rotation.z <= -0.42f && RTentacleState == TentacleState.Attack)
        {
            RTentacleState = TentacleState.Stop;
            RTentacleNF = Time.time + (RTentacleFR / 2.5f);
            RTentacleReady = false;
        }

        if (LeftTentacle.transform.rotation.z >= 0.42f && LTentacleState == TentacleState.Attack)
        {
            LTentacleState = TentacleState.Stop;
            LTentacleNF = Time.time + (LTentacleFR / 2.5f);
            LTentacleReady = false;
        }

        switch (tentacleState)
        {
            case TentacleState.Stop:
                {
                    // Tentacle doesn't move. Can be at either end point
                    if(right && !RTentacleReady)
                    {
                        //RightTentacle.transform.Rotate(new Vector3(0, 0, -55.5f));
                    }
                   
                    break;
                }
            case TentacleState.Attack:
                {
                    if(right)
                    {
                        RightTentacle.transform.Rotate(Vector3.forward * -Speed * 0.2f);
                    }
                    else
                    {
                        LeftTentacle.transform.Rotate(Vector3.forward * Speed * 0.2f);
                    }
                    break;
                }
            case TentacleState.Reset:
                {
                    if (right)
                    {
                        RightTentacle.transform.Rotate(Vector3.forward * Speed * 0.1f);
                        if (RightTentacle.transform.rotation.z >= 0.0966f)
                        {
                            RTentacleState = TentacleState.Stop;
                            RTentacleReady = true;
                            RTentacleNF = Time.time + RTentacleFR;
                        }
                    }
                    else
                    {
                        LeftTentacle.transform.Rotate(Vector3.forward * -Speed * 0.1f);
                        if (LeftTentacle.transform.rotation.z <= -0.0966f)
                        {
                            LTentacleState = TentacleState.Stop;
                            LTentacleReady = true;
                            LTentacleNF = Time.time + LTentacleFR;
                        }
                    }
                    break;
                }
        }
    }


    void Attack2()
    {
        if (Time.time > NextFireBubble)
        {
            int temp = rand.Next(90, 270);

            float angle = (float)temp;

            float projectileDirXposition = poseidonGun.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = poseidonGun.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector3(projectileDirXposition, projectileDirYposition);
            Vector3 projectileMoveDirection = (projectileVector - poseidonGun.position).normalized * Speed;

            var proj = Instantiate(BubblePrefab, poseidonGun.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            // We should alter this time for balancing later
            NextFireBubble = Time.time + FireIntervalLaser; // FireRateBubble + 20;
            //NextFireBubble = Time.time + FireIntervalLaser - fireBubbleTimeBeforeLaser;



            Destroy(proj, 7);
        }
        

    }

    void Attack3()
    {
        if (Time.time > FireIntervalLaser && Time.time > NextFireLaser)
        {
            float projectileDirXposition = poseidonGun.position.x + Mathf.Sin((currentAngle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = poseidonGun.position.y + Mathf.Cos((currentAngle * Mathf.PI) / 180) * radius;

            Vector3 projectileVector = new Vector2(projectileDirXposition, projectileDirYposition);
            Vector3 projectileMoveDirection = (projectileVector - poseidonGun.position).normalized * Speed;

            var proj = Instantiate(WaterBeam, poseidonGun.position, Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().velocity =
                new Vector2(projectileMoveDirection.x, projectileMoveDirection.y);

            NextFireLaser = Time.time + FireRateLaser;
            currentAngle -= 3f;

            if(currentAngle <= endAngle)
            {
                NextFireLaser = Time.time + FireIntervalLaser;
                currentAngle = startAngle;
            }
            Destroy(proj, 3.5f);
        }
        
    }

}
