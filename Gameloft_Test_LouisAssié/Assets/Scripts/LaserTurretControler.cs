using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretControler : MonoBehaviour
{
    public LayerMask laserTurretMask;
    public LayerMask defaultMask;

    public bool isSelected = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Selection();

        if (isSelected)
            TurretRotation();
    }

    void Selection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, laserTurretMask))
            {
                
                if (hit.transform.gameObject.Equals(gameObject) && !isSelected)
                    isSelected = true;
                else
                    isSelected = false;
            }
        }
    }


    void TurretRotation()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, defaultMask, QueryTriggerInteraction.Ignore))
            {
                Vector3 mousePos = hit.point;
                mousePos.y = 0;

                transform.LookAt(mousePos);
                
            }
        }
    }

}
