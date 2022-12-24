using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{

    public InputField createInput;
    public InputField joinInput;

    public Text nick;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
        //base.OnJoinedRoom();
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Lobby")
        {
            if (PhotonNetwork.LocalPlayer.IsLocal)
            {
                nick.text = PhotonNetwork.LocalPlayer.NickName;
            }
        }
    }
}
