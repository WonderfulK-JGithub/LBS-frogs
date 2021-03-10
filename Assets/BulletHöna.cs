using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHöna : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bulletRB;

    void Start()
    {
        //bullet, aka ägget, letar efter ett objekt med taggen "Player". När den ser, så kommer den skuta iväg en bullet mot den. 
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        Destroy(this.gameObject, 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //när den kolliderar med bara spelaren
        if (collision.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
