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
        shellDamage = Random.Range(20, 30);
    }

    void Update()
    {
        transform.Translate(shellSpeed * Vector3.up * Time.deltaTime);
        Destroy(this.gameObject, 7f); // REWORK
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall")
        {
            PhotonNetwork.Instantiate(explosion.name, this.transform.position, Quaternion.identity);
            if (GetComponent<PhotonView>().IsMine)
                StartCoroutine(DestroyShell());
            //PhotonNetwork.Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player1Controller>().PlayerCurrentHealth -= shellDamage;
            //Destroy(PhotonNetwork.Instantiate(explosion.name, this.transform.position, Quaternion.identity), 1f);
            //Invoke("DestroyExplosion", 1f);
            col.gameObject.GetComponent<Player1Controller>().hit = true;
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyShell()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
