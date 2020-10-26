using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public int damageVal = 50;

    [Header("Absolute values only!")]
    public float speedBoostForce = 1.5f;
    public float knockBackForce = 1.5f;
    

    //here for testing purposes
    public ForceMode fm = ForceMode.Impulse;

    public Player player;
    public Rigidbody playerBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            //Simultaneously deal damage to the enemy and check if it is dead as a result
            bool enemyKilled = other.GetComponent<Enemy>().TakeDamage(damageVal);

            if (enemyKilled)
            {
                player.PushPlayer(true, speedBoostForce);
            }
            else
            {
                player.PushPlayer(false, knockBackForce);
            }
        }
        if (other.tag.Equals("Terrain"))
        {
            player.PushPlayer(true, speedBoostForce);
        }
    }
}
