using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Player player;
    public Rigidbody playerBody;
    private Item powerUp = null;

    //Stuff I'm currently porting from Player.cs
    [Header("Movement Stuff")]
    public float frameAcceleration = 0.5f; //By how much the player's horizontal speed increases for every frame they hold a direction
    public float topSpeed = 10;
    public float speedBoostTime = 1; //Length of time, in seconds, that a speedBoost lasts
    public float knockBackTime = 1; //Length of time, in seconds, that a knockBack lasts
    private float pushForce = 0; //The speed at which the player is pushed
    private float pushTimer = 0; 
    private bool isBeingPushed = false;
    private bool isFacingRight = true;
    public float frameGravity = 0.5f; //By how much the player's vertical speed decreases for every frame they are in the air
    public float jumpForce = 5;
    public float jumpheight = 7;
    //public int speed = 5;


    [Header("Powerup Stuff")]
    public int speedModifier = 0;
    public float jumpModifier = 0f;
    //private bool poweredUp = false;
    private bool hasPowerUp = false;
    
    float timestamp;

    private void Start()
    {
        //Faces the player towards the right when they spawn in, because isFacingRight defaults to true
        gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
    }

    private void FixedUpdate()
    {
        if (playerBody != null)
        {
            //This if() block handles horizontal movement
            if (isBeingPushed) //When taking knockback or getting a speed boost, the pushForce overwrites any player input
            {
                playerBody.velocity = new Vector3(pushForce, playerBody.velocity.y, playerBody.velocity.z);
            }
            else //When not being pushed, and when not currently attacking, whether the player is holding a direction button will be read and converted into movement
            {
                MovePlayerFromInput(Input.GetAxisRaw("Horizontal"));
            }

            //playerBody.velocity = new Vector3(Input.GetAxis("Horizontal") * (speed + speedModifier), playerBody.velocity.y, playerBody.velocity.z);

            //if (playerBody.velocity.x > 0)
            //{
            //    gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
            //}
            //if (playerBody.velocity.x < 0)
            //{
            //    gameObject.transform.rotation = Quaternion.LookRotation(-Vector3.right);
            //}

            if (GroundCheck()) //If the player IS grounded...
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) //...and if the player is saying they want to jump...
                {
                    //playerBody.velocity = new Vector3(playerBody.velocity.x, (jumpheight + jumpModifier), playerBody.velocity.z);

                    //...start a jump
                    playerBody.velocity = new Vector3(playerBody.velocity.x, jumpForce, playerBody.velocity.z);
                }
                else //If the player is grounded but isn't trying to jump, reset their vertical velocity
                {
                    playerBody.velocity = new Vector3(playerBody.velocity.x, 0, playerBody.velocity.z);
                }
            }
            else //If the player is NOT grounded...
            {
                //...they should fall
                PullPlayerDown();
            }
        }

        if (isBeingPushed)
        {
            PushManager();
        }
        startCooldown(powerUp);
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

    //Like DeceleratePlayer(), but exclusively for the player's vertical velocity
    void PullPlayerDown()
    {
        playerBody.velocity = new Vector3(playerBody.velocity.x, playerBody.velocity.y - frameGravity,
                                                        playerBody.velocity.z);
    }

    //Responsible for the timer that dictates when the player should stop being pushed
    void PushManager()
    {
        pushTimer -= Time.deltaTime;

        if (pushTimer <= 0) //If the pushing timer has expired...
        {
            isBeingPushed = false;
            //inputOverride = true;
        }
    }

    //Sets up a temporary state of being pushed, where the player is given a constant x velocity
    public void PushPlayer(bool isSpeedBoost, float force)
    {
        Debug.Log("PushPlayer called");
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

    private bool GroundCheck()
    {
        float distance = 1.1f;
        Vector3 dir = new Vector3(0, -1, 0);
        RaycastHit other;
        bool isOverSomething = Physics.Raycast(playerBody.position, dir, out other, distance); //Checks if the player is on top of something, and sets "other" to be the thing the player is on top of
        if (isOverSomething && other.collider.gameObject.tag.Equals("Terrain")) //If the player is on top of specifically Terrain, we know that they are grounded
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void setTopSpeed(float value)
    {
        topSpeed = value;
    }

    public float getTopSpeed()
    {
        return topSpeed;
    }

    public void setJumpHeight(float value)
    {
        jumpheight = value;
    }

    public float getJumpHeight()
    {
        return jumpheight;
    }

    public void setSpeedMod(int value)
    {
        speedModifier = value;
    }

    public int getSpeedMod()
    {
        return speedModifier;
    }

    public void setJumpMod(float value)
    {
        jumpModifier = value;
    }

    public float getJumpMod()
    {
        return jumpModifier;
    }

    public void setPoweredUp(Item item)
    {
        powerUp = item;
        hasPowerUp = true;
        //Debug.Log("Set PowerUp. Duration: " + item.coolDuration);
        timestamp = Time.time;
    }

    public bool getIsFacingRight()
    {
        return isFacingRight;
    }

    public void setIsFacingRight(bool input)
    {
        isFacingRight = input;
    }

    public void startCooldown(Item item)
    {
        if(item != null && hasPowerUp)
        {
            //Debug.Log("HAS POWER UP");
            //poweredUp = true;
            
            if (Time.time < timestamp + item.coolDuration)
            {
                //Debug.Log("Time: " + Time.time + " | timestamp: " + timestamp + " | " + item.coolDuration);

                int speed = item.speed;
                setSpeedMod(speed);

                float jump = item.jump;
                setJumpMod(jump);
            }
            else
            {
                setSpeedMod(0);

                setJumpMod(0);

                //poweredUp = false;
                hasPowerUp = false;
                item = null;
            }
        }
    }
}
