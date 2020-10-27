using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopBackground : MonoBehaviour
{

    private GameObject skyObject;
    private List<GameObject> skyChildren = new List<GameObject>();

    private GameObject foreObject;
    private List<GameObject> foreChildren = new List<GameObject>();

    private GameObject backObject;
    private List<GameObject> backChildren = new List<GameObject>();
    public GameObject player;

    

    void Start()
    {

        

        foreObject = GameObject.Find("Fore");
        backObject = GameObject.Find("Back");
        for (int i = 0; i < foreObject.transform.childCount; i++)
        {
            foreChildren.Add(foreObject.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < backObject.transform.childCount; i++)
        {
            backChildren.Add(backObject.transform.GetChild(i).gameObject);
        }
    }

    void FixedUpdate()
    {
        foreChildren.Sort((x, y) => (x.transform.position.x - (x.GetComponent<SpriteRenderer>().bounds.size.x / 2)).CompareTo(y.transform.position.x - (y.GetComponent<SpriteRenderer>().bounds.size.x / 2)));
        backChildren.Sort((x, y) => (x.transform.position.x - (x.GetComponent<SpriteRenderer>().bounds.size.x / 2)).CompareTo(y.transform.position.x - (y.GetComponent<SpriteRenderer>().bounds.size.x / 2)));

        //Debug.Log(foreChildren[0].name + ": " + foreChildren[0].transform.position.x.ToString("F2") + " | " + foreChildren[1].name + ": " + foreChildren[1].transform.position.x.ToString("F2") + " | " + foreChildren[2].name + ": " + foreChildren[2].transform.position.x.ToString("F2"));
        foreach (GameObject child in foreChildren)
        {
            

            float leftBound = child.transform.position.x - (child.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float rightBound = child.transform.position.x + (child.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            if (player != null && player.transform.position.x < rightBound && player.transform.position.x > child.transform.position.x)
            {
                if(foreChildren.IndexOf(child) > 1)
                {
                    parallaxEffect parallax = (parallaxEffect)foreChildren[0].GetComponent(typeof(parallaxEffect));
                    parallax.setPos(rightBound + (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), -1.0f);
                    foreChildren[0].transform.position = new Vector3(rightBound + (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), child.transform.position.y, child.transform.position.z);
                    //foreObject.transform.GetChild(0).transform.position = new Vector3(rightBound, child.transform.position.y, child.transform.position.z);

                    break;
                }
                 
            }
            else if(player != null && player.transform.position.x > leftBound && player.transform.position.x < child.transform.position.x)
            {
                if (foreChildren.IndexOf(child) < 1)
                {
                    parallaxEffect parallax = (parallaxEffect)foreChildren[2].GetComponent(typeof(parallaxEffect));
                    parallax.setPos(leftBound - (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), +1.0f);
                    foreChildren[2].transform.position = new Vector3(leftBound - (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), child.transform.position.y, child.transform.position.z);
                    //foreObject.transform.GetChild(0).transform.position = new Vector3(rightBound, child.transform.position.y, child.transform.position.z);

                    break;
                }
            }
        }
        foreach (GameObject child in backChildren)
        {


            float leftBound = child.transform.position.x - (child.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            float rightBound = child.transform.position.x + (child.GetComponent<SpriteRenderer>().bounds.size.x / 2);
            if (player != null && player.transform.position.x < rightBound && player.transform.position.x > child.transform.position.x)
            {
                if (backChildren.IndexOf(child) > 1)
                {
                    parallaxEffect parallax = (parallaxEffect)backChildren[0].GetComponent(typeof(parallaxEffect));
                    parallax.setPos(rightBound + (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), -0.5f);
                    backChildren[0].transform.position = new Vector3(rightBound + (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), child.transform.position.y, child.transform.position.z);
                    //foreObject.transform.GetChild(0).transform.position = new Vector3(rightBound, child.transform.position.y, child.transform.position.z);

                    break;
                }

            }
            else if (player != null && player.transform.position.x > leftBound && player.transform.position.x < child.transform.position.x)
            {
                if (backChildren.IndexOf(child) < 1)
                {
                    parallaxEffect parallax = (parallaxEffect)backChildren[2].GetComponent(typeof(parallaxEffect));
                    parallax.setPos(leftBound - (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), +0.5f);
                    backChildren[2].transform.position = new Vector3(leftBound - (child.GetComponent<SpriteRenderer>().bounds.size.x / 2), child.transform.position.y, child.transform.position.z);
                    //foreObject.transform.GetChild(0).transform.position = new Vector3(rightBound, child.transform.position.y, child.transform.position.z);

                    break;
                }
            }
        }

    }
}
