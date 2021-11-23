using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTurretController : MonoBehaviour
{
    GameManager gameManager;

    GameObject target;
    List<GameObject> targetsInRange = new List<GameObject>();

    public GameObject projectilePrefab;
    public GameObject projectileSpawnPoint;
    public BasicTurret stats;

    bool canShoot = true;
    bool targetLock = false;
    bool targetExist = false;

    private void Awake()
    {

        stats = GetComponent<BasicTurret>();
        GetComponent<SphereCollider>().radius = stats.rangeRadius;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {

        if (gameManager.canPlay)
        {
            if (targetExist)
            {
                if (target.activeInHierarchy)
                    TargetTracking();
                else
                    ChangeTarget();
            }

            if (canShoot && targetExist && targetLock)
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
            ChangeTarget();
        else if (other.tag.Equals("Enemy"))
            targetsInRange.Remove(other.gameObject);
    }


    void TargetTracking()
    {
        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * stats.rotationSpeed).eulerAngles;
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
        bullet.GetComponent<Bullet>().Init(stats.rangeRadius, stats.damage, stats.armorPenetration, stats.projectileSpeed, transform.position, stats.superiorBuff.ToString());
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
        yield return new WaitForSeconds(stats.attackSpeed);
        canShoot = true;
    }
}
