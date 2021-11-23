using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitController : MonoBehaviour
{

    GameManager gameManager;
    public GameObject blockingUnit;
    public Image healthBar;

    public Unit stats;

    public int nextWaypoint = 1;

    public bool canMove = true;
    bool canAttack = true;
    bool hasChangedSide = false;

    // Start is called before  the first frame update
    void Awake()
    {
        stats = GetComponent<Unit>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (gameManager.canPlay)
        {
            if (stats.isAllied && !hasChangedSide)
            {
                nextWaypoint = nextWaypoint - 1;
                gameObject.tag = "Ally";
                GetComponentInChildren<UnitTrigger>().tag = "Ally";
                hasChangedSide = true;

            }

            if (canMove)
                Move();
            else if (!blockingUnit.activeInHierarchy)
                canMove = true;

            healthBar.fillAmount = stats.hp / stats.maxHp;
        }
    }


    void Move()
    {
        Vector3 dir = gameManager.waypointsList[nextWaypoint].transform.position - transform.position;
        transform.Translate(dir.normalized * stats.speed * Time.deltaTime, Space.World);

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
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(stats.attackSpeed);
        canAttack = true;
    }
}
