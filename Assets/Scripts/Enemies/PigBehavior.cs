using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehavior : MonoBehaviour
{ 
    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 0;

    public float direction = 1;
    Vector2 moveForce;

    Rigidbody2D rb;
    [SerializeField]BoxCollider2D boxCol = null;
    SpriteRenderer spr;
    EnemyBehavior enemyScr;

    public LayerMask mask;

   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        

        //skaffar referense till spriterenderer - KJ
        spr = GetComponent<SpriteRenderer>();

        enemyScr = GetComponent<EnemyBehavior>();


    }

    private void FixedUpdate()
    {
        
        if (!PauseMenu.GameIsPaused)
        {
            moveForce = new Vector2(horizontalSpeed * direction, rb.velocity.y);

            //ger rb velocity - KJ
            rb.velocity = moveForce;

            

            //Använder OverlapBox för att kolla om den nuddar en vägg. Viktigt att väggen den nuddar ligger på layern "Solid" - KJ
            Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + new Vector3((boxCol.bounds.extents.x / 1.6f) * direction, -0.18f), new Vector2(boxCol.bounds.extents.x, boxCol.bounds.extents.y * 2f), 0f, mask);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != null && colliders[i].gameObject != gameObject)
                {
                    //vänder håll
                    direction *= -1;
                    //vänder håll på spriten - KJ
                    spr.flipX = direction == 1;
                }
            }
            

        }
        //om den dör - KJ
        if (enemyScr.isDead)
        {
            //Gör att rb är stilla och disablar detta script - KJ
            GameObject.Find("GameManager").GetComponent<SoundManager>().PlaySound(GameObject.Find("GameManager").GetComponent<SoundManager>().pigDie); //Spelar ljud - Max
            rb.bodyType = RigidbodyType2D.Static;
            this.enabled = false;
        }

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + new Vector3((boxCol.bounds.extents.x / 1.6f)  * direction, -0.2f), new Vector2(boxCol.bounds.extents.x, boxCol.bounds.extents.y * 2f));
    }
}
