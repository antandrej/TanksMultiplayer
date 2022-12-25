using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDeath : MonoBehaviour
{
    void Start()
    {
        DestroyParticle();
    }

    [PunRPC]
    void DestroyParticle()
    { 
        Destroy(this.gameObject, 0.7f);
    }
}
