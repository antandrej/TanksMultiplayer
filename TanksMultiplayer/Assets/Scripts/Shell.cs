using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float shellSpeed;
    public int shellDamage;

    public GameObject explosion;

    //private GameObject toDestroy;

    void Start()
    {
        shellDamage = 25;
        DestroyShell(7f);
    }

    void Update()
    {
        transform.Translate(shellSpeed * Vector3.up * Time.deltaTime);
        //Destroy(this.gameObject, 7f); // REWORK
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall")
        {
            //PhotonNetwork.Instantiate(explosion.name, this.transform.position, Quaternion.identity);
            SpawnExplosion();
            DestroyShell(0f);
        }

        if (col.gameObject.tag == "Player")
        {
            //PhotonNetwork.Instantiate(explosion.name, this.transform.position, Quaternion.identity);
            SpawnExplosion();
            col.gameObject.GetComponent<Player1Controller>().PlayerCurrentHealth -= shellDamage;
            col.gameObject.GetComponent<Player1Controller>().hit = true;
            DestroyShell(0f);
        }
    }
    /*
    IEnumerator DestroyShell()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(this.gameObject);
    }*/

    [PunRPC]
    void DestroyShell(float time)
    {
        Destroy(this.gameObject, time);
    }

    [PunRPC]
    void SpawnExplosion()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GetComponent<Rigidbody>().position);
            stream.SendNext(GetComponent<Rigidbody>().rotation);
            stream.SendNext(GetComponent<Rigidbody>().velocity);
        }
        else
        {
            GetComponent<Rigidbody>().position = (Vector3)stream.ReceiveNext();
            GetComponent<Rigidbody>().rotation = (Quaternion)stream.ReceiveNext();
            GetComponent<Rigidbody>().velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            GetComponent<Rigidbody>().position += GetComponent<Rigidbody>().velocity * lag;
        }
    }
}
