using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text respawnText;
    public GameObject p1;
    public GameObject p2;
    public Player1Controller p1c;
    public Player2Controller p2c;
    public GameObject hbar1;
    public GameObject hbar2;

    float minDistance = 80;
    float maxDistance = 110;

    void Start()
    {
        respawnText.gameObject.SetActive(false);
        Vector3 spawnPosition = p2.transform.position;
        spawnPosition += new Vector3(Mathf.Cos(Random.Range(-Mathf.PI, Mathf.PI)), 0, Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI))) * Random.Range(minDistance, maxDistance);
        spawnPosition.x = Mathf.Clamp(spawnPosition.x, spawnPosition.x - 80, spawnPosition.x + 110);
        spawnPosition.y = 0.25f;
        spawnPosition.z = Mathf.Clamp(spawnPosition.z, spawnPosition.z - 80, spawnPosition.z + 100);
        p1.transform.position = spawnPosition;
    }

    void Update()
    {        
        if (p1c.dead)
        {
            respawnText.gameObject.SetActive(true);
            respawnText.text = "PRESS ENTER TO RESPAWN\nPLAYER 1";
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Vector3 spawnPosition = p2.transform.position;
                spawnPosition += new Vector3(Mathf.Cos(Random.Range(-Mathf.PI, Mathf.PI)), 0, Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI))) * Random.Range(minDistance, maxDistance);
                spawnPosition.x = Mathf.Clamp(spawnPosition.x, spawnPosition.x - 80, spawnPosition.x + 110);
                spawnPosition.y = 0.25f;
                spawnPosition.z = Mathf.Clamp(spawnPosition.z, spawnPosition.z - 80, spawnPosition.z + 110);
                p1.transform.position = spawnPosition;
                p1c.dead = false;               
                p1c.Player1CurrentHealth = p1c.Player1MaxHealth;
                respawnText.gameObject.SetActive(false);
                p1c.ResetParticles();
                hbar1.gameObject.SetActive(true);
                p1.gameObject.SetActive(true);
            }
        }

        if (p1c.toRespawn)
        {
            respawnText.gameObject.SetActive(true);
            respawnText.text = "PLAYER 2 WON\nPRESS ENTER TO START NEW GAME";
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if (p2c.dead)
        {
            respawnText.gameObject.SetActive(true);
            respawnText.text = "PRESS ENTER TO RESPAWN\nPLAYER 2";
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Vector3 spawnPosition = p1.transform.position;
                spawnPosition += new Vector3(Mathf.Cos(Random.Range(-Mathf.PI, Mathf.PI)), 0, Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI))) * Random.Range(minDistance, maxDistance);
                spawnPosition.x = Mathf.Clamp(spawnPosition.x, -80, spawnPosition.x + 110);
                spawnPosition.y = 0.25f;
                spawnPosition.z = Mathf.Clamp(spawnPosition.z, -80, spawnPosition.x + 110);
                p2.transform.position = spawnPosition;
                p2c.dead = false;
                p2c.Player2CurrentHealth = p2c.Player2MaxHealth;
                p2c.ResetParticles();
                respawnText.gameObject.SetActive(false);
                hbar2.gameObject.SetActive(true);
                p2.gameObject.SetActive(true);
            }
        }
        if (p2c.toRespawn)
        {
            respawnText.gameObject.SetActive(true);
            respawnText.text = "PLAYER 1 WON\nPRESS ENTER TO START NEW GAME";
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
