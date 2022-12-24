using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

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

    public Text playerName;

    PhotonView view;

    //private GameObject toDestroy;

    void Start()
    {
        //ResetParticles();
        //player1LivesTxt.text = MenuManager.p1Name + " lives left: " + player1Lives;
        PlayerCurrentHealth = PlayerMaxHealth;
        healthBar.SetMaxHealth(PlayerMaxHealth);
        view = GetComponent<PhotonView>();

        playerName.text = view.Owner.NickName;
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
                DestroyTank();
            }
            healthBar.SetHealth(PlayerCurrentHealth);
            /*
            if (player1Lives == 0)
            {
                toRespawn = true;
            }*/

            if (PlayerCurrentHealth < 65 && PlayerCurrentHealth > 35 && hit)
            {
                smoke.Play();
                hit = false;
            }

            if (PlayerCurrentHealth < 34 && PlayerCurrentHealth > 10 && hit)
            {
                sparks.Play();
                hit = false;
            }

            if (PlayerCurrentHealth < 9 && PlayerCurrentHealth > 0 && hit)
            {
                flames.Play();
                hit = false;
            }
        }
    }

    public void DestroyTank()
    {
        if (view.IsMine)
        {
            //player1Lives--;
            deathAudio.Play();
            //player1LivesTxt.text = MenuManager.p1Name + " lives left: " + player1Lives;
            dead = true;
            Destroy(PhotonNetwork.Instantiate(explosion1.name, this.transform.position, Quaternion.identity), 3f);
            //Invoke("DestroyExplosion", 3f);
            this.gameObject.SetActive(false);
        }
    }

    public void ResetParticles()
    {
        sparks.Stop();
        flames.Stop();
        smoke.Stop();
    }
    /*
    void DestroyExplosion()
    {
        PhotonNetwork.Destroy(toDestroy);
    }*/
}