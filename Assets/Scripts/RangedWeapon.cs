using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public int damageVal = 50;
    [Header("blastRadius is 2 by default")]
    public float blastRadius = 2;
    public float blastDuration = 1;
    public float horSpeed = 1.5f;
    //public float horOffset = 1;
    //public float horScaleFactor = 2;
    public float verSpeed = 1.5f;
    public float frameGravity = 5;

    private bool hasExploded = false;
    private bool isGoingRight = true;

    private Vector3 startPos;
    
    private playerMovement movementManager;
    private Rigidbody playerBody;
    public GameObject myExplosion;

    private void Start()
    {
        movementManager = GameObject.Find("Player").GetComponent<playerMovement>();
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        startPos = new Vector3(playerBody.position.x, playerBody.position.y, 0);
        gameObject.transform.position = startPos;
        myExplosion.gameObject.transform.localScale = new Vector3(blastRadius, blastRadius, blastRadius);
        

        isGoingRight = movementManager.getIsFacingRight();

        if (isGoingRight)
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(horSpeed, verSpeed, 0);
        }
        else //If the player is facing left...
        {
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(-horSpeed, verSpeed, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!hasExploded)
        {
            float prevVelX = gameObject.GetComponent<Rigidbody>().velocity.x;
            float prevVelY = gameObject.GetComponent<Rigidbody>().velocity.y;
            //Because horSpeed becomes negative in Start() if the player is facing left, this line works for both directions
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(prevVelX, prevVelY - frameGravity, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer.Equals(8))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Debug.Log("Explode()");
        myExplosion.GetComponent<Explosion>().Activate();
        gameObject.GetComponent<SphereCollider>().enabled = false;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        hasExploded = true;
    }
}
