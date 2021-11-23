using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserTurretControler : MonoBehaviour
{
    GameManager gameManager;


    
    public GameObject turretBase;
    public Material selectedMaterial;
    public GameObject canvas;
    Material unSelectedMaterial;

    public LayerMask laserTurretMask;
    public LayerMask defaultMask;

    public bool isSelected = false;

    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        unSelectedMaterial = turretBase.GetComponent<MeshRenderer>().material;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            Selection();
            if (gameManager.canPlay)
            {
                if (isSelected)
                    TurretRotation();
            }
        }
        
    }

    void Selection()
    {
        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, laserTurretMask) && gameManager.canSelectTurret)
            {
                if (hit.transform.gameObject.Equals(gameObject) && !isSelected)
                {
                    isSelected = true;
                    StartCoroutine(Delay());
                    turretBase.GetComponent<MeshRenderer>().material = selectedMaterial;
                    canvas.SetActive(true);
                }
                else if(isSelected) //Checking if the tapped turret is the selected one to avoid to activate tile selection when swapping turret
                {
                    gameManager.canSelectTile = true;
                    isSelected = false;
                    turretBase.GetComponent<MeshRenderer>().material = unSelectedMaterial;
                    canvas.SetActive(false);
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

    IEnumerator Delay() //We have to wait for the end of frame to be sure that the canSelecTile bool is not overwritten when swapping turrer
    {
        yield return new WaitForEndOfFrame();
        gameManager.canSelectTile = false;
    }

}
