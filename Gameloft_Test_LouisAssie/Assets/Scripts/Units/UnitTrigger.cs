using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTrigger : MonoBehaviour
{

    public GameObject parent;
    UnitController unitController;

    // Start is called before the first frame update
    void Awake()
    {
        unitController = parent.GetComponent<UnitController>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!unitController.isFighting)
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
                unitController.isFighting = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.tag.Equals("Ally") || other.tag.Equals("Enemy")))
        {
            unitController.canMove = false;

            if (!other.tag.Equals(parent.tag))
            {
                unitController.Attack(other.GetComponentInParent<Unit>());
                StartCoroutine(Delay(other.gameObject));
            }
        }
    }

    IEnumerator Delay(GameObject target) //To avoid overwrite
    {
        
        yield return new WaitForEndOfFrame();
        unitController.canMove = false;
        unitController.isFighting = true;
        unitController.blockingUnit = target.gameObject;
        unitController.Attack(target.GetComponentInParent<Unit>());
    }
}
