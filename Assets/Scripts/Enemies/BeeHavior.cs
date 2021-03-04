using UnityEngine;

//HHAHHAHAHAHA Get it?
//BeeHavior!!!?!?!??!?
public class BeeHavior : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float horizontalSpeed = 0;
    [SerializeField] private float verticalSpeed = 0;
    [SerializeField,Range(0.1f,2f)] private float verticalRange = 0; 
    private float verticalTime = 0;
    private bool flyUp = false;

    private float startHeight = 0;

    float direction = 1;
    Vector2 newPosition;

    Rigidbody2D rb;
    BoxCollider2D boxCol;

    int maskIndex;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maskIndex = LayerMask.GetMask("Solid");
        boxCol = GetComponent<BoxCollider2D>();

        verticalTime = verticalRange;

        startHeight = transform.position.y;
    }

    
    void Update()
    {
        float moveX = horizontalSpeed * Time.deltaTime * direction;

        float moveY = Mathf.Sin(verticalTime) * verticalRange;

        newPosition = new Vector2(moveX + transform.position.x,moveY + startHeight);

        verticalTime += verticalSpeed * Time.deltaTime;
    }
    void FixedUpdate()
    {
        rb.MovePosition(newPosition);

        //Använder OverlapBox för att kolla om den nuddar en vägg. Viktigt att väggen den nuddar ligger på layern "Solid"
        Collider2D collider = Physics2D.OverlapBox(transform.position + new Vector3((boxCol.bounds.extents.x / 2f) * direction, 0), new Vector2(boxCol.bounds.extents.x, boxCol.bounds.extents.y * 2f - 0.02f), 0f, maskIndex);
        if (collider != null)
        {
            //vänder håll
            direction *= -1;
        }
    }
}
