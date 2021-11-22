using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string name;
    public string type;
    public float hp;
    public int maxHp;
    public float speed;
    public int attack;
    public float attackSpeed;
    public float armor;
    public int damageToPlayer;
    public bool isAllied = false;


    public Unit(string Name, string Type, float Hp, int MaxHp, float Speed, int Attack, float AttackSpeed, float Armor, int DamageToPlayer)
    {
        name = Name;
        type = Type;
        hp = Hp;
        maxHp = MaxHp;
        speed = Speed;
        attack = Attack;
        attackSpeed = AttackSpeed;
        armor = Armor;
        damageToPlayer = DamageToPlayer;
    }

    /*public void DamageTaken(Bullet projectileStats)
    {
        if(projectileStats.damage < -1000000)
        {
            if (projectileStats.firstSuperiorAugment.Equals("InstaDeath"))
            {
                isAllied = true;
                hp = projectileStats.damage / -1000000;
            }
            else
            {
                hp = 0;
                Death();
            }
        }
        else
        {
            hp = Mathf.Clamp(hp - (projectileStats.damage - armor + projectileStats.armorPenetration), 0, maxHp);
            if (hp == 0)
                Death();
        }  
    }*/

    public void DamageTaken(GameObject damageSource)
    {
        if (damageSource.tag.Equals("Bullet"))
        {
            Bullet bulletStats = damageSource.GetComponent<Bullet>();
            if (bulletStats.damage < -1000000)
            {
                if (bulletStats.firstSuperiorAugment.Equals("InstaDeath"))
                {
                    isAllied = true;
                    hp = bulletStats.damage / -1000000;
                }
                else
                {
                    hp = 0;
                }
            }
            else
            {
                hp = Mathf.Clamp(hp - (bulletStats.damage - armor + bulletStats.armorPenetration), 0, maxHp);

            }
        }
        else
        {
            Unit attacker = damageSource.GetComponent<Unit>();
            hp = Mathf.Clamp(hp - (attacker.attack - armor), 0, maxHp);
        }



        if (hp == 0)
            Death();
    }

    void Death()
    {
        gameObject.SetActive(false);
    }

}
