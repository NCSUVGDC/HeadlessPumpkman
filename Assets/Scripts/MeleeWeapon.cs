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
    public playerMovement movementManager;
    public Rigidbody playerBody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            //Simultaneously deal damage to the enemy and check if it is dead as a result
            bool enemyKilled = other.GetComponent<Enemy>().TakeDamage(damageVal);

            if (enemyKilled)
            {
                movementManager.PushPlayer(true, speedBoostForce);
            }
            else
            {
                movementManager.PushPlayer(false, knockBackForce);
            }
        }
        if (other.tag.Equals("Terrain"))
        {
            movementManager.PushPlayer(true, speedBoostForce);
        }
        if(other.tag.Equals("Crate"))
        {
            other.GetComponent<Crate>().destroyAnimation();
            other.GetComponent<Crate>().setAnimState(true);

        }
    }
}
