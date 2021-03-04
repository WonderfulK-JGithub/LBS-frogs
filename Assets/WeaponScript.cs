using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    [Range(1, 100)] public int damage = 10;

    public Transform playerTr;

    void Update()
    {
        transform.position = playerTr.position;
    }

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

    public void InflictDamage(Collider2D[] enemies)
    {
        foreach(Collider2D enemyCol in enemies)
        {
            EnemyBehavior enemy = enemyCol.GetComponent<EnemyBehavior>();

            if (enemy.allowHit)
            {
                enemy.healthPoints -= damage;

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
        GetComponent<MeleeWeaponScript>().enabled = b;
    }
}
