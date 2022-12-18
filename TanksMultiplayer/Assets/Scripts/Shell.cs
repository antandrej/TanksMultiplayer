using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float shellSpeed;
    public int shellDamage;

    public GameObject explosion;

    void Start()
    {
        shellDamage = Random.Range(20, 30);
    }

    void Update()
    {
        transform.Translate(shellSpeed * Vector3.up * Time.deltaTime);
        Destroy(this.gameObject, 7f);
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Destroy(Instantiate(explosion, this.transform.position, Quaternion.identity), 1f);
            Destroy(this.gameObject);
        }

        if (col.gameObject.tag == "Player1")
        {
            col.gameObject.GetComponent<Player1Controller>().Player1CurrentHealth -= shellDamage;
            Destroy(Instantiate(explosion, this.transform.position, Quaternion.identity), 1f);
            col.gameObject.GetComponent<Player1Controller>().hit = true;
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Player2")
        {
            col.gameObject.GetComponent<Player2Controller>().Player2CurrentHealth -= shellDamage;
            Destroy(Instantiate(explosion, this.transform.position, Quaternion.identity), 1f);
            col.gameObject.GetComponent<Player2Controller>().hit = true;
            Destroy(this.gameObject);
        }
    }
}
