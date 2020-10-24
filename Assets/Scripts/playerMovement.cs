using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

    public int speed = 5;
    public Rigidbody player;
    public float jumpheight = 7;
    public float health = 3;

    

    private void FixedUpdate()
    {
        if (player != null)
        {
            player.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, player.velocity.y, player.velocity.z);

            if (player.velocity.x > 0)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(Vector3.right);
            }
            if (player.velocity.x < 0)
            {
                gameObject.transform.rotation = Quaternion.LookRotation(-Vector3.right);
            }
            if (Input.GetKey(KeyCode.W) && GroundCheck())
            {
                player.velocity = new Vector3(player.velocity.x, jumpheight, player.velocity.z);
            }
        }
    }

    

    private bool GroundCheck()
    {
        float distance = 1.1f;
        Vector3 dir = new Vector3(0, -1, 0);
        return Physics.Raycast(player.position, dir, distance);
    }

    public void setSpeed(int value)
    {
        speed = value;
    }

    public int getSpeed()
    {
        return speed;
    }

    public void setJump(float value)
    {
        jumpheight = value;
    }

    public float getJump()
    {
        return jumpheight;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
}
