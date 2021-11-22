using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControler : MonoBehaviour
{

    GameManager gameManager;
    public GameObject blockingUnit;

    public Unit stats;

    public int nextWaypoint = 1;

    public bool canMove = true;
    bool canAttack = true;

    // Start is called before the first frame update
    void Awake()
    {
        stats = GetComponent<Unit>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (stats.isAllied)
        {
            enabled = false;
            AllyControler allyControler = GetComponent<AllyControler>();
            allyControler.enabled = true;
            allyControler.nextWaypoint = nextWaypoint - 1;

        }

        if(canMove)
            Move();
        else if (!blockingUnit.activeInHierarchy)
            canMove = true;
    }


    void Move()
    {
        Vector3 dir = gameManager.waypointsList[nextWaypoint].transform.position - transform.position;
        transform.Translate(dir.normalized * stats.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, gameManager.waypointsList[nextWaypoint].transform.position) <= 0.2f)
        {
            if((nextWaypoint + 1) != gameManager.waypointsList.Count) //Just to avoid out of array error
                nextWaypoint++;
        }
    }

    public void Attack(Unit target)
    {
        if (canAttack)
        {
            target.DamageTaken(gameObject);
            StartCoroutine(AttackCooldown());
            canAttack = false;
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(stats.attackSpeed);
        canAttack = true;
    }






    /*void ChangeDestination()
    {
        if(agent.remainingDistance < 0.1f)
        {
            waypoints.Remove(nextWaypoint);
            if (waypoints.Count != 0)
            {
                FindNextWaypoint();
                agent.SetDestination(nextWaypoint.transform.position);
            }
        }
    }*/
}
