using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    public playerMovement movementManager;
    public Rigidbody playerBody;
    public GameObject meleeWeapon;
    public Text coinCounter;
    
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

    public float turnAroundDeadzone = 0.01f;
    private float coinCount = 0;

    public Animator animator;

    private void Start()
    {
        //coinCounter = GameObject.Find("Coin Count").GetComponent<Text>();
    }

    //Updates at a fixed interval, regardless of framerate
    private void FixedUpdate()
    {
        //PLAYER TURNING
        if (meleeState == AttackState.Ready || meleeState == AttackState.Cooldown) //Prevents the player from turning around during a melee attack
        {
            if (playerBody.velocity.x > 0 + turnAroundDeadzone || movementManager.getIsFacingRight()) //If substantially moving right, or you're already facing that way...
            {
                //Makes the player character face right, then repositions the sword hitbox accordingly
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
                meleeWeapon.transform.position = new Vector3(gameObject.transform.position.x + meleeReach, gameObject.transform.position.y, 0);
                movementManager.setIsFacingRight(true);
            }
            if (playerBody.velocity.x < 0 - turnAroundDeadzone || !movementManager.getIsFacingRight()) //If substantially moving left, or you're already facing that way...
            {
                //Makes the player character face left, then repositions the sword hitbox accordingly
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.left);
                meleeWeapon.transform.position = new Vector3(gameObject.transform.position.x - meleeReach, gameObject.transform.position.y, 0);
                movementManager.setIsFacingRight(false);
            }
        }
        
        //CONTROL INPUT
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.O)) || Input.GetKey(KeyCode.Mouse0) && meleeState == AttackState.Ready)
        {
            meleeState = AttackState.Windup;
            meleeTimer = meleeWindupTime;
            //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Yellow", typeof(Material)) as Material;
            //Swing animation would begin here
        }
        if ((Input.GetKeyUp(KeyCode.Z)) || Input.GetKeyUp(KeyCode.O))
        {

        }

        if ((Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.P)) | Input.GetKey(KeyCode.Mouse1) && rangedState == AttackState.Ready)
        {
            rangedState = AttackState.Windup;
            rangedTimer = rangedWindupTime;
            //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Yellow", typeof(Material)) as Material;
        }

        //ATTACK TIMERS
        if (meleeState != AttackState.Ready)
        {
            MeleeManager(meleeState);

            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
        if (rangedState != AttackState.Ready)
        {
            RangedManager(rangedState);
        }
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
                        //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Orange", typeof(Material)) as Material;
                        meleeState = AttackState.Attacking;
                        meleeTimer = meleeAttackingTime;
                    }
                    break;
                case (AttackState.Attacking): //If the Attacking timer has expired...
                    {
                        meleeWeapon.GetComponent<BoxCollider>().enabled = false;
                        meleeWeapon.GetComponent<MeshRenderer>().enabled = false;
                        //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Red", typeof(Material)) as Material;
                        meleeState = AttackState.Cooldown;
                        meleeTimer = meleeCooldownTime;
                    }
                    break;
                case (AttackState.Cooldown): //If the Cooldown timer has expired...
                    {
                        //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Green", typeof(Material)) as Material;
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
                        //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Orange", typeof(Material)) as Material;
                        rangedState = AttackState.Attacking;
                        rangedTimer = rangedAttackingTime;
                    }
                    break;
                case (AttackState.Attacking): //If the Attacking timer has expired...
                    {
                        //gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Red", typeof(Material)) as Material;
                        rangedState = AttackState.Cooldown;
                        rangedTimer = rangedCooldownTime;
                    }
                    break;
                case (AttackState.Cooldown): //If the Cooldown timer has expired...
                    {
                       // gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Green", typeof(Material)) as Material;
                        rangedState = AttackState.Ready;
                    }
                    break;
            }
        }
    }

    //A setter method for the player's coin count. Can increment and decrement the counter, for when a coin is collected or spent, respectively.
    public void ChangeCoinCount(float input)
    {
        coinCount += input;
        coinCounter.text = coinCount.ToString();
    }

    public AttackState GetMeleeState()
    {
        return meleeState;
    }
}
