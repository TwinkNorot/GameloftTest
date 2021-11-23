using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTileControler : MonoBehaviour
{
    public Material selectedMaterial;
    Material unSelectedMaterial;


    public GameManager gameManager;

    public GameObject canvas;
    public GameObject turretPrefab;
    public LayerMask buildableTileTurretMask;

    int consecutiveClicks;

    public bool isSelected = false;
    bool selectable = true;

    Touch touch;

    // Start is called before the first frame update
    void Start()
    {
        unSelectedMaterial = GetComponent<MeshRenderer>().material;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.canPlay)
        {
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (selectable && gameManager.canSelectTile)
                    Selection();
            }
        }
    }

    void Selection()
    {
        if(touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, buildableTileTurretMask) && gameManager.canSelectTile)
            {

                if (hit.transform.gameObject.Equals(gameObject) && !isSelected)
                {
                    isSelected = true;
                    canvas.SetActive(true);
                    gameManager.canSelectTurret = false;
                    GetComponent<MeshRenderer>().material = selectedMaterial;
                }
                else if(isSelected) //Checking if the tapped tile is the selected one to avoid to activate turret selection when swapping tile
                {
                    gameManager.canSelectTurret = true;
                    isSelected = false;
                    canvas.SetActive(false);
                    GetComponent<MeshRenderer>().material = unSelectedMaterial;
                }
            }
            else if(Physics.Raycast(ray, out hit, 100, 5)) //if we tap anywhere else on the screen
            {
                if (!hit.transform.gameObject.tag.Equals("UIBackground") && isSelected)
                {
                    gameManager.canSelectTurret = true;
                    isSelected = false;
                    canvas.SetActive(false);
                    GetComponent<MeshRenderer>().material = unSelectedMaterial;
                }
            }
        }
    }

    public void BuildTurret()
    {
        if(isSelected)
        {
            Instantiate(turretPrefab, transform.position, transform.rotation);
            selectable = false;
            isSelected = false;
            canvas.SetActive(false);
            gameManager.canSelectTurret = true;
            GetComponent<MeshRenderer>().material = unSelectedMaterial;
        }
    }
}
