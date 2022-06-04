using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text purpleOrbText;

    [SerializeField]
    private TMP_Text timeText;

    [SerializeField]
    private TMP_Text scoreText;


    private float animationTime;
    bool updating = false;

    ScoreManager.PathCompletionRecord finalRecord;
    // Start is called before the first frame update
    void Start()
    {
        Reset();
        Test();
    }

    // Update is called once per frame
    void Update()
    {
        if (updating)
        {
            AnimationUpdate();
        }
    }
    private void Test()
    {
        TesstScore();
        StartAnimation();
    }

    private void TesstScore()
    {

        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.BOBA);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.BOBA);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.BOBA);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.BOBA);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.BOBA);

        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.JEWELRY);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.JEWELRY);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.JEWELRY);

        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS) ;
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);

        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.PURPLEORB);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.RING);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.RING);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.RING);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.RING);

        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.STATUE);

        ScoreManager.Instance.PathCompleted(100);
        ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.STATUE);
        ScoreManager.Instance.PathCompleted(3);


    }
    void StartAnimation()
    {
        Reset();
        updating = true;
        finalRecord = ScoreManager.Instance.GetSumary();
    }
    void Reset()
    {
        updating = false;
        purpleOrbText.gameObject.SetActive(false);
       // statueText.gameObject.SetActive(false);
        //jewelryText.gameObject.SetActive(false);
        //ringText.gameObject.SetActive(false);
        //bobaText.gameObject.SetActive(false);
        //orbText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        timeText.gameObject.SetActive(false);
    }
    void AnimationUpdate()
    {
        animationTime += Time.deltaTime;
        if(animationTime >= 0 && !purpleOrbText.gameObject.activeSelf)
        {
            purpleOrbText.gameObject.SetActive(true);
        }
        var currentValue = (int)Mathf.Lerp(0, finalRecord.purpleOrbsCollected, animationTime);
        purpleOrbText.text = "Purple Orbs: " + currentValue;
        if (animationTime >= 1 && !timeText.gameObject.activeSelf)
        {
            timeText.gameObject.SetActive(true);
        }

        currentValue = (int)Mathf.Lerp(0, finalRecord.remainingTime, animationTime - 1);
        timeText.text = "Time left: " + currentValue;

        if (animationTime >= 2 && !scoreText.gameObject.activeSelf)
        {
            scoreText.gameObject.SetActive(true);
        }
        currentValue = (int)Mathf.Lerp(0, finalRecord.Score(), animationTime - 2);
        scoreText.text = "Score: " + currentValue;

        /*
        if (animationTime >= 1 && !statueText.gameObject.activeSelf)
        {
            statueText.gameObject.SetActive(true);
        }

        currentValue = (int)Mathf.Lerp(0, finalRecord.StateUnlocked, animationTime - 1);
        statueText.text = "Statues: " + currentValue;

        if (animationTime >= 2 && !jewelryText.gameObject.activeSelf)
        {
            jewelryText.gameObject.SetActive(true);
        }
        currentValue = (int)Mathf.Lerp(0, finalRecord.jewelryCollected, animationTime - 2);
        jewelryText.text = "Jewelry: " + currentValue;

        if (animationTime >= 3 && !ringText.gameObject.activeSelf)
        {
            ringText.gameObject.SetActive(true);
        }
        currentValue = (int)Mathf.Lerp(0, finalRecord.ringCollected, animationTime - 3);
        ringText.text = "Rings: " + currentValue;
        if (animationTime >= 4 && !bobaText.gameObject.activeSelf)
        {
            bobaText.gameObject.SetActive(true);
        }
        currentValue = (int)Mathf.Lerp(0, finalRecord.bobaTeaCollected, animationTime - 4);
        bobaText.text = "Bobas: " + currentValue;

        if (animationTime >= 5 && !orbText.gameObject.activeSelf)
        {
            orbText.gameObject.SetActive(true);
        }
        currentValue = (int)Mathf.Lerp(0, finalRecord.orbsCollected, animationTime - 5);
        orbText.text = "Orbs: " + currentValue;
        if (animationTime >= 6 && !scoreText.gameObject.activeSelf)
        {
            scoreText.gameObject.SetActive(true);
        }
        currentValue = (int)Mathf.Lerp(0, finalRecord.Score(), animationTime - 6);
        scoreText.text = "Score: " + currentValue;
        */
    }
}
