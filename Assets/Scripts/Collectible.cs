using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float value = 1;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.tag.Equals("Player"))
        {
            other.GetComponent<Player>().ChangeCoinCount(value);
            Destroy(gameObject);
        }
    }
}
