using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void enterCampaign()
    {
        SceneManager.LoadScene("Level1_Joseph");
    }

    public void enterEndless()
    {
        SceneManager.LoadScene("NicoScene");
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
