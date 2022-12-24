using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MenuManager : MonoBehaviour
{
    public Text pcontrols;
    //public Text p2controls;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "HowToPlay")
        {
            if (PhotonNetwork.LocalPlayer.IsLocal)
            {
                pcontrols.text = PhotonNetwork.LocalPlayer.NickName + " controls:";
            }
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Loading");
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
    /*
    public void SetPlayer1Name(string s)
    {
        p1Name = s;
    }*/

    //public void SetPlayer2Name(string s)
    //{
    //    p2Name = s;
    //}
}
