using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int damageVal = 50;
    public float speedBoostVal = 10;
    public float knockBackVal = -10;
    
    //here for testing purposes
    public ForceMode fm = ForceMode.Impulse;

    public Rigidbody playerBody;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            //Simultaneously deal damage to the enemy and check if it is dead as a result
            bool enemyKilled = other.GetComponent<Enemy>().TakeDamage(damageVal);

            if (enemyKilled)
            {
                Vector3 speedBoost = new Vector3(speedBoostVal, 0, 0);
                playerBody.AddForce(speedBoost, fm);
            }
            else
            {
                Vector3 knockBack = new Vector3(knockBackVal, 0, 0);
                playerBody.AddForce(knockBack);
            }
        }
    }
}
