using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    public int damageVal = 50;
    public float explosionRadius = 0.5f;
    public float percentProgress = 0;
    public float previousProgress = 0;
    public float bombSpeed = 1.5f;
    public float horizontalRange = 4;
    public Vector2 startPos;
    public Vector2 currentPos;

    public Rigidbody playerBody;

    private void Start()
    {
        playerBody = GameObject.Find("Player").GetComponent<Rigidbody>();
        startPos = new Vector2(playerBody.position.x, playerBody.position.y);
        gameObject.transform.position = startPos;
        currentPos = startPos;
    }

    private void FixedUpdate()
    {
        previousProgress = percentProgress;
        float xPos = currentPos.x + Time.deltaTime * bombSpeed;
        float yPos = -(Mathf.Pow(xPos - (horizontalRange / 2) - startPos.x, 2)) + horizontalRange + startPos.y;
        currentPos = new Vector3(xPos, yPos, 0);
        gameObject.transform.position = currentPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something has collided with a bomb.");
        if (other.tag.Equals("Enemy") || other.tag.Equals("Terrain"))
        {
            Debug.Log("The bomb should now explode!");
            Explode();
        }
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
