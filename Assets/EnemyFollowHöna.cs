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

    // Start is called before the first frame update
    void Start()
    {
        //Hönan letar efter en spelare med taggen "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if satsen gör att när spelaren är inom en viss area så kommer hönan att börja flytta på sig mot spelaren. 
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSite && distanceFromPlayer>shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        //Else if satsen gör att om spelaren är inom "shooting range" så kommer hönan att börja skuta ägg
        else if (distanceFromPlayer <= shootingRange && nextFireTime <Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextFireTime = Time.time + fireRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        //Färgen på cirklarna är grön. Dem kommer inte visas i riktiga spelet. 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

}
