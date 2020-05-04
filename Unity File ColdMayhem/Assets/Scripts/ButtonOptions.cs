using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonOptions : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void ArenaSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Debug()
    {
        SceneManager.LoadScene(2);
    }

    //Below here Arena selection buttons

    public void Arena01()
    {
        SceneManager.LoadScene(2);
    }

    public void Arena02()
    {
        SceneManager.LoadScene(4);
    }

    public void Arena03()
    {
        SceneManager.LoadScene(4);
    }

    public void Arena04()
    {
        SceneManager.LoadScene(3);
    }
    public void Arena05()
    {
        SceneManager.LoadScene(6);
    }
}

