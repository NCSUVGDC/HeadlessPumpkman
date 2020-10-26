using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerBody;

    public GameObject meleeWeapon;

    public int health = 100;

    public float frameAcceleration = 0.5f;
    public float topSpeed = 10;
    public float speed = 10;
    public float meleeReach = 2;

    public enum AttackState
    {
        Ready, //This attack is not currently in use
        Windup, //This attack has started, but no hitbox is active yet (potentially no other attacks can interrupt it, requires playtesting)
        Attacking, //This attack is currently in use
        Cooldown, //This attack has ended and cannot be used again until its cooldown timer expires
    }

    private AttackState meleeState = AttackState.Ready;
    public float meleeWindupTime = 1;
    public float meleeAttackingTime = 1;
    public float meleeCooldownTime = 1;
    private float meleeTimer = 0;
    private AttackState rangedState = AttackState.Ready;
    public float rangedWindupTime = 11;
    public float rangedAttackingTime = 1;
    public float rangedCooldownTime = 1;
    private float rangedTimer = 0;

    public float speedBoostTime = 1;
    public float knockBackTime = 1;
    private float pushTimer = 0;

    private bool isBeingPushed = false;
    private float pushForce = 0;
    private bool isFacingRight = true;

    public float appliedFriction = 0.6f;

    private void Start()
    {
        //Faces the player towards the right when they spawn in, because isFacingRight defaults to true
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
    }

    //Updates at a fixed interval, regardless of framerate
    private void FixedUpdate()
    {
        //PLAYER MOVEMENT


        //This syntax reads: if isBeingPushed, then pushForce... else, Input.GetAxis() * speed
        //float movement = (isBeingPushed ? pushForce : Input.GetAxis("Horizontal") * speed * appliedFriction);
        //playerBody.velocity = new Vector3(playerBody.velocity.x + movement - playerBody.velocity.x * appliedFriction, playerBody.velocity.y, playerBody.velocity.z);

        if (isBeingPushed)
        {
            playerBody.velocity = new Vector3(pushForce, playerBody.velocity.y, playerBody.velocity.z);
        }
        else
        {
            MovePlayerFromInput(Input.GetAxisRaw("Horizontal"));
        }

        if (meleeState == AttackState.Ready || meleeState == AttackState.Cooldown) //Prevents the player from turning around during a melee attack
        {
            if (playerBody.velocity.x > 0) //If moving right...
            {
                //Makes the player character face right, then repositions the sword hitbox accordingly
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
                meleeWeapon.transform.position = new Vector3(gameObject.transform.position.x + meleeReach, gameObject.transform.position.y, 0);
                isFacingRight = true;
            }
            if (playerBody.velocity.x < 0) //If moving left...
            {
                //Makes the player character face left, then repositions the sword hitbox accordingly
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.left);
                meleeWeapon.transform.position = new Vector3(gameObject.transform.position.x - meleeReach, gameObject.transform.position.y, 0);
                isFacingRight = false;
            }
        }
        
        


        //CONTROL INPUT
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.O)) && meleeState == AttackState.Ready)
        {
            meleeState = AttackState.Windup;
            meleeTimer = meleeWindupTime;
            gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Yellow", typeof(Material)) as Material;
            //Swing animation would begin here
        }
        if ((Input.GetKeyUp(KeyCode.Z)) || Input.GetKeyUp(KeyCode.O))
        {

        }

        if ((Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.P)) && rangedState == AttackState.Ready)
        {
            rangedState = AttackState.Windup;
            rangedTimer = rangedWindupTime;
            gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Yellow", typeof(Material)) as Material;
        }


        if ((Input.GetKey(KeyCode.C)))
        {
            Debug.Log("Enemy Spawned");
            Instantiate(Resources.Load("Prefabs/DummyEnemy"));
        }


        //ATTACK TIMERS
        if (meleeState != AttackState.Ready)
        {
            MeleeManager(meleeState);
        }
        if (rangedState != AttackState.Ready)
        {
            RangedManager(rangedState);
        }
        if (isBeingPushed)
        {
            PushManager();
        }
    }

    void MovePlayerFromInput(float input)
    {
        switch (input)
        {
            case 1:
                {
                    AcceleratePlayer();
                    if (playerBody.velocity.x > topSpeed + frameAcceleration)
                    {
                        DeceleratePlayer();
                    }
                    else if (playerBody.velocity.x > topSpeed)
                    {
                        playerBody.velocity = new Vector3(topSpeed, playerBody.velocity.y, playerBody.velocity.z);
                    }
                }
                break;
            case 0:
                {
                    if (playerBody.velocity.x > 0)
                    {
                        DeceleratePlayer();
                        if (playerBody.velocity.x < 0)
                        {
                            playerBody.velocity = new Vector3(0, playerBody.velocity.y, playerBody.velocity.z);
                        }
                    }
                    else if (playerBody.velocity.x < 0)
                    {
                        AcceleratePlayer();
                        if (playerBody.velocity.x > 0)
                        {
                            playerBody.velocity = new Vector3(0, playerBody.velocity.y, playerBody.velocity.z);
                        }
                    }
                }
                break;
            case -1:
                {
                    DeceleratePlayer();
                    if (playerBody.velocity.x < -topSpeed - frameAcceleration)
                    {
                        AcceleratePlayer();
                    }
                    else if (playerBody.velocity.x < -topSpeed)
                    {
                        playerBody.velocity = new Vector3(-topSpeed, playerBody.velocity.y, playerBody.velocity.z);
                    }
                }
                break;
            default:
                {
                    Debug.Log("Input.GetAxisRaw returned a non-integer.");
                }
                break;
        }
    }

    void AcceleratePlayer()
    {
        playerBody.velocity = new Vector3(playerBody.velocity.x + frameAcceleration,
                                                        playerBody.velocity.y, playerBody.velocity.z);
    }

    void DeceleratePlayer()
    {
        playerBody.velocity = new Vector3(playerBody.velocity.x - frameAcceleration,
                                                        playerBody.velocity.y, playerBody.velocity.z);
    }

    //Responsible for the Sword Swing's states, timers, and activation.
    void MeleeManager(AttackState currentState)
    {
        meleeTimer -= Time.deltaTime;
        if (meleeTimer <= 0)
        {
            switch (currentState)
            {
                case (AttackState.Windup): //If the Windup timer has expired...
                    {
                        meleeWeapon.GetComponent<BoxCollider>().enabled = true;
                        meleeWeapon.GetComponent<MeshRenderer>().enabled = true;
                        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Orange", typeof(Material)) as Material;
                        meleeState = AttackState.Attacking;
                        meleeTimer = meleeAttackingTime;
                    }
                    break;
                case (AttackState.Attacking): //If the Attacking timer has expired...
                    {
                        meleeWeapon.GetComponent<BoxCollider>().enabled = false;
                        meleeWeapon.GetComponent<MeshRenderer>().enabled = false;
                        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Red", typeof(Material)) as Material;
                        meleeState = AttackState.Cooldown;
                        meleeTimer = meleeCooldownTime;
                    }
                    break;
                case (AttackState.Cooldown): //If the Cooldown timer has expired...
                    {
                        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Green", typeof(Material)) as Material;
                        meleeState = AttackState.Ready;
                    }
                    break;
            }
        }
    }

    //Responsible for the Pumpkin Bomb's states, timers, and activation.
    void RangedManager(AttackState currentState)
    {

        rangedTimer -= Time.deltaTime;

        if (rangedTimer <= 0)
        {
            switch (currentState)
            {
                case (AttackState.Windup): //If the Windup timer has expired...
                    {
                        Instantiate(Resources.Load("Prefabs/PlayerBomb"));
                        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Orange", typeof(Material)) as Material;
                        rangedState = AttackState.Attacking;
                        rangedTimer = rangedAttackingTime;
                    }
                    break;
                case (AttackState.Attacking): //If the Attacking timer has expired...
                    {
                        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Red", typeof(Material)) as Material;
                        rangedState = AttackState.Cooldown;
                        rangedTimer = rangedCooldownTime;
                    }
                    break;
                case (AttackState.Cooldown): //If the Cooldown timer has expired...
                    {
                        gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Green", typeof(Material)) as Material;
                        rangedState = AttackState.Ready;
                    }
                    break;
            }
        }
    }

    void PushManager()
    {
        pushTimer -= Time.deltaTime;

        if (pushTimer <= 0) //If the pushing timer has expired...
        {
            isBeingPushed = false;
            //inputOverride = true;
        }
    }

    public void PushPlayer(bool isSpeedBoost, float force)
    {
        isBeingPushed = true;
        if (isFacingRight)
        {
            if (isSpeedBoost)
            {
                pushTimer = speedBoostTime;
                pushForce = force; //Pushes right, TOWARDS the enemy
            }
            else //KnockBack
            {
                pushTimer = knockBackTime;
                pushForce = -force; //Pushes left, AWAY from the enemy
            }
        }
        else //Facing left
        {
            if (isSpeedBoost)
            {
                pushTimer = speedBoostTime;
                pushForce = -force; //Pushes left, TOWARDS the enemy
            }
            else //KnockBack
            {
                pushTimer = knockBackTime;
                pushForce = force; //Pushes right, AWAY from the enemy
            }
        }
    }
}
