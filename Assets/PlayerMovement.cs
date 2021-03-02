using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//Den här koden hanterar vänster höger och hopp - KJ
public class PlayerMovement : MonoBehaviour
{
    //Referense till rigidbody - KJ
    Rigidbody2D rb;

    [Header("Force")]
    [SerializeField,Range(1,10)] float verticalSpd;
    [SerializeField,Range(0.1f,10)] public float jumpForce;

    [Header("Jump buffer")]
    [SerializeField,Range(0,1)] float extraTime;
    

    //håller koll på hur länge man tryckt på hopp knappen
    float holdTime;

    //Vector 2 som håller koll på vilken velocity rigidbodyn ska ha - KJ
    Vector2 moveForce;

    bool hasJumped = false;

    //är till för att veta om man håller ner hoppknappen för att hoppa högre - KJ
    bool holdJumpButton = false;

    void Start()
    {
        //hämtar referensen - KJ
        rb = GetComponent<Rigidbody2D>();

        holdTime = extraTime;
    }

    
    void Update()
    {
        print(hasJumped);

        //skaffar input från höger och vänster knappar - KJ
        float vert = Input.GetAxis("Horizontal");

        if(holdJumpButton)
        {
            if(Input.GetButton("Jump"))
            {
                holdTime -= 1 * Time.deltaTime; //tar bort tid från variabel holdTime - KJ

                if(holdTime <= 0)
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
        if(Input.GetButtonDown("Jump") && !hasJumped)
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);//lägger till en hopp kraft -KJ

            hasJumped = true;
            holdJumpButton = true;
        }

        print(moveForce.y);

        moveForce = new Vector2(vert * verticalSpd,rb.velocity.y); //Ser till att den eventuellt rör sig vänster eller höger, men alltid behåller fall - KJ
    }

    void FixedUpdate()
    {
        //sätter rigidbodyns velocity till det den ska vara - KJ
        rb.velocity = moveForce;
    }

    //Ser till att playern kan hoppa igen när han landar på ett objekt med taggen "ground" - KJ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ground"))
        {
            hasJumped = false;
        }
    }
}
