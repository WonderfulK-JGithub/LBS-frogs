using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeWeaponScript : MonoBehaviour
{
    public enum TypeOfAttack { Slash, Stab };
    public TypeOfAttack typeOfSwing;

    public bool canAttack = true;

    public LayerMask layerMask;

    Vector2 transformVector2;

    BoolClass drawTopBox = new BoolClass();
    public Vector2 topBoxPosOffset;
    public Vector2 topBoxSize;

    BoolClass drawFrontBox = new BoolClass();
    public Vector2 frontBoxPosOffset;
    public Vector2 frontBoxSize;

    WeaponScript weaponScript;

    private void Start()
    {
        weaponScript = GetComponent<WeaponScript>();
    }

    private void Update()
    {
        transformVector2 = new Vector2(transform.position.x, transform.position.y);

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (drawTopBox != null && drawTopBox.drawBox)
        {
            Gizmos.DrawCube(transformVector2 + topBoxPosOffset, topBoxSize);
        }

        if (drawFrontBox != null && drawFrontBox.drawBox)
        {
            Gizmos.DrawCube(transformVector2 + frontBoxPosOffset, frontBoxSize);
        }
    }

    public IEnumerator Attack()
    {
        canAttack = false;
        if (typeOfSwing == TypeOfAttack.Slash)
        {
            drawTopBox.drawBox = true;
            StartCoroutine(TurnOffBool(0.3f, drawTopBox));
            while(drawTopBox.drawBox == true)
            {
                Collider2D[] results = Physics2D.OverlapBoxAll(transformVector2 + topBoxPosOffset, topBoxSize, 0f, layerMask);
                weaponScript.InflictDamage(results);
                yield return null;
            }

            drawFrontBox.drawBox = true;
            StartCoroutine(TurnOffBool(0.2f, drawFrontBox));
            while (drawFrontBox.drawBox == true)
            {
                Collider2D[] results = Physics2D.OverlapBoxAll(transformVector2 + frontBoxPosOffset, frontBoxSize, 0f, layerMask);
                weaponScript.InflictDamage(results);
                yield return null;
            }
        }
        else if (typeOfSwing == TypeOfAttack.Stab)
        {

        }
        canAttack = true;
    }

    IEnumerator TurnOffBool(float time, BoolClass boolClass)
    {
        yield return new WaitForSeconds(time);
        boolClass.drawBox = false;
    }
}

public class BoolClass
{
    public bool drawBox = false;
}
