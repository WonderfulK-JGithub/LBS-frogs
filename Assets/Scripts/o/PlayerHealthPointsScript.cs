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

    //Räknar framesen man varit odödlig i - KJ
    private float frameCount;

    private bool allowHit = true;

    SpriteRenderer rend;
    void Start()
    {
        playerHealth = playerMaxHealth;

        rend = GetComponent<SpriteRenderer>();
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
        if (enemy != null)
        {
            TakeDamage(enemy.damage);
        }
    }

    void TakeDamage(int damage)
    {
        //kollar om man är temporärt odödlig
        if(allowHit)
        {
            //Tar bort hp från spelaren
            playerHealth -= damage;

            //kollar om man har 0 eller mindre hp
            if (playerHealth <= 0)
            {
                playerHealth = 0;
                GameOver();
            }
            else
            {
                //Ger spelaren temporärt odödlighet (invisible frames)
                frameCount = invisFrames;
                allowHit = false;
            }
        }
        
    }

    public void GameOver()
    {
        //En game over screen eller något
    }
}
