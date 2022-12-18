using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Controller : MonoBehaviour
{
    public int player2Lives = 3;
    public int Player2MaxHealth = 100;
    public int Player2CurrentHealth;

    public float moveSpeed;
    private float timeStamp;
    public float cooldown;

    public bool hit = false;
    public bool dead;
    public bool toRespawn = false;
    public bool rotating = false;

    public Transform firePos;
    public GameObject shell;
    public HealthBar2 healthBar;
    public Text player2LivesTxt;

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
        Player2CurrentHealth = Player2MaxHealth;
        healthBar.SetMaxHealth(Player2MaxHealth);
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow))
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
        if (Input.GetKey(KeyCode.DownArrow))
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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0f, 1.7f, 0f);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0f, -1.7f, 0f);
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            movingAudio.Play();
            rotating = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            movingAudio.Stop();
            rotating = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            movingAudio.Play();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            movingAudio.Stop();
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (timeStamp <= Time.time)
            {
                shootAudio.Play();
                timeStamp = Time.time + cooldown;
                Instantiate(shell, firePos.position, transform.rotation * shell.transform.rotation);
            }
        }
        if (Player2CurrentHealth <= 0)
        {
            DestroyTank();
        }
        healthBar.SetHealth(Player2CurrentHealth);
        if (player2Lives == 0)
        {
            toRespawn = true;
        }
        if (Player2CurrentHealth < 65 && Player2CurrentHealth > 35 && hit)
        {
            instobject1 = Instantiate(smoke, this.transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity, transform);
            hit = false;
        }

        if (Player2CurrentHealth < 34 && Player2CurrentHealth > 10 && hit)
        {
            instobject2 = Instantiate(sparks, this.transform.position + new Vector3(0, 0.5f, 0.5f), Quaternion.identity, transform);
            hit = false;
        }

        if (Player2CurrentHealth < 9 && Player2CurrentHealth > 0 && hit)
        {
            instobject3 = Instantiate(flames, this.transform.position + new Vector3(0, 0.7f, 0), Quaternion.identity, transform);
            hit = false;
        }
    }

    public void DestroyTank()
    {
        player2Lives--;
        deathAudio.Play();
        player2LivesTxt.text = "Player 2 lives left: " + player2Lives;
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
