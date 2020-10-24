using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public int speed = 5;
    public Rigidbody player;
    public float jumpheight = 7;
    public float health = 3;

    public int speedModifier = 0;
    public float jumpModifier = 0f;
    //private bool poweredUp = false;
    private bool hasPowerUp = false;
    private Item powerUp = null;
    float timestamp;

    private void FixedUpdate()
    {
        if (player != null)
        {
            player.velocity = new Vector3(Input.GetAxis("Horizontal") * (speed + speedModifier), player.velocity.y, player.velocity.z);

            if (player.velocity.x > 0)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
            }
            if (player.velocity.x < 0)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(-Vector3.right);
            }
            if (Input.GetKey(KeyCode.W) && GroundCheck())
            {
                player.velocity = new Vector3(player.velocity.x, (jumpheight + jumpModifier), player.velocity.z);
            }
        }

        startCooldown(powerUp);
    }

    

    private bool GroundCheck()
    {
        float distance = 1.1f;
        Vector3 dir = new Vector3(0, -1, 0);
        return Physics.Raycast(player.position, dir, distance);
    }

    public void setSpeed(int value)
    {
        speed = value;
    }

    public int getSpeed()
    {
        return speed;
    }

    public void setJump(float value)
    {
        jumpheight = value;
    }

    public float getJump()
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

    public void startCooldown(Item item)
    {
        if(item != null && hasPowerUp)
        {
            //Debug.Log("HAS POWER UP");
            //poweredUp = true;
            
            if (Time.time < timestamp + item.coolDuration)
            {
                Debug.Log("Time: " + Time.time + " | timestamp: " + timestamp + " | " + item.coolDuration);

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
