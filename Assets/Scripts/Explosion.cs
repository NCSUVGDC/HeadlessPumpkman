﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject myBomb;
    private float blastTimer = 0;
    private bool isActive = false;

    private void Start()
    {
        blastTimer = myBomb.GetComponent<RangedWeapon>().blastDuration;
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            blastTimer -= Time.deltaTime;
            if (blastTimer <= 0)
            {
                Destroy(myBomb);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            Debug.Log("OnTriggerEnter()");
            other.GetComponent<Enemy>().TakeDamage(myBomb.GetComponent<RangedWeapon>().damageVal);
        }
        else if (other.tag.Equals("Crate"))
        {
            Debug.Log("OnTriggerEnter()");
            other.GetComponent<Crate>().destroyAnimation();
        }
    }

    public void Activate()
    {
        isActive = true;
        gameObject.GetComponent<SphereCollider>().enabled = true;
        //gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
}
