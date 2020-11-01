using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    private bool startedAnimation = false;
    public Animator animator;
    public void destroyAnimation()
    {
        animator.SetBool("destroyAnim", true);
    }
    public void setAnimState(bool state)
    {
        startedAnimation = state;
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        GameObject other = collision.collider.gameObject;

        if (other != null)
        {
            if (other.CompareTag("wallOfDeath") || other.CompareTag("wallHands"))
            {
                Debug.Log("collides");
                destroyAnimation();
            }
        }
    }
}

