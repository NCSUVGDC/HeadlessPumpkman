using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public float speed = .03f;
    private bool isActive = false;
    public Transform wall;

    public void StartMoving()
    {
        isActive = true;
    }

    public void StopMoving()
    {
        isActive = false;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            wall.position += new Vector3(speed, 0, 0);
        }
    }
}
