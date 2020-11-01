using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
     
    public Item item = null;

    //public Transform self;

    public bool isDestroyed = true;

    private bool hasCooldown = false;
    private bool poweredUp = false;

    public bool hasTrigger = true;
    

    private void Start()
    {
       hasCooldown = item.hasCooldown;
    }

   

    private void OnTriggerEnter(Collider other)
    {
        
        if (other != null && other.CompareTag("Player") && hasTrigger)
        {
            GameObject player = other.gameObject;
            playerMovement scriptMove = (playerMovement)player.GetComponent(typeof(playerMovement));
            playerHealth scriptHealth = (playerHealth)player.GetComponent(typeof(playerHealth));

            if(hasCooldown)
            {
                if(!poweredUp)
                {
                    scriptMove.setPoweredUp(item);
                    //poweredUp = true;
                    //float timestamp = Time.time;
                    //if (Time.time < timestamp + item.coolDuration)
                    //{
                    //    Debug.Log("Time: " + Time.time + " | timestamp: " + timestamp);

                    //    int speed = item.speed;
                    //    scriptMove.setSpeedMod(speed);

                    //    float jump = item.jump;
                    //    scriptMove.setJumpMod(jump);
                    //}
                    //else
                    //{
                    //    scriptMove.setSpeedMod(0);

                    //    scriptMove.setJumpMod(0);

                    //    poweredUp = false;
                    //}

                    //while(Time.time < timestamp + item.coolDuration)
                    //{
                    //    int speed = item.speed;
                    //    scriptMove.setSpeedMod(speed);

                    //    float jump = item.jump;
                    //    scriptMove.setJumpMod(jump);
                    //}



                }
            }
            else
            {
                int speed = item.speed;
                scriptMove.setSpeedMod(speed);

                float jump = item.jump;
                scriptMove.setJumpMod(jump);
            }

            Debug.Log(scriptHealth.getMaxHealth() + " : " + item.health + " : " + scriptHealth.getMaxHealth());

            if (scriptHealth.getHealth() + item.health < scriptHealth.getMaxHealth())
            {
                int health = scriptHealth.getHealth();
                health += item.health;
                //Debug.Log(item.health);
                scriptHealth.setHealth(health);
            }

            if(isDestroyed) Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision != null && collision.gameObject.CompareTag("Player") && !hasTrigger)
        {
            GameObject player = collision.gameObject;
            playerMovement scriptMove = (playerMovement)player.GetComponent(typeof(playerMovement));
            playerHealth scriptHealth = (playerHealth)player.GetComponent(typeof(playerHealth));

            if (hasCooldown)
            {
                if (!poweredUp)
                {
                    scriptMove.setPoweredUp(item);
                }
            }
            else
            {
                int speed = item.speed;
                scriptMove.setSpeedMod(speed);

                float jump = item.jump;
                scriptMove.setJumpMod(jump);
            }

            

            if (scriptHealth.getHealth() + item.health < scriptHealth.getMaxHealth())
            {
                int health = scriptHealth.getHealth();
                health += item.health;
                //Debug.Log(item.health);
                scriptHealth.setHealth(health);
            }

            if (isDestroyed) Destroy(gameObject);
        }
    }
}
