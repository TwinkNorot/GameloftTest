using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;
    public string type;
    public float hp;
    public int maxHp;
    public float speed;
    public int attack;
    public float armor;

    public Enemy(string Name, string Type, float Hp, int MaxHp, float Speed, int Attack, float Armor)
    {
        name = Name;
        type = Type;
        hp = Hp;
        maxHp = MaxHp;
        speed = Speed;
        attack = Attack;
        armor = Armor;
    }
}
