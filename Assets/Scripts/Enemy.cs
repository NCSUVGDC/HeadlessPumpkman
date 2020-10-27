﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10;
    public Rigidbody player;
    public Rigidbody self;
    public float detectionRange = 15;
    public float speed = 5;
    public float jumpHeight = 3;
    public bool goRight = true;

    //Decrements this enemy's health by the specified integer value
    //Returns 'true' if this call results in this enemy's death, for melee speed boost purposes
    public bool TakeDamage(int damageReceived)
    {
        if (gameObject != null)
        {
            health -= damageReceived;
            if (health <= 0)
            {
                Destroy(gameObject);
                return true;
            }
            else
            {
                return false;
            }
        }

        Debug.Log("An enemy that does not exist just tried to TakeDamage().");
        return false;
    }

    // Update is called once per frame

    void Start()
    {
        InvokeRepeating("switchDirection", 5, 5);
    }
    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 playerLoc = player.position - gameObject.transform.position;
            playerLoc.y = 0;

            if (playerLoc.magnitude <= detectionRange)
            {
                //Debug.Log("Chasing. Distance between is: " + playerLoc.magnitude);
                Quaternion direction;
                direction = Quaternion.LookRotation(playerLoc);
                gameObject.transform.rotation = direction;

                Vector3 vel = gameObject.transform.forward * speed;
                vel.y = self.velocity.y;
                self.velocity = vel;
            }
            else
            {
                //normal patrol behavior
                Vector3 vel = patrol();
                self.velocity = vel;
            }
        }
    }

    private Vector3 patrol()
    {
        Debug.Log("Goright's value is: " + goRight);
        if (goRight)
        {
            Debug.Log("Patrol right");
            gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
            return new Vector3(speed, self.velocity.y, self.velocity.z);
        }
        else
        {
            Debug.Log("Patrol left");
            gameObject.transform.rotation = Quaternion.LookRotation(-Vector3.right);
            return new Vector3(-speed, self.velocity.y, self.velocity.z);
        }
    }
    private void switchDirection()
    {
        goRight = !goRight;
    }
}