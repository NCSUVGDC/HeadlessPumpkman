using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void enterCampaign()
    {
        SceneManager.LoadScene("Level1");
    }

    public void enterEndless()
    {
        SceneManager.LoadScene("Endless");
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void quitGame()
    {
        //Debug.Log("Quitting");
        Application.Quit();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
