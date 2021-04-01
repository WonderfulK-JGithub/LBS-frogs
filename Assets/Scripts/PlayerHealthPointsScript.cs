using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPointsScript : MonoBehaviour
{
   
    
    [Header("HP")]
    public int playerMaxHealth;
    public int playerHealth;

    [Header("Hit Detection")]
    [SerializeField] private float invisFrames = 0;
    [SerializeField] private int flickerRate = 2; //bestämemr hur snabbt man ska blinka när man är temporärt odödlig.

    public GameObject GameOverScreen;

    //Räknar framesen man varit odödlig i - KJ
    private float frameCount;

    private bool allowHit = true;

    SpriteRenderer rend;
    HealthBar healthBar;

    [SerializeField] SpriteRenderer gameoverRend = null;
    [SerializeField] Animator gameoverAnim = null;
    [SerializeField] GameObject gameCanvas = null;
    [SerializeField] GameObject pauseMenu = null;

    void Start()
    {
        playerHealth = playerMaxHealth;

        //Får healthbaren att fungera - Max
        healthBar = FindObjectOfType<HealthBar>();
        healthBar.SetMaxHealth(playerMaxHealth);

        rend = GetComponent<SpriteRenderer>();

        gameoverAnim.speed = 0;
    }

    void Update()
    {
        if(!PauseMenu.GameIsPaused)
        {
            //kollar om man är temporärt odödlig - KJ
            if (!allowHit)
            {
                //tar bort från frameCount. om frameCount är 0 eller mindre slutar man vara temporärt odödlig - KJ
                frameCount -= 60 * Time.deltaTime;
                if (frameCount <= 0)
                {
                    allowHit = true;
                    rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
                }
                else
                {

                    //Denna uträkning ser till att man blinkar endast var enda "flickerRate" frame. Om flickerRate == 2 är uträkningen true varannan frame - KJ
                    if (Mathf.Floor(frameCount) % flickerRate == 0)
                    {
                        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 0);
                    }
                    else
                    {
                        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
                    }
                }
            }
        }
       
    }

    //Kollar om man nuddar en fiendes hitbox
    //Alla fiender har ett child objekt som har deras hitbox på sig. Den boxcollidern måste vara triggerd - KJ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Skaffar referense till enemyns EnemyBehavior script, för att kunna veta hur mycket skada spelaren ska ta
        EnemyBehavior enemy = collision.GetComponentInParent<EnemyBehavior>();

        //om enemy == null betyder det att det blev en collision med en boxcollider som var triggerd, men inte var en enemy.
        //därför ska spelaren inte ta skada - KJ
        if (enemy != null && !enemy.isDead)
        {
            TakeDamage(enemy.damage);
        }
       
    }

    public void TakeDamage(int damage)
    {
        //kollar om man är temporärt odödlig - KJ
        if(allowHit)
        {
            GameObject.Find("GameManager").GetComponent<SoundManager>().PlaySound(GameObject.Find("GameManager").GetComponent<SoundManager>().playerHurt); //Spelar ljud - Max
            //Tar bort hp från spelaren - KJ
            playerHealth -= damage;
            

            //kollar om man har 0 eller mindre hp - KJ
            if (playerHealth <= 0)
            {
                healthBar.SetHealth(0); //Uppdaterar healthbaren - Max
                playerHealth = 0;
                GameOver();
            }
            else
            {
                //Ger spelaren temporärt odödlighet (invisible frames)
                frameCount = invisFrames;
                allowHit = false;

                healthBar.SetHealth(playerHealth); //Uppdaterar healthbaren - Max
            }
        }
        
    }

    public void GameOver()
    {

        gameoverAnim.speed = 1;
        gameoverRend.enabled = true;

        //Disablar UI
        gameCanvas.SetActive(false);
        pauseMenu.SetActive(false);

        //stänger av musiken
        FindObjectOfType<SoundManager>().backgroundMusic.Stop();

        Invoke("ScreenGameOver", 4f);

        //stänger av spelaren - KJ
        GetComponent<PlayerMovement>().enabled = false;
        rend.enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        PauseMenu.GameIsPaused = true;

    }

    void ScreenGameOver()
    {
        //Sätter på game over screenen - KJ
        GameOverScreen.SetActive(true);
    }

    
}
