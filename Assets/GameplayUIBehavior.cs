using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

        if (gameWon)
        {
            if (timeLeft > 0)
            {
                YouWin();
            }
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
        string text = "Orbs collected = " + orb + "/20";
        print(text);
    }

    public void YouWin()
    {
        gameStarted = false;
        gameWon = true;
        winText.SetActive(true);
        finalTime = timeLeft;

        score += finalTime;

        scoreText = "Score: " + (float)Math.Round((score), 0);

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
        orb = orbTotal;
        UpdateOrbUI();
    }
}
