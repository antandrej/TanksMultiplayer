using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    public Text respawnText;
    public GameObject playerPrefab;
    //public GameObject p2;
    private Player1Controller pc;
    //public Player2Controller p2c;
    //public GameObject hbar;
    //public GameObject hbar2;
    public Vector3 spawnPosition;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;

    void Start()
    {
        respawnText.gameObject.SetActive(false);
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), 0.25f, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        pc = playerPrefab.GetComponent<Player1Controller>();
        //playerPrefab.transform.position = spawnPosition;
    }

    void Update()
    {        
        if (pc.dead)
        {
            respawnText.gameObject.SetActive(true);
            respawnText.text = "YOU DIED\nNPRESS ENTER TO RESPAWN";
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                playerPrefab.transform.position = spawnPosition;
                pc.dead = false;               
                pc.PlayerCurrentHealth = pc.PlayerMaxHealth;
                respawnText.gameObject.SetActive(false);
                pc.ResetParticles();
                //hbar.gameObject.SetActive(true);
                playerPrefab.gameObject.SetActive(true);
            }
        }

        //if (p1c.toRespawn)
        //{
        //    respawnText.gameObject.SetActive(true);
        //    //respawnText.text = MenuManager.p2Name + " WON\nPRESS ENTER TO START NEW GAME";
        //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    }
        //}
        //
        //if (p2c.dead)
        //{
        //    respawnText.gameObject.SetActive(true);
        //    //respawnText.text = "PRESS ENTER TO RESPAWN\n" + MenuManager.p2Name;
        //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        //    {
        //        Vector3 spawnPosition = p1.transform.position;
        //        spawnPosition += new Vector3(Mathf.Cos(Random.Range(-Mathf.PI, Mathf.PI)), 0, Mathf.Sin(Random.Range(-Mathf.PI, Mathf.PI))) * Random.Range(minDistance, maxDistance);
        //        spawnPosition.x = Mathf.Clamp(spawnPosition.x, -80, spawnPosition.x + 110);
        //        spawnPosition.y = 0.25f;
        //        spawnPosition.z = Mathf.Clamp(spawnPosition.z, -80, spawnPosition.x + 110);
        //        p2.transform.position = spawnPosition;
        //        //p2c.dead = false;
        //        //p2c.Player2CurrentHealth = p2c.Player2MaxHealth;
        //        //p2c.ResetParticles();
        //        respawnText.gameObject.SetActive(false);
        //        hbar2.gameObject.SetActive(true);
        //        p2.gameObject.SetActive(true);
        //    }
        //}
        //if (p2c.toRespawn)
        //{
        //    respawnText.gameObject.SetActive(true);
        //    respawnText.text = MenuManager.p1Name + " WON\nPRESS ENTER TO START NEW GAME";
        //    if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        //    {
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //    }
        //}
    }
}
