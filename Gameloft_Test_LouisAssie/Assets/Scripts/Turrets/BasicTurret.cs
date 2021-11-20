using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurret : MonoBehaviour
{

    public GameObject target;
    public List<GameObject> targetsInRange = new List<GameObject>();

    public GameObject projectilePrefab;
    public GameObject projectileSpawnPoint;

    public string name;
    public string damageType;
    public float rangeRadius;
    public float damage;
    public float armorPenetration;
    public float attackSpeed;
    public float projectileSpeed;
    public float rotationSpeed;
    public bool canShoot = true;
    public bool targetLock = false;
    public bool targetExist = false;



    /*public BasicTurret(string Name, string DamageType, float Range, float Damage, float ArmorPenetration, float AttackSpeed, float ProjectileSpeed)
    {
        name = Name;
        damageType = DamageType;
        range = Range;
        damage = Damage;
        armorPenetration = ArmorPenetration;
        attackSpeed = AttackSpeed;
        projectileSpeed = ProjectileSpeed;
    }*/

    private void Awake()
    {
        GetComponent<SphereCollider>().radius = rangeRadius;
    }

    private void Update()
    {
        

        if (targetExist)
        {
            if (target.activeInHierarchy)
                TargetTracking();
            else
                ChangeTarget();


        }

        if (canShoot && targetExist && targetLock)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Enemy"))
        {
            if (!targetExist)
            {
                target = other.gameObject;
                targetExist = true;
            }
            else
            {
                targetsInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            ChangeTarget();
        }
        else if (other.tag.Equals("Enemy"))
        {
            targetsInRange.Remove(other.gameObject);
        }
    }


    void TargetTracking()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        float angle = Vector3.Angle(target.transform.position - transform.position, transform.forward);

        if (angle < 25.0f)
            targetLock = true;
        else
            targetLock = false;
    }
    

    void Shoot()
    {
        GameObject bullet = Instantiate(projectilePrefab, projectileSpawnPoint.transform.position, projectileSpawnPoint.transform.rotation);
        bullet.GetComponent<Bullet>().Init(damageType, rangeRadius, damage, armorPenetration, projectileSpeed, transform.position);
        canShoot = false;
        StartCoroutine(FireCooldownCoroutine());
    }

    public void ChangeTarget()
    {
        if (targetsInRange.Count == 0)
        {
            targetExist = false;
            targetLock = false;

        }
        else
        {   
            target = targetsInRange[0];
            targetsInRange.Remove(target);
        }
    }

    private IEnumerator FireCooldownCoroutine()
    {
        yield return new WaitForSeconds(attackSpeed);
        canShoot = true;
    }
}
