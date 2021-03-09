using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    //Hur mycket skada vapnet gör på en fiende - Max
    [Range(1, 100)] public int damage = 10;

    [HideInInspector]
    public Transform playerTr;

    void Update()
    {
        //Sätter vapnets position till att följa efter spelaren - Max
        transform.position = playerTr.position;
    }

    //En funktion för att skada en enda fiende - Max
    public void InflictDamage(EnemyBehavior enemy)
    {
        if (!enemy.allowHit)
        {
            return;
        }

        enemy.healthPoints -= damage;

        if(enemy.healthPoints <= 0)
        {
            enemy.EnemyDeath();
        }
        else
        {
            enemy.EnemyHit();
        }
    }

    //En override till InflictDamage där man kan göra skada till flera enemies samtidigt, detta används av melee-vapen - Max
    public void InflictDamage(Collider2D[] enemies)
    {
        //loopar igenom alla fiender som ska skadas - Max
        foreach(Collider2D enemyCol in enemies)
        {
            EnemyBehavior enemy = enemyCol.GetComponent<EnemyBehavior>();

            //Om fienden är odödlig så ska den inte ta skada - Max
            if (enemy.allowHit)
            {
                //Annars så ska den ta damage - Max
                enemy.healthPoints -= damage;

                //Kollar om fienden har dött och callar en funktion då. Annars callas bara en funktion så att fienden vet att den sakdats - Max
                if (enemy.healthPoints <= 0)
                {
                    enemy.EnemyDeath();
                }
                else
                {
                    enemy.EnemyHit();
                }
            }     
        }
    }

    public void SetActive(bool b)
    {
        //För att kunna stänga av vapnets funktioner ifall det inte är det som används - Max
        GetComponent<MeleeWeaponScript>().enabled = b;
    }
}
