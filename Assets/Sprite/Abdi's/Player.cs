using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Gjorts av Abdiladif 
    // Ingen annan har lagt till ändringar  


    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;// När vi startar spelet kommer vår health var Max
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update() 
    {
        

    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;// subtrahera skador från vår nuvarande current health

        healthBar.SetHealth(currentHealth);// uppdateringar varje gång vi tar skada
    }
}
