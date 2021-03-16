using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeWeaponScript : MonoBehaviour
{
    [Header("General")]
    public LayerMask layerMask;

    public bool canAttack = true;

    [Header("Attack")]
    public List<HitBox> HitBoxes = new List<HitBox>();

    public float timeTilNextAttack;

    //Local Variables:

    Vector2 transformVector2;

    List<BoolClass> drawBox = new List<BoolClass>();

    WeaponScript weaponScript;
    PlayerMovement player;


    private void Start()
    {
        weaponScript = GetComponent<WeaponScript>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();

        //Skapar nya instances av BoolClass till varje hitbox - Max
        for (int i = 0; i < HitBoxes.Count; i++)
        {
            drawBox.Add(new BoolClass());
        }
    }

    private void Update()
    {
        //Det behövs en vector2-version av transform.position - Max
        transformVector2 = new Vector2(transform.position.x, transform.position.y);

        //Om spelaren attackerar - Max
        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    public IEnumerator Attack()
    {
        //Spelaren ska inte kunna attackera om han redan attackerar, det hade blivit konstigt - Max
        canAttack = false;
        //ska inte heller kunna vända sig om mitt i en attack - Max
        player.canFlip = false;
        player.anim.SetTrigger("attack");

        for (int i = 0; i < HitBoxes.ToArray().Length; i++)
        {
            //Sätter på så att boxen drawas i OnDrawGizmos - Max
            drawBox[i].drawThisBox = true;
            //sätter en timer tills hitboxen ska försvinna igen - Max
            StartCoroutine(TurnOffBool(HitBoxes[i].time, drawBox[i]));
            while (drawBox[i].drawThisBox == true)
            {
                //Skapar en overlapbox - Max
                Collider2D[] results = Physics2D.OverlapBoxAll(transformVector2 + new Vector2(HitBoxes[i].position.x * (player.isFlipped ? -1 : 1), HitBoxes[i].position.y), HitBoxes[i].size, 0f, layerMask);
                //Gör damage till alla fiender i hitboxen - Max
                weaponScript.InflictDamage(results);
                yield return null;
            }
            drawBox[i].drawThisBox = false;
        }

        //Sätter på så man kan attackera och vända sig igen - Max

        yield return new WaitForSeconds(timeTilNextAttack);

        canAttack = true;
        player.canFlip = true;
    }

    IEnumerator TurnOffBool(float time, BoolClass boolClass)
    {
        //Detta är bara en timer för att stänga av en bool - Max
        //anledningen till varför det är en class med en bool istället för bara en bool är
        //för att med en class får man en referens till just den boolen som jag vill ändra och kan ändra just den - Max
        yield return new WaitForSeconds(time);
        boolClass.drawThisBox = false;
    }

    private void OnDrawGizmos()
    {
        //Här drawas hitboxesarna så att man kan se dem i unity - Max
        Gizmos.color = Color.red;

        for (int i = 0; i < drawBox.Count; i++)
        {
            if (drawBox[i] != null && drawBox[i].drawThisBox)
            {
                Gizmos.DrawCube(transformVector2 + new Vector2(HitBoxes[i].position.x * (player.isFlipped ? -1 : 1), HitBoxes[i].position.y), HitBoxes[i].size);
            }
        }
    }   
}

public class BoolClass
{
    //En class som bara innehåller en bool - Max
    public bool drawThisBox = false;
}


[System.Serializable]
public struct HitBox
{
    //Varje hitbox har lite olika properties för att spelet ska evta vad som ska göras med dem - Max
    public Vector2 position;
    public Vector2 size;
    public float time;

    //Den här constructorn är bara nödvändig om man vill ändra en hitbox på runtime - Max
    public HitBox(Vector2 position, Vector2 size, float time)
    {
        this.position = position;
        this.size = size;
        this.time = time;
    }
}
