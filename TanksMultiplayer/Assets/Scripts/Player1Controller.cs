using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{
    public int player1Lives = 3;
    public int Player1MaxHealth = 100;
    public int Player1CurrentHealth;

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
    public Text player1LivesTxt;

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject sparks;
    public GameObject smoke;
    public GameObject flames;

    private GameObject instobject1;
    private GameObject instobject2;
    private GameObject instobject3;

    public AudioSource movingAudio;
    public AudioSource shootAudio;
    public AudioSource deathAudio;

    void Start()
    {
        Player1CurrentHealth = Player1MaxHealth;
        healthBar.SetMaxHealth(Player1MaxHealth);
    }

    void FixedUpdate()
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

    void Update()
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
                Instantiate(shell, firePos.position, transform.rotation * shell.transform.rotation);
            }
        }
        if (Player1CurrentHealth <= 0)
        {
            DestroyTank();
        }
        healthBar.SetHealth(Player1CurrentHealth);
        if (player1Lives == 0)
        {
            toRespawn = true;
        }

        if (Player1CurrentHealth < 65 && Player1CurrentHealth > 35 && hit)
        {
            instobject1 = Instantiate(smoke, this.transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity, transform);
            hit = false;
        }

        if (Player1CurrentHealth < 34 && Player1CurrentHealth > 10 && hit)
        {
            instobject2 = Instantiate(sparks, this.transform.position + new Vector3(0, 0.5f, 0.5f), Quaternion.identity, transform);
            hit = false;
        }

        if (Player1CurrentHealth < 9 && Player1CurrentHealth > 0 && hit)
        {
            instobject3 = Instantiate(flames, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity, transform);
            hit = false;
        }
    }

    public void DestroyTank()
    {
        player1Lives--;
        deathAudio.Play();
        player1LivesTxt.text = "Player 1 lives left: " + player1Lives;
        dead = true;
        Destroy(Instantiate(explosion1, this.transform.position, Quaternion.identity), 3f);
        this.gameObject.SetActive(false);
    }

    public void ResetParticles()
    {
        Destroy(instobject1);
        Destroy(instobject2);
        Destroy(instobject3);
    }
}