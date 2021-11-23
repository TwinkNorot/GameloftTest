using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour
{
    public string name;
    [Tooltip("Range of the turret, in units. (Tiles are size 5x5)")]
    public float rangeRadius;
    public float damage;
    public float armorPenetration;
    [Tooltip("Time between each shot, in seconds.")]
    public float attackSpeed;
    [Tooltip("The distance the projectile travel each second, in unit")]
    public float projectileSpeed;
    public float rotationSpeed;
    public enum SuperiorBuff { None, Healing, InstaDeath }
    public SuperiorBuff superiorBuff = SuperiorBuff.None;
}
