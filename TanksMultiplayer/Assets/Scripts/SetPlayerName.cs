using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SetPlayerName : MonoBehaviourPun
{
    public Text playerName;

    void Start()
    {
        if (photonView.IsMine) { return; }

        SetName();
    }

    private void SetName() => playerName.text = photonView.Owner.NickName;
}
