using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player1Controller : MonoBehaviour
{
    ///public int player1Lives = 3;
    public int PlayerMaxHealth = 100;
    public int PlayerCurrentHealth;

    public float moveSpeed;
    private float timeStamp;
    public float cooldown;

    public bool hit = false;
    public bool dead;
    public bool toRespawn = false;
    public bool rotating = false;

    public Transform firePos;
    public GameObject shell;
    public HealthBar1 healthBar;
    //public Text player1LivesTxt;

    private GameObject toDestroy;
    public GameObject explosion1;
    public GameObject explosion2;
    public ParticleSystem sparks;
    public ParticleSystem smoke;
    public ParticleSystem flames;
    /*
    private GameObject instobject1;
    private GameObject instobject2;
    private GameObject instobject3;
    */
    public AudioSource movingAudio;
    public AudioSource shootAudio;
    public AudioSource deathAudio;

    public Text playerNametxt;

    PhotonView view;

    Vector3 spawnPosition;
    public GameObject respawnText;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    //private GameObject toDestroy;

    void Start()
    {
        view = GetComponent<PhotonView>();
        //ResetParticles();
        //player1LivesTxt.text = MenuManager.p1Name + " lives left: " + player1Lives;
        PlayerCurrentHealth = PlayerMaxHealth;
        healthBar.SetMaxHealth(PlayerMaxHealth);

        playerNametxt.text = view.Owner.NickName;

        spawnPosition = new Vector3(Random.Range(minX, maxX), 0.25f, Random.Range(minZ, maxZ));
        if (view.IsMine)
        {
            respawnText = GameObject.FindWithTag("Respawn");
            respawnText.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        if (view.IsMine)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (!rotating)
                {
                    moveSpeed = 15f;
                    transform.Translate(moveSpeed * Vector3.right * Time.deltaTime);
                }

                if (rotating)
                {
                    moveSpeed = 7;
                    transform.Translate(moveSpeed * Vector3.right * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (!rotating)
                {
                    moveSpeed = 15f;
                    transform.Translate(moveSpeed * Vector3.left * Time.deltaTime);
                }

                if (rotating)
                {
                    moveSpeed = 7;
                    transform.Translate(moveSpeed * Vector3.left * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(0f, 1.7f, 0f);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(0f, -1.7f, 0f);
            }
        }
    }

    void Update()
    {
        healthBar.SetHealth(PlayerCurrentHealth);
        if (view.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
            {
                movingAudio.Play();
                rotating = true;
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                movingAudio.Stop();
                rotating = false;
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
            {
                movingAudio.Play();
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                movingAudio.Stop();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (timeStamp <= Time.time)
                {
                    shootAudio.Play();
                    timeStamp = Time.time + cooldown;
                    PhotonNetwork.Instantiate(shell.name, firePos.position, transform.rotation * shell.transform.rotation);
                }
            }
            if (PlayerCurrentHealth <= 0)
            {
                view.RPC("DestroyTank", RpcTarget.All);
                //DestroyTank();
            }
            //healthBar.SetHealth(PlayerCurrentHealth);
            /*
            if (player1Lives == 0)
            {
                toRespawn = true;
            }*/

            if (PlayerCurrentHealth < 65 && PlayerCurrentHealth > 35 && hit)
            {
                //view.RPC("PlayParticle", RpcTarget.All, smoke);
                PlayParticle(smoke);
                hit = false;
            }

            if (PlayerCurrentHealth < 34 && PlayerCurrentHealth > 10 && hit)
            {
                //view.RPC("PlayParticle", RpcTarget.All, sparks);
                PlayParticle(sparks);
                hit = false;
            }

            if (PlayerCurrentHealth < 9 && PlayerCurrentHealth > 0 && hit)
            {
                //view.RPC("PlayParticle", RpcTarget.All, flames);
                PlayParticle(flames);
                hit = false;
            }

            if (dead)
            {
                Debug.Log("Dead");
                respawnText.gameObject.SetActive(true);
                //view.RPC("Resp", RpcTarget.All, true);
                respawnText.GetComponent<Text>().text = "YOU DIED\nRESPAWNING IN 3 SECONDS";
                Debug.Log("text active");
                view.RPC("SpawnPlayer", RpcTarget.All);
                Invoke("SetResp", 3f);
                //if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
                //{                   
                //    this.transform.position = spawnPosition;
                //    dead = false;
                //    PlayerCurrentHealth = PlayerMaxHealth;
                //    view.RPC("Resp", RpcTarget.All, false);
                //    //respawnText.gameObject.SetActive(false);
                //    ResetParticles();
                //    this.gameObject.SetActive(true);
                //    Debug.Log("Alive");
                //}
            }
        }
    }

    [PunRPC]
    public void DestroyTank()
    {
        dead = true;
        toDestroy = PhotonNetwork.Instantiate(explosion1.name, this.transform.position, Quaternion.identity);
        //player1Lives--;
        deathAudio.Play();
        //player1LivesTxt.text = MenuManager.p1Name + " lives left: " + player1Lives;
        DestroyParticle();
        //Invoke("DestroyExplosion", 3f);
        this.gameObject.SetActive(false);
    }

    [PunRPC]
    public void ResetParticles()
    {
        sparks.Stop();
        flames.Stop();
        smoke.Stop();
    }

    [PunRPC]
    void PlayParticle(ParticleSystem particle)
    {
        particle.Play();
    }

    [PunRPC]
    void DestroyParticle()
    {
        Destroy(toDestroy, 3f);
    }

    //[PunRPC]
    //void Resp(bool show)
    //{
    //    respawnText.gameObject.SetActive(show);
    //}

    [PunRPC]
    void SpawnPlayer()
    {
        Invoke("SpawnPlayerIn", 3f);
    }

    void SpawnPlayerIn()
    {
        //yield return new WaitForSeconds(3f);
        spawnPosition = new Vector3(Random.Range(minX, maxX), 0.25f, Random.Range(minZ, maxZ));
        this.transform.position = spawnPosition;
        dead = false;
        PlayerCurrentHealth = PlayerMaxHealth;
        //view.RPC("Resp", RpcTarget.All, false);
        //respawnText.gameObject.SetActive(false);
        view.RPC("ResetParticles", RpcTarget.All);
        this.gameObject.SetActive(true);
        Debug.Log("Alive");
    }

    void SetResp()
    {
        respawnText.gameObject.SetActive(false);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    stream.SendNext(transform.position);
        //    stream.SendNext(transform.rotation);
        //}
        //else
        //{
        //    transform.position = (Vector3)stream.ReceiveNext();
        //    transform.rotation = (Quaternion)stream.ReceiveNext();

        //    float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
        //    transform.position += GetComponent<Rigidbody>().velocity * lag;
        //}
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