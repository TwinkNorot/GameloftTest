using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    public enum Type {Damage, Speed, Range, Healing, InstaDeath}
    public Type buffType;
    public float bonusStrengh;

    public void ApplyBuff(GameObject BuffTarget)
    {
        if (BuffTarget.tag.Equals("Bullet"))
        {
            Bullet bulletStats = BuffTarget.GetComponent<Bullet>();
            switch (buffType)
            {
                case Type.Damage:
                    bulletStats.damage *= bonusStrengh;
                    break;
                case Type.Speed:
                    bulletStats.projectileSpeed *= bonusStrengh;
                    break;
                case Type.Range:
                    bulletStats.range *= bonusStrengh;
                    break;
                case Type.Healing:
                    bulletStats.damage *= -bonusStrengh;
                    if (bulletStats.firstSuperiorBuff.Equals("None"))
                        bulletStats.firstSuperiorBuff = "Healing";
                    break;
                case Type.InstaDeath:
                    bulletStats.damage *= 1000000;
                    if (bulletStats.firstSuperiorBuff.Equals("None"))
                        bulletStats.firstSuperiorBuff = "InstaDeath";
                    break;
            }
        }
        else //else can only be Turret
        {
            BasicTurretController turretStats = BuffTarget.GetComponentInParent<BasicTurretController>(); //GetComponentInParent because we have detected the body of the turret
            switch (buffType)
            {
                case Type.Damage:
                    turretStats.stats.damage *= bonusStrengh;
                    break;
                case Type.Speed:
                    turretStats.stats.attackSpeed /= bonusStrengh;
                    turretStats.stats.projectileSpeed *= bonusStrengh;
                    turretStats.stats.rotationSpeed *= bonusStrengh;
                    break;
                case Type.Range:
                    turretStats.stats.rangeRadius *= bonusStrengh;
                    turretStats.GetComponent<SphereCollider>().radius = turretStats.stats.rangeRadius;
                    break;
                case Type.Healing:
                    turretStats.stats.damage *= -bonusStrengh;
                    turretStats.stats.superiorBuff = BasicTurret.SuperiorBuff.Healing;
                    break;
                case Type.InstaDeath:
                    turretStats.stats.damage *= 1000000;
                    turretStats.stats.superiorBuff = BasicTurret.SuperiorBuff.InstaDeath;
                    break;
            }
        }
    }

    public void CancelBuff(GameObject BuffedTarget)
    {
        BasicTurretController turretStats = BuffedTarget.GetComponentInParent<BasicTurretController>(); //GetComponentInParent because we have detected the body of the turret
        switch (buffType)
        {
            case Type.Damage:
                turretStats.stats.damage /= bonusStrengh;
                break;
            case Type.Speed:
                turretStats.stats.attackSpeed *= bonusStrengh;
                turretStats.stats.projectileSpeed /= bonusStrengh;
                turretStats.stats.rotationSpeed /= bonusStrengh;
                break;
            case Type.Range:
                turretStats.stats.rangeRadius /= bonusStrengh;
                turretStats.GetComponent<SphereCollider>().radius = turretStats.stats.rangeRadius;
                break;
            case Type.Healing:
                turretStats.stats.damage /= -bonusStrengh;
                turretStats.stats.superiorBuff = BasicTurret.SuperiorBuff.None;
                break;
            case Type.InstaDeath:
                turretStats.stats.damage /= 1000000;
                turretStats.stats.superiorBuff = BasicTurret.SuperiorBuff.None;
                break;
        }
    }

}
