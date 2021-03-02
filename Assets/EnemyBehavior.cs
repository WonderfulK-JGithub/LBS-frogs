using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //En enumerator som är till för att veta vilken slags fiende det är
    public enum EnemyType
    {
        pig,
        chicken,
        farmer
    }

    [Header("Type of enemy")]
    //variabel för vilken typ av fiende det är
    public EnemyType type;

    [Header("Enemy stats")]

    public int healthPoints;
    [SerializeField] private int damage;

    [Header("Movement")]
    [SerializeField] private float spd;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }
}
