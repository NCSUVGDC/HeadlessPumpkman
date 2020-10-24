using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
     
    public Item item = null;

    //public Transform self;

    public bool isDestroyed = true;


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

            int speed = scriptMove.getSpeed();
            speed += item.speed;
            scriptMove.setSpeed(speed);

            float jump = scriptMove.getJump();
            jump += item.jump;
            scriptMove.setJump(jump);

            if (scriptHealth.getHealth() < scriptHealth.getMaxHealth())
            {
                int health = scriptHealth.getHealth();
                health += item.health;
                scriptHealth.setHealth(health);
            }

            if(isDestroyed) Destroy(gameObject);
        }
    }


}
