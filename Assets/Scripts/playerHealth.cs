using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public int health = 3;

    public GameObject player;

    public GameObject wall;

    public int maxHealth = 3;

    

    private void FixedUpdate()
    {
        if(health <= 0)
        {
            Destroy(player);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if(other != null)
        {
            if(other.CompareTag("wallOfDeath"))
            {
                health = 0;
            }
        }
    }

    public void setHealth(int value)
    {
        health = value;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    
}
