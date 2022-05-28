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


    [Tooltip("Is the game pause?")]
    [SerializeField] private bool paused;

    private bool gameStarted;


    [Tooltip("The total amount of time.")]
    [SerializeField] private float timeTotal;

    private float timeLeft;


    private string timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeTotal;
        timeText = "Time: 120";
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

        timerPanel.GetComponentInChildren<TMP_Text>().text = timeText;
    }
}
