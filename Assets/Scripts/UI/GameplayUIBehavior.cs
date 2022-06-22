using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUIBehavior : MonoBehaviour
{

    [Tooltip("The main panel for pausing.")]
    [SerializeField] private GameObject pausePanel;

    [Tooltip("The main panel for the timer.")]
    [SerializeField] private GameObject timerPanel;

    [Tooltip("The main panel for the timer.")]
    [SerializeField] private GameObject scorePanel;

    [Tooltip("The temp text that pops up when the player loses. Will change later")]
    [SerializeField] private GameObject loseText;

    [Tooltip("The temp text that pops up when the player wins. Will change later")]
    [SerializeField] private GameObject winText;

    [Tooltip("The temp text that pops up when the player collects a collectiable")]
    [SerializeField] private GameObject linkNumText;
    [SerializeField] private GameObject linkText;

    [SerializeField]
    private GameObject fractionSlider;

    [SerializeField]
    private GameObject fractionText;


    [Tooltip("Is the game pause?")]
    [SerializeField] private bool paused;

    private bool gameStarted;

    private bool gameWon;


    [Tooltip("The total amount of time.")]
    [SerializeField] private float timeTotal;

    public float TimeTotal { get { return timeTotal; } set { } }

    private float timeLeft;

    public float TimeLeft { get { return timeLeft; } set { } }

    private float finalTime;

    [Tooltip("The total spped of adding score.")]
    [SerializeField] private float scoreSpeed;


    private string timeText;

    private string scoreText;

    private int orb;
    public int orbMax { get; set; }

    [Tooltip("The amount of time the checkpoint display is up")]
    [SerializeField]private float displayTime;

    [Tooltip("The text object for displaying checkpoint time")]
    [SerializeField]private GameObject previousCheckpointTime;

    public static GameplayUIBehavior Instance
    {
        get
        {
            instance = GameObject.FindObjectOfType<GameplayUIBehavior>();
            if (instance == null)
            {

                GameObject a = new GameObject("UIBehaviorManager");
                a.AddComponent<GameplayUIBehavior>();
                instance = a.GetComponent<GameplayUIBehavior>();

            }
            return instance;
        }
    }

    static GameplayUIBehavior instance;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeTotal;
        timeText = "Time: " + timeTotal;
        timerPanel.GetComponentInChildren<TMP_Text>().text = timeText;
        fractionSlider.GetComponent<Slider>().value = 0;
        linkText.SetActive(false);
        linkNumText.SetActive(false);
        if(Time.timeScale == 0)
        {
            //Fix BUG: Start of the game is always playing
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Stop") || Input.GetButtonDown("Stop 2"))
        {
            PauseUnpause();
        }


        if (!paused && gameStarted)
        {
            UpdateTheTimer();
            scoreText = "Score: " + ScoreManager.Instance.GetCurrentScore();
            scorePanel.GetComponentInChildren<TMP_Text>().text = scoreText;
        }
    }

    public void StartTheGame()
    {
        gameStarted = true;
        timeLeft = timeTotal;

    }
    public void OnSamePathRepeated()
    {
        gameStarted = true;
    }

    public void showLinkNumber(bool show)
    {
        if (show)
        {
            linkNumText.SetActive(true);
            linkText.SetActive(true);
            linkNumText.GetComponent<TMP_Text>().text = GameplayManager.Instance.LinkCount.ToString();
        }
        else
        {
            linkNumText.SetActive(false);
            linkText.SetActive(false);
        }
    }


    public void PauseUnpause()
    {
        paused = !paused;

        if (paused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }


    private void UpdateTheTimer()
    {
        if (timeLeft > 0)
        {
            float x = (float)Math.Round((timeLeft -= Time.deltaTime), 0);
            timeText = "Time: " + x;
        }
        else
        {
            gameStarted = false;
            loseText.SetActive(true);
        }

        timerPanel.GetComponentInChildren<TMP_Text>().text = timeText;
    }

    private void UpdateOrbUI()
    {
        string textTemp = "Orbs collected = " + orb + "/" + orbMax;
        
        if(orbMax <= 0)
        {
            textTemp = "Al; Collected";
            fractionSlider.GetComponent<Slider>().value = 1;
        }
        else if (orb == 0)
        {
            fractionSlider.GetComponent<Slider>().value = 0;
        }
        else if(orb <= orbMax)
        {
            fractionSlider.GetComponent<Slider>().value += (1/orbMax);
        }

        fractionText.GetComponent<TMP_Text>().text = textTemp;
    }

    public void YouWin()
    {
        gameStarted = false;
        gameWon = true;
        winText.SetActive(true);
        finalTime = timeLeft;
        //timeLeft = 0;
        scorePanel.GetComponentInChildren<TMP_Text>().text = scoreText;
    }


    public int GetOrb()
    {
        return orb;
    }

    public void SetOrb(int orbTotal)
    {
        if(orbTotal == 0)
        {
            orb = orbTotal;
        }
        else
        {
            orb++;
        }
        
        UpdateOrbUI();
    }

    public void SetMaxOrb(int current)
    {
        orbMax -= current;
        SetOrb(0);
        UpdateOrbUI();
    }


    public float getTime()
    {
        return timeLeft;
    }

    public void setTime(float time)
    {
        this.timeLeft += time;
    }

    /// <summary>
    /// Breaks up the time passed through to display hours, minutes, seconds, and milliseconds since the last checkpoint.
    /// </summary>
    /// <param name="timeToDisplay">The time passed through between the previous and current time</param>
    public void UpdateTime(double timeToDisplay)
    {
        // Breaks down the time
        int hours = (int)(timeToDisplay / 6000);
        timeToDisplay -= hours * 6000;
        int min = (int)(timeToDisplay / 60);
        timeToDisplay -= min * 60;
        int seconds = (int)(timeToDisplay / 1);
        timeToDisplay -= seconds;
        // Final display that keeps being built
        string display = "";
        // The sections that get the time places 
        string next = "";
        if(hours >= 10)
        {
            next = hours.ToString();
        }
        else
        {
            next = "0" + hours.ToString();
        }
        display += next + ":";
        if(min >= 10)
        {
            next = min.ToString();
        }
        else
        {
            next = "0" + min.ToString();
        }
        display += next + ":";
        if(seconds >= 10)
        {
            next = seconds.ToString();
        }
        else
        {
            next = "0" + seconds.ToString();
        }
        display += next + ":";
        int check = (int)(timeToDisplay * 1000);
        next = "";
        if(check < 100)
        {
            next = "0";
        }
        if(check < 10)
        {
            next += "0";
        }
        if(check == 0)
        {
            next += "0";
        }
        else
        {
            next += check;
        }
        display += next;
        previousCheckpointTime.SetActive(true);
        previousCheckpointTime.GetComponent<TMP_Text>().text = display;
        Invoke("TurnOffCheckPoint", displayTime);
    }

    /// <summary>
    /// After the display time has passed, turns it off.
    /// </summary>
    void TurnOffCheckPoint()
    {
        previousCheckpointTime.gameObject.SetActive(false);
    }
}
