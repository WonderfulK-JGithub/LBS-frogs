using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowHöna : MonoBehaviour
{
    public float speed;
    public float lineOfSite;
    public float shootingRange;
    public float fireRate = 1f;
    public float nextFireTime;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    //float yPos; - Melker

    Rigidbody2D rb;
    float direction = 0;

    EnemyBehavior enemyScr;
    SpriteRenderer rend;
    Animator anim;

   
    void Start()
    {
        //Hönan letar efter en spelare med taggen "Player" - Melker J
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //yPos = transform.position.y; - Melker J

        enemyScr = GetComponent<EnemyBehavior>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        //Om den dör - Melker J
        if (enemyScr.isDead)
        {
            GameObject.Find("GameManager").GetComponent<SoundManager>().PlaySound(GameObject.Find("GameManager").GetComponent<SoundManager>().hönaDie); //Spelar ljud - Max
            //disablar scriptet - Melker J
            enabled = false;
        }
        //if satsen gör att när spelaren är inom en viss area så kommer hönan att börja flytta på sig mot spelaren. - Melker J
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
        {
            //transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime); 

            //kollar vilket håll hönan ska gå - Melker J
            direction = transform.position.x > player.position.x ? -1 : 1;

            //nytt sätt för hönan att röra sig, använder sig nu av en rigidbody - Melker J
            rb.MovePosition(new Vector2(transform.position.x + speed * Time.deltaTime * direction, transform.position.y));

            //Ger spriten rätt håll
            rend.flipX = direction == 1;

            anim.SetBool("IsWalking", true);
        }
        //Else 
        else 
        {
            //if satsen gör att om spelaren är inom "shooting range" så kommer hönan att börja skuta ägg - Melker J
            if (distanceFromPlayer <= shootingRange && nextFireTime < Time.time)
            {
                Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
                nextFireTime = Time.time + fireRate;
            }
            anim.SetBool("IsWalking", false);
        }

        //transform.position = new Vector3(transform.position.x, yPos);
    }

    private void OnDrawGizmosSelected()
    {
        //Färgen på cirklarna är grön. Dem kommer inte visas i riktiga spelet. - Melker J
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

}
