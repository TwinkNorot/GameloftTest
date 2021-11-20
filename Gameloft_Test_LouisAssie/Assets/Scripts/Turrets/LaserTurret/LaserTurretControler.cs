using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretControler : MonoBehaviour
{
    GameManager gameManager;

    public LayerMask laserTurretMask;
    public LayerMask defaultMask;

    public bool isSelected = false;

    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Selection();

            if (isSelected)
                TurretRotation();
        }
    }

    void Selection()
    {
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, laserTurretMask))
            {

                if (hit.transform.gameObject.Equals(gameObject) && !isSelected)
                {
                    isSelected = true;
                    gameManager.canSelectTile = false;
                }
                else
                {
                    isSelected = false;
                    gameManager.canSelectTile = true;
                }
            }
        }
    }


    void TurretRotation()
    {
        if (touch.phase == TouchPhase.Moved)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, defaultMask, QueryTriggerInteraction.Ignore))
            {
                Vector3 touchPos = hit.point;
                touchPos.y = 0;

                transform.LookAt(touchPos);
                
            }
        }
    }

}
