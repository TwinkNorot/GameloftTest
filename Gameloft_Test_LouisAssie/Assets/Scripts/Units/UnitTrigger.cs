using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTrigger : MonoBehaviour
{

    public GameObject parent;
    public AllyControler allyControler;
    public EnemyControler enemyControler;

    // Start is called before the first frame update
    void Awake()
    {
        allyControler = parent.GetComponent<AllyControler>();
        enemyControler = parent.GetComponent<EnemyControler>();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals(parent.tag))
        {
            if (parent.tag.Equals("Ally"))
            {
                allyControler.canMove = false;
                allyControler.blockingUnit = other.gameObject;
            }
            else
            {
                enemyControler.canMove = false;
                enemyControler.blockingUnit = other.gameObject;
            }
        }
        else if (other.tag.Equals("Ally") || other.tag.Equals("Enemy"))
        {
            if (parent.tag.Equals("Ally"))
            {
                allyControler.Attack(other.GetComponentInParent<Unit>());
                allyControler.blockingUnit = other.gameObject;
                allyControler.canMove = false;
            }
            else
            {
                enemyControler.Attack(other.GetComponentInParent<Unit>());
                enemyControler.blockingUnit = other.gameObject;
                enemyControler.canMove = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag.Equals("Ally") || other.tag.Equals("Enemy")) && (!other.tag.Equals(parent.tag)))
        {
            if (parent.tag.Equals("Ally"))
                allyControler.Attack(other.GetComponentInParent<Unit>());
            else
                enemyControler.Attack(other.GetComponentInParent<Unit>());
        }
    }
}
