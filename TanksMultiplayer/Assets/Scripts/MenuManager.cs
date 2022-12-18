using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static string p1Name = "Player1";
    public static string p2Name = "Player2";

    public Text p1controls;
    public Text p2controls;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "HowToPlay")
        {
            p1controls.text = p1Name + " Controls :";
            p2controls.text = p2Name + " Controls :";
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LoadHowToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPlayer1Name(string s)
    {
        p1Name = s;
    }

    public void SetPlayer2Name(string s)
    {
        p2Name = s;
    }
}
