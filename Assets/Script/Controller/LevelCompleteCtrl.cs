using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelCompleteCtrl : MonoBehaviour
{
    public Button btnNext;          
    public Sprite goldenStar;     
    public Image Star1;             
    public Image Star2;             
    public Image Star3;             
    public Text txtScore;          
    public int levelNumber;         
    public int score;               
    public int ScoreForThreeStars;  
    public int ScoreForTwoStars;    
    public int ScoreForOneStar;   
    public int ScoreForNextLevel;   
    public float animStartDelay;  
    public float animDelay;       

    bool showTwoStars, showThreeStars;

    public string time;
    void Start()
    {
        score = GameCtrl.instance.GetScore();
        txtScore.text = "" + score;
        time = GameCtrl.instance.GetLevelCompletionTime();

        if(score >= ScoreForThreeStars)
        {
            showThreeStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 3, time);
            Invoke("ShowGoldenStars", animStartDelay);
            if (levelNumber == 2)
            {
                BossAI bossAI = GameObject.FindGameObjectWithTag("LevelOneBoss").GetComponent<BossAI>();
                bossAI.SetBossHealth(30); // Atur nyawa bos menjadi 30
            }
        }

        if (score >= ScoreForTwoStars && score < ScoreForThreeStars)
        {
            showTwoStars = true;
            GameCtrl.instance.SetStarsAwarded(levelNumber, 2, time);
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if (score <= ScoreForOneStar)
        {
            GameCtrl.instance.SetStarsAwarded(levelNumber, 1, time);
            Invoke("ShowGoldenStars", animStartDelay);
        }
    }

    void ShowGoldenStars()
    {
        StartCoroutine("HandleFirstStarAnim", Star1);
    }

    IEnumerator HandleFirstStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        if (showTwoStars || showThreeStars)
            StartCoroutine("HandleSecondStarAnim", Star2);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;

        if (showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
        else
            Invoke("CheckLevelStatus", 1.2f);
    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;
        Invoke("CheckLevelStatus", 1.2f);

    }
    void CheckLevelStatus()
    {
        if(score >= ScoreForNextLevel)
        {
            btnNext.interactable = true;
            SFXCtrl.instance.ShowBulletSparkle(btnNext.gameObject.transform.position);
            AudioCtrl.instance.KeyFound(btnNext.gameObject.transform.position);
            GameCtrl.instance.UnlockLevel(levelNumber);
        }
        else
        {
            btnNext.interactable = false;
        }
    }

    void DoAnim(Image starImg)
    {
        starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);
        starImg.sprite = goldenStar;
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(237f, 228f), 0.5f, false);
        AudioCtrl.instance.KeyFound(starImg.gameObject.transform.position);
        SFXCtrl.instance.ShowBulletSparkle(starImg.gameObject.transform.position); 
    }

    void Update()
    {

    }
}
