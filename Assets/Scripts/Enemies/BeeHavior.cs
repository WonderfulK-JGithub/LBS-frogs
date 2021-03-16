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
    

    private float startHeight = 0;

    float direction = 1;
    Vector2 newPosition;

    Rigidbody2D rb;
    BoxCollider2D boxCol;
    EnemyBehavior enemyScr;

    int maskIndex;

    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maskIndex = LayerMask.GetMask("Solid");
        boxCol = GetComponent<BoxCollider2D>();

        enemyScr = GetComponent<EnemyBehavior>();

        verticalTime = verticalRange;

        startHeight = transform.position.y;
    }

    
    void Update()
    {
        
        if(!PauseMenu.GameIsPaused)
        {
            //räknar ut hur mycket den ska flyttas i x - KJ
            float moveX = horizontalSpeed * Time.deltaTime * direction;

            //räknar ut hur vart den ska vara i y
            //Detta är lite annorlunda men Sin gör basically att det blir vågigt. verticalRange bestämmer hur stora vågorna ska vara.
            //Om man skriver detta i t.ex geogebra, vilket då blir: y = Sin(x) * valfri siffra, blir det en bra illustration på hur biet flyger upp och ner - KJ
            float moveY = Mathf.Sin(verticalTime) * verticalRange;

            //bestämmer rigidbodyns nya position
            newPosition = new Vector2(moveX + transform.position.x, moveY + startHeight);

            verticalTime += verticalSpeed * Time.deltaTime;
        }

        //om den dör
        if (enemyScr.isDead)
        {
            //Gör att rb är stilla och disablar detta script
            rb.bodyType = RigidbodyType2D.Static;
            this.enabled = false;
        }

    }
    void FixedUpdate()
    {
        if(!PauseMenu.GameIsPaused)
        {
            //flyttar rigidbodyn till ny position - KJ
            rb.MovePosition(newPosition);

            //Använder OverlapBox för att kolla om den nuddar en vägg. Viktigt att väggen den nuddar ligger på layern "Solid" - KJ
            Collider2D collider = Physics2D.OverlapBox(transform.position + new Vector3((boxCol.bounds.extents.x / 2f) * direction, 0), new Vector2(boxCol.bounds.extents.x, boxCol.bounds.extents.y * 2f - 0.02f), 0f, maskIndex);
            if (collider != null)
            {
                //vänder håll
                direction *= -1;
            }
        }
        
    }
}
