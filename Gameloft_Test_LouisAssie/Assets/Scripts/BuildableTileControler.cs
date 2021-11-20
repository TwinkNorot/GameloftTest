using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildableTileControler : MonoBehaviour
{

    GameManager gameManager;

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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (selectable && gameManager.canSelectTile)
                Selection();
        }
    }

    void Selection()
    {
        if(touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, buildableTileTurretMask))
            {

                if (hit.transform.gameObject.Equals(gameObject) && !isSelected)
                {
                    isSelected = true;
                    canvas.SetActive(true);
                    gameManager.UIBackground.SetActive(true);
                }
                else
                {
                    isSelected = false;
                    canvas.SetActive(false);
                    gameManager.UIBackground.SetActive(false);
                }
            }
            else if(Physics.Raycast(ray, out hit, 100, 5))
            {
                Debug.Log(hit.transform.gameObject.tag);
                if (!hit.transform.gameObject.tag.Equals("UIBackground"))
                {
                    isSelected = false;
                    canvas.SetActive(false);
                    gameManager.UIBackground.SetActive(false);
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
            gameManager.UIBackground.SetActive(false);
        }
    }
}
