using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    //Håller kåll så att fienden inte ramlar ner
    public float distance;
    
    private bool movingRight = true;
    //Håller kåll på om det finns mark framför sig.
    public Transform groundDetection;

    void Update()
    {
        //Hur fungerar detta makalösa script? Tänk dig att det finns en onsynlig pinne framför spelaren några millimeter bort. 
        //Denna pinne håller kåll på om det finns mark som fienden kan gå på. Om det finns, så ska fienden fortsätta gå. Men om det inte finns någon mark, så ska fienden vrida sig 180 grader, alltså gå åt vänster.
        //Samma sak gäller när den går åt vänster. När det inte finns någon mark kvar så ska fienden vända och gå till höger.
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distance);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

}
