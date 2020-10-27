using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxEffect : MonoBehaviour
{

    private float length, startpos;
    public Camera mainCamera;
    public float parallax;
    private float dist;
    

    //public GameObject back

    private Vector2 screenBounds;
    
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));

        
        
    }

    void FixedUpdate()
    {
        dist = ((mainCamera.transform.position.x) * parallax);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        
    }

    public void setPos(float val, float offset)
    {
        startpos = val - dist + offset;
    }
}
