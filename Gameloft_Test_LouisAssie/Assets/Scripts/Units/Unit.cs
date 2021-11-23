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

    public void DamageTaken(GameObject damageSource)
    {
        if (damageSource.tag.Equals("Bullet"))
        {
            Bullet bulletStats = damageSource.GetComponent<Bullet>();
            if (bulletStats.damage < -1000000 && !isAllied)
            {
                if (bulletStats.firstSuperiorBuff.Equals("InstaDeath"))
                {
                    isAllied = true;
                    hp = bulletStats.damage / -1000000;
                }
                else
                {
                    hp = 0;
                }
            }
            else if (isAllied)
            {
                if (bulletStats.damage < 0)
                {
                    if (bulletStats.damage < -1000000)
                    {
                        hp = Mathf.Clamp(hp + bulletStats.damage / -1000000, 0, maxHp);
                    }
                    else
                    {
                        hp = Mathf.Clamp(hp + bulletStats.damage, 0, maxHp);
                    }
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
        if(!isAllied)
            GameObject.Find("GameManager").GetComponent<GameManager>().remainingEnemies--;
        gameObject.SetActive(false);
    }

}
