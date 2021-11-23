using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public Text timeDisplayText;
    public Text FPSDisplayText;
    public Text moneyDisplayText;
    public Text hpDisplayText;

    public List<GameObject> waypointsList = new List<GameObject>();
    public GameObject UIBackground;
    public float timeBeforNextWave;
    public int playerLifePoints;
    public int money;
    public bool canSelectTile = true;
    public bool canSelectTurret = true;
    public bool canPlay = true;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int currentFPS = (int)(1f / Time.unscaledDeltaTime);
            FPSDisplayText.text = "FPS : " + currentFPS;
            timer = Time.unscaledTime + 0.25f;
        }
        timeDisplayText.text = "Next in : " + (int)timeBeforNextWave;
        moneyDisplayText.text = "Coins : " + money;
        hpDisplayText.text = "Life : " + playerLifePoints;
    }

    public void PauseButton()
    {
        if (canPlay)
        {
            canPlay = false;
        }
        else
        {
            canPlay = true;
        }
    }
}
