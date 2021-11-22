using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public string damageType;
    public float range;
    public float damage;
    public float armorPenetration;
    public float projectileSpeed;
    public Vector3 parentPosition;
    public string firstSuperiorAugment = "none";

    public void Init(string DamageType, float Range, float Damage, float ArmorPenetration, float ProjectileSpeed, Vector3 ParentPosition)
    {
        damageType = DamageType;
        range = Range;
        damage = Damage;
        armorPenetration = ArmorPenetration;
        projectileSpeed = ProjectileSpeed;
        parentPosition = ParentPosition;
    }
}
