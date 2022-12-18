using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
//using Photon.Pun;
//using Photon.Realtime;

//[RequireComponent(typeof(InputField))]
public class MenuManager : MonoBehaviour
{
    //const string playerNamePrefKey = "PlayerName";
    //public static string p2Name = "Player2";

    public Text pcontrols;
    //public Text p2controls;

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "HowToPlay")
        {
            pcontrols.text = PhotonNetwork.NickName + " controls:";
        }
        //string defaultName = string.Empty;
        //InputField _inputField = this.GetComponent<InputField>();
        //if (_inputField != null)
        //{
        //    if (PlayerPrefs.HasKey(playerNamePrefKey))
        //    {
        //        defaultName = PlayerPrefs.GetString(playerNamePrefKey);
        //        _inputField.text = defaultName;
        //    }
        //}

        //PhotonNetwork.NickName = defaultName;
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
