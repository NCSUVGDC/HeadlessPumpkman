using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health = 3;

    public GameObject player;

    public GameObject wall;

    public int maxHealth = 3;

    Sprite fullHp = null;
    Sprite emptyHp = null;

    private void Start()
    {
        fullHp = Resources.Load<Sprite>("HealthIcon1");
        emptyHp = Resources.Load<Sprite>("HealthIcon2");
    }
    

    private void FixedUpdate()
    {
        changeHealth();



        if (health <= 0)
        {
            Destroy(player);
        }

       


    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if(other != null)
        {
            if(other.CompareTag("wallOfDeath"))
            {
                health = 0;
            }
        }
    }

    public void setHealth(int value)
    {
        health = value;
    }

    public int getHealth()
    {
        return health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }


    public void changeHealth()
    {

        GameObject healthBar = GameObject.Find("HealthBar");
        Image[] healthBarImages = healthBar.GetComponentsInChildren<Image>();


        if (healthBar != null)
        {
            if (health >= 3)
            {
                if (healthBarImages[2].sprite != fullHp)
                {
                    healthBarImages[2].sprite = fullHp;
                }
                if (healthBarImages[1].sprite != fullHp)
                {
                    healthBarImages[1].sprite = fullHp;
                }
                if (healthBarImages[0].sprite != fullHp)
                {
                    healthBarImages[0].sprite = fullHp;
                }
            }
            else if (health == 2)
            {
                if (healthBarImages[2].sprite != emptyHp)
                {
                    healthBarImages[2].sprite = emptyHp;
                }
                if (healthBarImages[1].sprite != fullHp)
                {
                    healthBarImages[1].sprite = fullHp;
                }
                if (healthBarImages[0].sprite != fullHp)
                {
                    healthBarImages[0].sprite = fullHp;
                }
            }
            else if (health == 1)
            {
                if (healthBarImages[2].sprite != emptyHp)
                {
                    healthBarImages[2].sprite = emptyHp;
                }
                if (healthBarImages[1].sprite != emptyHp)
                {
                    healthBarImages[1].sprite = emptyHp;
                }
                if (healthBarImages[0].sprite != fullHp)
                {
                    healthBarImages[0].sprite = fullHp;
                }
            }
            else
            {
                if (healthBarImages[2].sprite != emptyHp)
                {
                    healthBarImages[2].sprite = emptyHp;
                }
                if (healthBarImages[1].sprite != emptyHp)
                {
                    healthBarImages[1].sprite = emptyHp;
                }
                if (healthBarImages[0].sprite != emptyHp)
                {
                    healthBarImages[0].sprite = emptyHp;
                }
            }
        }

    }
}
