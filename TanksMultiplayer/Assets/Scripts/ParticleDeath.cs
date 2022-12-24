using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    void Start()
    {
        Debug.Log("instantiated");
        StartCoroutine(DestroyParticle());
    }

    IEnumerator DestroyParticle()
    {
        yield return new WaitForSeconds(0.74f);
        Debug.Log("destroyed");
        PhotonNetwork.Destroy(this.gameObject);
    }
}
