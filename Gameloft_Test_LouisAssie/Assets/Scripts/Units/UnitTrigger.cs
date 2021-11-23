using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTrigger : MonoBehaviour
{

    public GameObject parent;
    public UnitController unitController;

    // Start is called before the first frame update
    void Awake()
    {
        unitController = parent.GetComponent<UnitController>();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag.Equals(parent.tag))
        {
            unitController.canMove = false;
            unitController.blockingUnit = other.gameObject;
        }
        else if (other.tag.Equals("Ally") || other.tag.Equals("Enemy"))
        {
            unitController.Attack(other.GetComponentInParent<Unit>());
            unitController.blockingUnit = other.gameObject;
            unitController.canMove = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag.Equals("Ally") || other.tag.Equals("Enemy")) && (!other.tag.Equals(parent.tag)))
        {
            unitController.Attack(other.GetComponentInParent<Unit>());
        }
    }
}
