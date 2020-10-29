using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public int damageVal = 50;
    public float blastRadius = 0.5f;
    public float blastDuration = 1;
    public float horSpeed = 1.5f;
    public float horOffset = 1;
    public float horScaleFactor = 2;
    public float verSpeed = 1.5f;
    public float gravForce = 5;

    private bool hasExploded = false;

    private Vector3 startPos;

    private Rigidbody playerBody;
    public GameObject myExplosion;

    private void Start()
    {
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        startPos = new Vector3(playerBody.position.x, playerBody.position.y, 0);
        gameObject.transform.position = startPos;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(playerBody.velocity.x + horOffset * horScaleFactor, verSpeed, 0);
    }

    private void FixedUpdate()
    {
        if (!hasExploded)
        {
            float prevVelX = gameObject.GetComponent<Rigidbody>().velocity.x;
            float prevVelY = gameObject.GetComponent<Rigidbody>().velocity.y;
            gameObject.GetComponent<Rigidbody>().velocity = new Vector3(prevVelX, prevVelY - gravForce, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Enemy") || collision.collider.tag.Equals("Terrain"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        myExplosion.GetComponent<Explosion>().Activate();
        gameObject.GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        hasExploded = true;
    }
}
