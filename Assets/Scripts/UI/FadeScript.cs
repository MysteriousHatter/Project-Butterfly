using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup myUIGroup => GetComponent<CanvasGroup>();
    public bool fadeOut { get; set; }
    private GameplayUIBehavior gameplayUI => FindObjectOfType<GameplayUIBehavior>();

    private GameplayManager gameplayManager => FindObjectOfType<GameplayManager>();


    private void Start()
    {
        ShowUI();
        fadeOut = true;
        gameplayManager.enabled = false;
        gameplayUI.enabled = false;

    }

    public void ShowUI()
    {
        myUIGroup.alpha = 1f;
    }

    public void HideUI()
    {
        myUIGroup.alpha = 0f;
    }


    private void Update()
    {
        if(fadeOut)
        {
            if(myUIGroup.alpha >= 0f)
            {
                myUIGroup.alpha -= Time.deltaTime;
                if(myUIGroup.alpha == 0f)
                {
                    gameplayManager.enabled = true;
                    gameplayUI.enabled = true;
                    HideUI();
                }
            }
        }
    }
}
