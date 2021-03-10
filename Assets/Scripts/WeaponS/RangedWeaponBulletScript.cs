using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangedWeaponBulletScript : MonoBehaviour
{
    public CollisionEvent collide = new CollisionEvent();

    public void OnCollisionEnter2D(Collision2D collision)
    {
        collide.Invoke(collision);
    }
}

[System.Serializable]
public class CollisionEvent : UnityEvent<Collision2D>
{
}
