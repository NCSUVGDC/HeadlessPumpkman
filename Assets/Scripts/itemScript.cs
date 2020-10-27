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
    

    private void Start()
    {
       hasCooldown = item.hasCooldown;
    }
    private void FixedUpdate()
    {
       
    }

    

    private void OnTriggerEnter(Collider other)
    {
        
        if (other != null && other.CompareTag("Player"))
        {
            GameObject player = GameObject.Find("Player");
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

            

            if (scriptHealth.getHealth() + item.health < scriptHealth.getMaxHealth())
            {
                int health = scriptHealth.getHealth();
                health += item.health;
               // Debug.Log(item.health);
                scriptHealth.setHealth(health);
            }

            if(isDestroyed) Destroy(gameObject);
        }
    }


}
