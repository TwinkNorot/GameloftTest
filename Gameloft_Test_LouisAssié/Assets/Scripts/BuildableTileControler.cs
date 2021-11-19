using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTileControler : MonoBehaviour
{

    public GameObject turretPrefab;
    public LayerMask buildableTileTurretMask;
    
    public bool isSelected = false;
    bool selectable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
            BuildTurret();

        if(selectable)
            Selection();
    }

    void Selection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, buildableTileTurretMask))
            {

                if (hit.transform.gameObject.Equals(gameObject) && !isSelected)
                    isSelected = true;
                else
                    isSelected = false;
            }
        }
    }

    void BuildTurret()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(turretPrefab, transform.position, transform.rotation);
            selectable = false;
        }
    }
}
