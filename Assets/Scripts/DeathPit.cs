using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) //När objektet kolliderar med tagen "Player", så förstörs objektet (Spelaren) och flyttar dig till en annan scene - Melker J
        {
            Destroy(collision.transform.gameObject);
            SceneManager.LoadScene(0);
        }

    }

}
