using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if(other.CompareTag("Player"))
        {
            playerHealth scriptHealth = (playerHealth)other.GetComponent(typeof(playerHealth));

            int health = scriptHealth.getHealth();
            health -= 1;
            scriptHealth.setHealth(health);
        }
    }
}
