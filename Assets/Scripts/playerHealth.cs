using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{
    public int health = 3;

    public GameObject player;
    public GameObject levelTimer;
    public GameObject wall;

    public int maxHealth = 3;
    public float endTimer = 2;

    Sprite fullHp = null;
    Sprite emptyHp = null;

    private void Start()
    {
        fullHp = Resources.Load<Sprite>("Sprites/HealthIcon1");
        emptyHp = Resources.Load<Sprite>("Sprites/HealthIcon2");
    }
    

    private void FixedUpdate()
    {
        changeHealth();



        if (health <= 0)
        {
            levelTimer.GetComponent<LevelTimer>().ChangeTimerState(false);
            endTimer -= Time.deltaTime;
            if (endTimer <= 0)
            {
                SceneManager.LoadScene("StartMenu");
            }
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
