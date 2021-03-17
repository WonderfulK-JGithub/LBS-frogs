using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBomb : MonoBehaviour
{ 

    private bool canPlant = false;
    private bool countDown = false;
    private float timer = 0;
    [SerializeField] private float plantTime = 5;
    [SerializeField] private float escapeTime = 20;

    [SerializeField] private GameObject bombPrefab = null;

    [SerializeField] private Text timerText = null;

    [SerializeField] private GameObject victoryScreen = null;

    [SerializeField] private Slider plantSlider = null;

    private bool isPlanting = false;

    private void Start()
    {
        
    }
    private void Update()
    {
        if(!PauseMenu.GameIsPaused)
        {
            //kollar om timern ska gå ner - KJ
            if (countDown)
            {
                timer -= 1 * Time.deltaTime;
                UppdateText();
                if (timer <= 0)
                {
                    GetComponent<PlayerHealthPointsScript>().GameOver();
                }
            }
            //annars kollar om man trycker på Plantknappen och kan planta bomben - KJ
            else if (Input.GetButtonDown("Plant") && canPlant && !countDown)
            {
                isPlanting = true;
                timer = plantTime;
                plantSlider.gameObject.SetActive(true);
            }
        }

        if(isPlanting)
        {
            if (Input.GetButton("Plant"))
            {
                timer -= 1 * Time.deltaTime;

                plantSlider.value = 1 - timer / plantTime;

                if (timer <= 0)
                {
                    PlantBomb();
                    isPlanting = false;
                }
            }
            else
            {
                isPlanting = false;
                plantSlider.gameObject.SetActive(false);
            }
        }
        
        
    }

    void PlantBomb()
    {
        countDown = true;
        timer = escapeTime;
        Instantiate(bombPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        plantSlider.gameObject.SetActive(false);
    }

    void UppdateText()
    {
        //avrundar timer till 2 decimal tal och ger dens värde till timer texten
        float rounded = Mathf.Round(timer * 100)/100;
        timerText.text = rounded.ToString();
    }

    void GameWon()
    {
        //yippie
        victoryScreen.SetActive(true);

        //stänger av spelaren
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

        PauseMenu.GameIsPaused = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Kollar om den har taggen BombArea - KJ
        if(collision.CompareTag("BombArea"))
        {
            //gör att man kan planta bomben
            canPlant = true;
        }
        //annars kollar den omd en har taggen StartArea - KJ
        else if(collision.CompareTag("StartArea") && countDown)
        {
            //Stänger av timern och gör banan avklarad
            countDown = false;
            timerText.color = Color.green;
            GameWon();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("BombArea"))
        {
            //gör att man inte kan planta bomben - KJ
            canPlant = false;
        }
    }
}
