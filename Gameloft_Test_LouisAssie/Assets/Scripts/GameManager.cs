using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    Text timeDisplayText;
    Text FPSDisplayText;
    Text hpDisplayText;
    GameObject winScreen;
    public GameObject loseScreen;
    GameObject pauseButtons;

    public List<GameObject> waypointsList = new List<GameObject>();
    public GameObject UIBackground;
    public float timeBeforNextWave;
    public int playerLifePoints;
    public int remainingEnemies;
    public bool allWavesSpawend = false;
    public bool canSelectTile = true;
    public bool canSelectTurret = true;
    public bool canPlay = true;

    public int currentLevel;

    float timer;

    // Start is called before the first frame update
    void Start()
    {
        winScreen = GameObject.Find("WinScreen");
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        pauseButtons = GameObject.Find("DuringPauseButtons");
        pauseButtons.SetActive(false);
        timeDisplayText = GameObject.Find("TimeDisplay").GetComponent<Text>();
        FPSDisplayText = GameObject.Find("FPSDisplay").GetComponent<Text>();
        hpDisplayText = GameObject.Find("hpDisplay").GetComponent<Text>();
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
        hpDisplayText.text = "Life : " + playerLifePoints;

        if (canPlay)
        {
            if (playerLifePoints <= 0)
            {
                Lost();
            }
            if (remainingEnemies == 0 && allWavesSpawend)
            {
                Won();
            }
        }

    }

    public void PauseButton()
    {
        if (canPlay)
        {
            canPlay = false;
            pauseButtons.SetActive(true);
        }
        else
        {
            canPlay = true;
            pauseButtons.SetActive(false);
        }
    }

    public void Lost()
    {
        loseScreen.SetActive(true);
        canPlay = false;
    }

    public void Won()
    {
        winScreen.SetActive(true);
        canPlay = false;
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        if(currentLevel < 4)
            SceneManager.LoadScene("LD" + (currentLevel + 1));
        else
            SceneManager.LoadScene("MainMenu");
    }
}
