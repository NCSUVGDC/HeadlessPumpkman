using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Rigidbody player;
    //public float scrollDistance;
    //public float scrollSpeed = 0.1f;

    private void Start()
    {
        //scrollDistance = Camera.main.transform.position.x;
    }

    private void FixedUpdate()
    {
        Camera.main.transform.position = new Vector3(player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //Debug.Log(player.transform.position.x);
        //Camera.main.transform.position = new Vector3(scrollDistance, Camera.main.transform.position.y, Camera.main.transform.position.z);
        //scrollDistance += scrollSpeed;
    }
}
