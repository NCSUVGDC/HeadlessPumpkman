using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;

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
}
