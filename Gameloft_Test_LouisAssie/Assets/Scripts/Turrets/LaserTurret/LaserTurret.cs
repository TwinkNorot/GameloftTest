using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : MonoBehaviour
{
    string name;
    public enum Type {Damage, Speed, Range, Healing, InstaDeath}
    public Type buffType;
    public float range;
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
                    if (bulletStats.firstSuperiorAugment.Equals("none"))
                        bulletStats.firstSuperiorAugment = "Healing";
                    break;
                case Type.InstaDeath:
                    bulletStats.damage *= 1000000;
                    if (bulletStats.firstSuperiorAugment.Equals("none"))
                        bulletStats.firstSuperiorAugment = "InstaDeath";
                    break;
            }
        }
        else //else can only be Turret
        {
            BasicTurret turretStats = BuffTarget.GetComponentInParent<BasicTurret>(); //GetComponentInParent because we have detect the body of the turret
            switch (buffType)
            {
                case Type.Damage:
                    turretStats.damage *= bonusStrengh;
                    break;
                case Type.Speed:
                    turretStats.attackSpeed /= bonusStrengh;
                    turretStats.projectileSpeed *= bonusStrengh;
                    turretStats.rotationSpeed *= bonusStrengh;
                    break;
                case Type.Range:
                    turretStats.rangeRadius *= bonusStrengh;
                    BuffTarget.GetComponent<SphereCollider>().radius = turretStats.rangeRadius;
                    break;
                case Type.Healing:
                    turretStats.damage *= -bonusStrengh;
                    break;
                case Type.InstaDeath:
                    turretStats.damage *= 1000000;
                    break;
            }
        }
    }

    public void CancelBuff(GameObject BuffedTarget)
    {
        BasicTurret turretStats = BuffedTarget.GetComponentInParent<BasicTurret>(); //GetComponentInParent because we have detected the body of the turret
        switch (buffType)
        {
            case Type.Damage:
                turretStats.damage /= bonusStrengh;
                break;
            case Type.Speed:
                turretStats.attackSpeed *= bonusStrengh;
                turretStats.projectileSpeed /= bonusStrengh;
                turretStats.rotationSpeed /= bonusStrengh;
                break;
            case Type.Range:
                turretStats.rangeRadius /= bonusStrengh;
                BuffedTarget.GetComponent<SphereCollider>().radius = turretStats.rangeRadius;
                break;
            case Type.Healing:
                turretStats.damage /= -bonusStrengh;
                break;
            case Type.InstaDeath:
                turretStats.damage /= 1000000;
                break;
        }
    }

}
