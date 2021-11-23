using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float range;
    public float damage;
    public float armorPenetration;
    public float projectileSpeed;
    public Vector3 parentPosition;
    public string firstSuperiorBuff = "None";

    public void Init(float Range, float Damage, float ArmorPenetration, float ProjectileSpeed, Vector3 ParentPosition, string FirstSuperiorBuff)
    {
        range = Range;
        damage = Damage;
        armorPenetration = ArmorPenetration;
        projectileSpeed = ProjectileSpeed;
        parentPosition = ParentPosition;
        firstSuperiorBuff = FirstSuperiorBuff;

        if (firstSuperiorBuff.Equals("Healing"))
        {
            damage *= -1;
        }
        else if(firstSuperiorBuff.Equals("InstaDeath"))
        {
            damage *= 1000000;
        }

    }
}
