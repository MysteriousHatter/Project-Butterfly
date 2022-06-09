using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas ScoreCanvas;
    public static UIManager Instance
    {
        get
        {
            instance = GameObject.FindObjectOfType<UIManager>();
            if (instance == null)
            {

                GameObject a = new GameObject("a");
                a.AddComponent<UIManager>();
                instance = a.GetComponent<UIManager>();

            }
            return instance;
        }
    }

    private static UIManager instance;
    // Start is called before the first frame update
    void Start()
    {
        ScoreCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetScoreCanvas(bool active)
    {
        ScoreCanvas.gameObject.SetActive(active);
    }
}
