﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Player name input field. Let the user input his name, will appear above the player in the game.
/// </summary>
[RequireComponent(typeof(InputField))]
public class PlayerNameInputField : MonoBehaviour
{

    // Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";

    /// MonoBehaviour method called on GameObject by Unity during initialization phase.
    void Start() {

        string defaultName = string.Empty;
        InputField _inputField = this.GetComponent<InputField>();
        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            PhotonNetwork.NickName = defaultName;
        }
    }

    /// &lt;summary&gt;
    /// Sets the name of the player, and save it in the PlayerPrefs for future sessions.
    /// &lt;/summary&gt;
    /// &lt;param name="value"&gt;The name of the Player&lt;/param&gt;
    public void SetPlayerName(string value)
    {
        // #Important
        if (string.IsNullOrEmpty(value))
        {
            Debug.LogError("Player Name is null or empty");
            return;
        }

        PhotonNetwork.NickName = value;

        PlayerPrefs.SetString(playerNamePrefKey, value);
    }
}
