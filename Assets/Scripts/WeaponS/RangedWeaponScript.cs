using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeaponScript : MonoBehaviour
{
    [Header("Properties")]

    public BulletsPerShot bulletsPerShot;
    [Range(0, 180)] public int spread;  

    [Range(0, 100)] public float bulletSpeed = 10f;

    public float timeTilNextShot;

    public bool automatic = false;

    [Header("Debug")]
    public GameObject bullet;

    public bool canAttack = true;

    //Privata variablar
    WeaponScript weaponScript;
    PlayerMovement player;

    private void Start()
    {
        weaponScript = GetComponent<WeaponScript>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (automatic)
        {
            if (Input.GetButton("Fire1") && canAttack)
            {
                StartCoroutine(Fire());
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && canAttack)
            {
                StartCoroutine(Fire());
            }
        }
        
    }

    public IEnumerator Fire()
    {
        canAttack = false;
        player.canFlip = false;

        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
        int bulletAmount = UnityEngine.Random.Range(bulletsPerShot.min, bulletsPerShot.max + 1);
        for (int i = 0; i < bulletAmount; i++)
        {
            UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
            Vector3 rotation = new Vector3(0, 0, (UnityEngine.Random.Range(-spread, spread)) * (player.isFlipped ? -1 : 1));
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.Euler(rotation));
            newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.right * bulletSpeed * (player.isFlipped ? -1 : 1);
            newBullet.GetComponent<RangedWeaponBulletScript>().collide.AddListener(Damage);
            Destroy(newBullet, 5);
            print(rotation);
        }

        yield return new WaitForSeconds(timeTilNextShot);

        canAttack = true;
        player.canFlip = true;
    }

    public void Damage(Collision2D collision)
    {
        if(collision.collider.gameObject.layer == 9)
        {
            weaponScript.InflictDamage(collision.collider.gameObject.GetComponent<EnemyBehavior>());
            Destroy(collision.otherCollider.gameObject);
        }
        else
        {
            Destroy(collision.otherCollider.gameObject);
        }
    }
}

[System.Serializable]
public struct BulletsPerShot
{
    public int max;
    public int min;
}
