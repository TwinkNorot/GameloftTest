using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretTrigger : MonoBehaviour
{

    LaserTurret parentScript;

    // Start is called before the first frame update
    void Awake()
    {
        parentScript = GetComponentInParent<LaserTurret>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet") || other.tag.Equals("Turret"))
        {
            parentScript.ApplyBuff(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Turret"))
        {
            parentScript.CancelBuff(other.gameObject);
        }
    }
}
