using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody playerBody;

    public GameObject meleeWeapon;
    public GameObject rangedWeapon;

    public int health = 100;

    public float speed = 5;
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
    private float rangeTimer = 0;

    //Start is called before the first frame update
    void Start()
    {
        
    }

    //Updates at a fixed interval, regardless of framerate
    void FixedUpdate()
    {
        //PLAYER MOVEMENT
        playerBody.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, playerBody.velocity.y, playerBody.velocity.z);

        if (playerBody.velocity.x > 0) //If moving right...
        {
            //Makes the player character face right, then repositions the sword hitbox accordingly
            gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
            meleeWeapon.transform.position = new Vector3(gameObject.transform.position.x + meleeReach, gameObject.transform.position.y, 0);
        }
        if (playerBody.velocity.x < 0) //If moving left...
        {
            //Makes the player character face left, then repositions the sword hitbox accordingly
            gameObject.transform.rotation = Quaternion.LookRotation(Vector3.left);
            meleeWeapon.transform.position = new Vector3(gameObject.transform.position.x - meleeReach, gameObject.transform.position.y, 0);
        }
        


        //CONTROL INPUT
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.O)) && meleeState == AttackState.Ready)
        {
            meleeState = AttackState.Windup;
            meleeTimer = meleeWindupTime;
            gameObject.GetComponent<Renderer>().material = Resources.Load("Materials/Yellow", typeof(Material)) as Material;
            //Swing animation would begin here
        }
        else if (Input.GetKey(KeyCode.X))
        {
            if (rangedState == AttackState.Ready)
            {
                RangedAttack();
            }
        }



        //ATTACK TIMERS
        if (meleeState != AttackState.Ready)
        {
            MeleeManager(meleeState);
        }
    }

    void MeleeAttack()
    {
        
    }

    //Responsible for the Melee Attack's states and timers.
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

    void RangedAttack()
    {
        //bomb implementation
    }
}
