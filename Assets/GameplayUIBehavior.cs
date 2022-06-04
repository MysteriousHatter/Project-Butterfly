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

    private float timeLeft;

    private float finalTime;

    [Tooltip("The total spped of adding score.")]
    [SerializeField] private float scoreSpeed;

    [SerializeField] private float score;

    private string timeText;

    private string scoreText;

    private int orb;

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
        }
    }

    public void StartTheGame()
    {
        gameStarted = true;
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
        string textTemp = "Orbs collected = " + orb + "/20";
        fractionText.GetComponent<TMP_Text>().text = textTemp;
        if (orb == 0)
        {
            fractionSlider.GetComponent<Slider>().value = 0;
        }
        else if(orb <= 20)
        {
            fractionSlider.GetComponent<Slider>().value += 0.05f;
        }
        
        
    }

    public void YouWin()
    {
        gameStarted = false;
        gameWon = true;
        winText.SetActive(true);
        finalTime = timeLeft;
        //timeLeft = 0;
        finalTime = (float)Math.Round((finalTime), 0);
        score += finalTime;

        scoreText = "Score: " + 0;

        scorePanel.GetComponentInChildren<TMP_Text>().text = scoreText;
    }

    public float getScore()
    {
        return score;
    }

    public void setScore(float point)
    {
        scoreText = "Score: " + (this.score += point);

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
}
