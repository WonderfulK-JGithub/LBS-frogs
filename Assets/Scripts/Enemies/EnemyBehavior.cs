using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Detta är ett generellt script för alla fiender i spelet. Här i finns info om hur mycket hp dem har och när vapnena ska göra skada aktiverar dem funktioner i detta script - KJ
public class EnemyBehavior : MonoBehaviour
{
    [Header("Enemy stats")]

    public int healthPoints;
    public int damage;

    

    [Header("Hit Detection")]
    [SerializeField] private float invisFrames = 0;
    [SerializeField] private int flickerRate = 2; //bestämemr hur snabbt man ska blinka när man är temporärt odödlig.

    //Räknar framesen man varit odödlig i - KJ
    private float frameCount;

    [HideInInspector] public bool allowHit = true;

    SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //kollar om man är temporärt odödlig - KJ
        if(!allowHit)
        {
            //tar bort från frameCount. om frameCount är 0 eller mindre slutar man vara temporärt odödlig - KJ
            frameCount -= 60 * Time.deltaTime;
            if(frameCount <= 0)
            {
                allowHit = true;
                rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
            }
            else
            {
                
                //Denna uträkning ser till att fienden blinkar endast var enda "flickerRate" frame. Om flickerRate == 2 är uträkningen true varannan frame - KJ
                if(Mathf.Floor(frameCount) % flickerRate == 0)
                {
                    rend.color = new Color(rend.color.r,rend.color.g,rend.color.b,0);
                }
                else
                {
                    rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, 1);
                }
            }
        }
        
    }

    //kallas av vapnet när fienden har 0 eller mindre hp - KJ
    public void EnemyDeath()
    {
        Destroy(gameObject);
    }
    //kallas av vapnet när fienden har blivit slagen - KJ
    public void EnemyHit()
    {
        allowHit = false;
        frameCount = invisFrames;
    }
}
