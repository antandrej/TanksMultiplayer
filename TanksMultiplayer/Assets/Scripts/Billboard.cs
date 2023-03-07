using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Billboard : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0, 7.5f, 0);

    
    public PhotonView view;

    void Start()
    {

        //if (player.gameObject.tag == "Player1")
        //{
        //if (PhotonNetwork.LocalPlayer.IsLocal)
        //{
        //    this.gameObject.GetComponentInChildren<Text>().text = view.Owner.NickName;
        //}
        //}

        //if (player.gameObject.tag == "Player2")
        //{
        //    this.gameObject.GetComponentInChildren<Text>().text = MenuManager.p2Name;
        //}
    }

    void Update()
    {
        transform.rotation = Quaternion.identity;
        transform.position = player.transform.position + offset;

        //if (player.gameObject.tag == "Player1" && player.GetComponent<Player1Controller>().Player1CurrentHealth <= 0)
        //{
            //this.gameObject.SetActive(false);
        //}

        //if (player.gameObject.tag == "Player2" && player.GetComponent<Player2Controller>().Player2CurrentHealth <= 0)
        //{
        //    this.gameObject.SetActive(false);
        //}
    }
}
