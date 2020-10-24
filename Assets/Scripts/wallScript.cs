using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallScript : MonoBehaviour
{
    public float speed = .03f;

    public Transform wall;

    private void FixedUpdate()
    {
        wall.position += new Vector3(speed, 0, 0);
    }
}
