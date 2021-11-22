using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyControler : MonoBehaviour
{

    GameManager gameManager;
    public GameObject blockingUnit;

    public Unit stats;

    public int nextWaypoint = 1;

    public bool canMove = true;
    bool canAttack = true;




    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<Unit>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameObject.tag = "Ally";
        GetComponentInChildren<UnitTrigger>().tag = "Ally";
    }

    // Update is called once per frame
    void Update()
    {
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
            if ((nextWaypoint) != 0) //Just to avoid out of array error
                nextWaypoint--;
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
