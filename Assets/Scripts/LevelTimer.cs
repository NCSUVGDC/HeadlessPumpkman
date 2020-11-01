using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelTimer : MonoBehaviour
{
    private bool isTimerEnabled = false;
    private float globalTime = 0;
    public GameObject wallOfDeath;

    // Start is called before the first frame update
    void Start()
    {
        wallOfDeath = GameObject.Find("WallOfDeath");
        globalTime = 0;
        gameObject.GetComponent<Text>().text = "00.00.00";
    }

    public void ChangeTimerState(bool input)
    {
        if (input) //Just because I think this might be a problem down the line (playing levels back to back), I'm going to hard reset the timer every time its activated
        {
            if (!isTimerEnabled)
            {
                isTimerEnabled = true;
                globalTime = 0;
                wallOfDeath.GetComponent<wallScript>().StartMoving();
                gameObject.GetComponent<Text>().text = "00.00.00";
            }
        }
        else //If the level has just ended...
        {
            isTimerEnabled = false;
            wallOfDeath.GetComponent<wallScript>().StopMoving();
        }
    }

    //Updates at a fixed rate
    void FixedUpdate()
    {
        if (isTimerEnabled)
        {
            globalTime += Time.deltaTime;
            int minutesVal = (int)(globalTime / 60);
            double secondsVal = Math.Round(globalTime - (minutesVal * 60), 3);
            string minutes = minutesVal.ToString();
            string seconds = secondsVal.ToString();

            //This is all formatting stuff; namely, adding leading and trailing zeroes to stabilize the usually jittery timer
            if (secondsVal < 10)
            {
                seconds = "0" + seconds;
            }

            if (seconds.Length == 2)
            {
                seconds = seconds + ".00";
            }
            else if (seconds.Length == 4)
            {
                seconds = seconds + "0";
            }

            if (minutesVal < 10)
            {
                minutes = "0" + minutes;
            }

            gameObject.GetComponent<Text>().text = minutes + "." + seconds;
        }
    }
}
