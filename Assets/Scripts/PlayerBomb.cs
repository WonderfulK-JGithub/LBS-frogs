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
    [SerializeField] private float escapeTime = 75;

    [SerializeField] private GameObject bombPrefab = null;
    GameObject bombReference;

    [SerializeField] private Text timerText = null;

    [SerializeField] private GameObject victoryScreen = null;

    [SerializeField] private Slider plantSlider = null;

    private bool isPlanting = false;

    SoundManager snd;

    [SerializeField] SpriteRenderer bombCountDownRenderer = null;
    [SerializeField] Animator bombCountDownAnim = null;

   

    private void Start()
    {
        snd = FindObjectOfType<SoundManager>();

        bombCountDownAnim.speed = 0;

        timerText.enabled = false;
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

                

                //om timer är 0 (vilket betyder att man kommer exploderas) - KJ
                if (timer <= 0)
                {
                    //kallar GameOver i PlayerHealthPointsScript scriptet - KJ
                    GetComponent<PlayerHealthPointsScript>().GameOver();

                    //Gör att kameran skakar lite - KJ
                    StartCoroutine(FindObjectOfType<CameraFollow>().ScreenShake());

                    //Stoppar musiken och spelar en bomb ljudeffekt - KJ
                    snd.backgroundMusic.Stop();
                    snd.PlaySound(snd.bombSoundEffect,2);

                    //förstör bombobjektet, om man av någon anledning bestämmer sig för att vänta och kolla vad som händer med bomben - KJ
                    Destroy(bombReference);

                    bombCountDownRenderer.enabled = false;

                   
                }
            }
            //annars kollar om man trycker på Plantknappen och kan planta bomben - KJ
            else if (Input.GetButtonDown("Plant") && canPlant && !countDown)
            {
                
                isPlanting = true;

                //Ger timer variabeln den tid som plantTime har - KJ
                timer = plantTime;

                //Gör att man kan se slidern - KJ
                plantSlider.gameObject.SetActive(true);
            }
        }

        if(isPlanting)
        {
            //Ser till att man inte kan planta medans man går - KJ
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                if (Input.GetButton("Plant"))
                {
                    //tar bort tid från timern
                    timer -= 1 * Time.deltaTime;

                    //Gör att man kan se på slidern hur mycket mer man måste hålla ner plantknappen - KJ
                    plantSlider.value = 1 - timer / plantTime;

                    
                    if (timer <= 0)
                    {
                        PlantBomb();
                        isPlanting = false;
                    }
                }
                else
                {
                    //slutar planta - KJ
                    isPlanting = false;
                    plantSlider.gameObject.SetActive(false);
                }
            }
            else
            {
                //slutar planta - KJ
                isPlanting = false;
                plantSlider.gameObject.SetActive(false);
            }

        }
        
        
    }

    void PlantBomb()
    {
        //Ser till att timern börjar räkna ner när man plantat bomben - KJ
        countDown = true;
        timer = escapeTime;

        bombCountDownRenderer.enabled = true;
        bombCountDownAnim.speed = 2.6f / escapeTime;

        //Skapar ett bombObjekt - KJ
        bombReference = Instantiate(bombPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);

        //Tar bort slidern - KJ
        plantSlider.gameObject.SetActive(false);

        //Ändrar musicen till en lite mer episk låt - KJ
        snd.backgroundMusic.clip = snd.superMegaEpicBombMusic;
        snd.backgroundMusic.Play();
        
    }

    void UppdateText()
    {
        //avrundar timer till 2 decimal tal och ger dens värde till timer texten - KJ
        float rounded = Mathf.Round(timer * 100)/100;
        timerText.text = rounded.ToString();
    }

    void GameWon()
    {
        //yippie
        victoryScreen.SetActive(true);

        //stänger av spelaren - KJ
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
            timerText.enabled = true;
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
            if(!countDown)timerText.enabled = false;
        }
    }
}
