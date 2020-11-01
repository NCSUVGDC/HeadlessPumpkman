using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class LevelEdge : MonoBehaviour
{
    public bool isStartLine; //This script is attached to both the level's starting line and finish line, and this boolean makes that distinction
    public Text levelTimer;
    public string nextLevel;
    public float nextLevelTimer = 3;
    private bool levelEnded = false;

    private void Start()
    {
        //levelTimer = GameObject.Find("Level Timer").GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (levelEnded && !isStartLine)
        {
            nextLevelTimer -= Time.deltaTime;
            if (nextLevelTimer <= 0)
            {
                SceneManager.LoadScene(nextLevel);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            if (isStartLine)
            {
                //Start the level (and the timer)
                levelTimer.GetComponent<LevelTimer>().ChangeTimerState(true);
            }
            else //If this is a finish line...
            {
                //End the level (and stop the timer)
                levelEnded = true;
                levelTimer.GetComponent<LevelTimer>().ChangeTimerState(false);
            }
        }
    }
}
