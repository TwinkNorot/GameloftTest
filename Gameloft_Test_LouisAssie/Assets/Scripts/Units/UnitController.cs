using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{

    GameManager gameManager;
    public UnitTrigger unitTrigger;
    public GameObject body;
    public GameObject blockingUnit;
    public Image healthBar;

    public Unit stats;

    public int nextWaypoint = 1;

    public bool canMove = true;
    public bool isFighting = false;
    bool canAttack = true;
    bool hasChangedSide = false;

    // Start is called before  the first frame update
    void Awake()
    {
        stats = GetComponent<Unit>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        unitTrigger = GetComponentInChildren<UnitTrigger>();
    }

    void Update()
    {
        if (gameManager.canPlay)
        {
            if (stats.isAllied && !hasChangedSide)
            {
                nextWaypoint = nextWaypoint - 1;
                gameObject.tag = "Ally";
                unitTrigger.tag = "Ally";
                body.tag = "Ally";
                gameManager.remainingEnemies--;
                hasChangedSide = true;

            }

            if (canMove && !isFighting)
                Move();
            else if (!blockingUnit.activeInHierarchy)
            {
                canMove = true;
                isFighting = false;
            }
            else if (blockingUnit.tag.Equals(gameObject.tag))
                AvoidBlocking();

            healthBar.fillAmount = stats.hp / stats.maxHp;
        }
    }


    void Move()
    {
        Vector3 dir = gameManager.waypointsList[nextWaypoint].transform.position - transform.position;
        transform.Translate(dir.normalized * stats.speed * Time.deltaTime, Space.World);
        body.transform.LookAt(gameManager.waypointsList[nextWaypoint].transform.position);

        if (Vector3.Distance(transform.position, gameManager.waypointsList[nextWaypoint].transform.position) <= 0.2f)
        {
            if (!stats.isAllied)
            {
                if ((nextWaypoint + 1) != gameManager.waypointsList.Count) //Just to avoid out of array error
                    nextWaypoint++;
            }
            else if (stats.isAllied)
            {
                if ((nextWaypoint) != 0) //Just to avoid out of array error
                    nextWaypoint--;
            }
        }
    }

    public void Attack(Unit target)
    {
        if (canAttack)
        {
            target.DamageTaken(gameObject);
            StartCoroutine(AttackCooldown());
            canAttack = false;
            if (!target.gameObject.activeInHierarchy)
                isFighting = false;
        }
    }

    void AvoidBlocking()
    {
        if (blockingUnit.GetComponentInParent<UnitController>().nextWaypoint < nextWaypoint) //meaning this object is the first in the line so it can move
        {
            canMove = true;
        }
        else if (blockingUnit.GetComponentInParent<UnitController>().nextWaypoint != nextWaypoint)
        {
            if (Vector3.Angle(gameManager.waypointsList[nextWaypoint].transform.position, new Vector3(blockingUnit.transform.position.x, 0, blockingUnit.transform.position.z)) > 5 && !isFighting) //meaning this object is the frist in the line so it can move
            {
                canMove = true;
            }
        }
        else 
        {
            if(Vector3.Distance(blockingUnit.transform.position, gameManager.waypointsList[nextWaypoint].transform.position) > Vector3.Distance(transform.position, gameManager.waypointsList[nextWaypoint].transform.position)) //meaning this object is the frist in the line so it can move
                canMove = true;
            else if (Vector3.Distance(blockingUnit.transform.position, gameManager.waypointsList[nextWaypoint].transform.position) + 1.5f < Vector3.Distance(transform.position, gameManager.waypointsList[nextWaypoint].transform.position)) //meaning this object is the frist in the line so it can move
                canMove = true;
        }

        if (isFighting)
        {
            if (!blockingUnit.activeInHierarchy)
                isFighting = false;
        }

    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(stats.attackSpeed);
        canAttack = true;
    }
}
