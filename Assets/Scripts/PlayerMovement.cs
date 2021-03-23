using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Den här koden hanterar vänster höger och hopp - KJ
public class PlayerMovement : MonoBehaviour
{
    //Referense till rigidbody - KJ
    Rigidbody2D rb;
    
    [Header("Force")]
    [SerializeField,Range(1,10)] float verticalSpd = 1;
    [SerializeField,Range(0.1f,10)] float jumpForce = 0.1f;
    [SerializeField] float extraGravity = 0;
    float ogGravityScale = 0;

    [Header("Jump buffer")]
    [SerializeField,Range(0,1)] float extraTime = 0;
    

    //håller koll på hur länge man tryckt på hopp knappen
    float holdTime;

    //Vector 2 som håller koll på vilken velocity rigidbodyn ska ha - KJ
    Vector2 moveForce;

    bool hasJumped = false;

    //är till för att veta om man håller ner hoppknappen för att hoppa högre - KJ
    bool holdJumpButton = false;

    //referens till playerns collider
    BoxCollider2D boxCol;

    [HideInInspector]public Animator anim;
    SpriteRenderer rend;

    int maskIndex;

    [Header("Facing")]
    //En bool för att kolla om spelaren kollar åt höger eller vänster - Max
    public bool isFlipped = false;
    //för så att spelaren inte kan byta håll - Max
    public bool canFlip = true;

    void Awake()
    {
        //Hämtar referensen - Max
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();

        //hämtar referensen - KJ
        rb = GetComponent<Rigidbody2D>();

        holdTime = extraTime;

        ogGravityScale = rb.gravityScale;

        boxCol = GetComponent<BoxCollider2D>();
        maskIndex = LayerMask.GetMask("Solid");
    }

    
    void Update()
    {
        
        if(!PauseMenu.GameIsPaused)
        {
            //skaffar input från höger och vänster knappar - KJ
            float vert = Input.GetAxisRaw("Horizontal");

            if (holdJumpButton)
            {
                if (Input.GetButton("Jump"))
                {
                    holdTime -= 1 * Time.deltaTime; //tar bort tid från variabel holdTime - KJ

                    if (holdTime <= 0)
                    {
                        //Efter att en viss tid har gått avbryts extra hoppet - KJ
                        holdJumpButton = false;
                        holdTime = extraTime;
                        moveForce.y = rb.velocity.y;

                    }
                    else
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce); //håller kvar hoppkraften - KJ
                    }
                }
                else
                {
                    //om man inte håller in hopp knappen avbryts extra hoppet - KJ
                    holdJumpButton = false;
                    holdTime = extraTime;
                    moveForce.y = rb.velocity.y;
                }
            }
            else
            {
                moveForce.y = rb.velocity.y;
            }
            //Kollar om man trycker på hopp knappen (vilket just nu är bindat till space) och om man inte har hoppat - KJ
            if (Input.GetButtonDown("Jump") && !hasJumped)
            {
                anim.SetTrigger("jump"); //Spelar animation - Max
                anim.SetBool("isGrounded", false);

               

                FindObjectOfType<SoundManager>().PlaySound(FindObjectOfType<SoundManager>().playerJump); //Spelar ljud - Max

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);//lägger till en hopp kraft -KJ

                hasJumped = true;
                holdJumpButton = true;
            }



            moveForce = new Vector2(vert * verticalSpd, rb.velocity.y); //Ser till att den eventuellt rör sig vänster eller höger, men alltid behåller fall - KJ

            //Flippar vilket håll som spelaren kollar åt - Max
            if (canFlip)
            {
                if (vert < 0 && !isFlipped)
                {
                    isFlipped = true;
                    rend.flipX = true;
                }
                else if (vert > 0 && isFlipped)
                {
                    isFlipped = false;
                    rend.flipX = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(!PauseMenu.GameIsPaused)
        {
            //sätter rigidbodyns velocity till det den ska vara - KJ
            rb.velocity = moveForce;

            //visar gå-animationen om spelaren rör sig - Max
            if (rb.velocity.x > -0.2f && rb.velocity.x < 0.2f)
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }

            if (rb.velocity.y <= 0)
            {
                //ökar gravitationen på objektet när den faller, så att man faller mycket snabbare och inte känner att man är på månen - KJ
                rb.gravityScale = ogGravityScale * extraGravity;

                //kollar om spelaren kan hoppa
                //Detta görs genom OverlapBox. Den fungerar genom att jag sätter ut positionen och storleken på en box och sedan kollar den om det finns ett objekt i den boxen
                //Bara objekt som ligger på layern "Solid" kommer kunna hittas. Därför är det viktigt att sätta layern på marken till det. (Layer ligger höger om där tag är på ett objekt)
                // - KJ
                Collider2D collider = Physics2D.OverlapBox(transform.position, new Vector2(boxCol.bounds.extents.x * 2f - 0.2f, boxCol.bounds.extents.y * 2), 0f, maskIndex);

                //Om det fanns en collider i boxen betyder det att spelaren kan hoppa igen - KJ
                if (collider != null)
                {
                    hasJumped = false;
                    anim.SetBool("isGrounded", true);
                }
                else
                {
                    anim.SetBool("isGrounded", false);
                }
            }
            else
            {
                rb.gravityScale = ogGravityScale;
            }


        }

    }
}
