using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigBehavior : MonoBehaviour
{ 
    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 0;

    float direction = 1;
    Vector2 moveForce;

    Rigidbody2D rb;
    BoxCollider2D boxCol;

    int maskIndex;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //skaffar referense till boxCollidern
        boxCol = GetComponent<BoxCollider2D>();

        //skaffar en maskindex från layern "Solid"
        maskIndex = LayerMask.GetMask("Solid");
    }

    private void FixedUpdate()
    {
        moveForce = new Vector2(horizontalSpeed * direction, rb.velocity.y);

        //ger rb velocity
        rb.velocity = moveForce;

        //Använder OverlapBox för att kolla om den nuddar en vägg. Viktigt att väggen den nuddar ligger på layern "Solid"
        Collider2D collider = Physics2D.OverlapBox(transform.position + new Vector3((boxCol.bounds.extents.x / 2f) * direction, 0), new Vector2(boxCol.bounds.extents.x, boxCol.bounds.extents.y * 2f - 0.02f), 0f, maskIndex);
        if (collider != null)
        {
            //vänder håll
            direction *= -1;
        }
    }
}
