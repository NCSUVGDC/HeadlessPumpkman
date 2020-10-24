using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public Rigidbody player;


    private void Start()
    {
        
    }

    private void FixedUpdate()
    {

        if(player != null)
        {
            Camera.main.transform.position = new Vector3(player.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z);
        }



    }

   
}
