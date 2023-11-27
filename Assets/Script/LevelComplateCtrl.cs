using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelComplateCtrl : MonoBehaviour
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
    void Start()
    {
        //score = GameCtrl.instance.GetScore(); 
        txtScore.text = "" + score;

        if(score >= ScoreForThreeStars)
        {
            showThreeStars = true;
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if (score >= ScoreForTwoStars && score < ScoreForThreeStars)
        {
            showTwoStars = true;
            Invoke("ShowGoldenStars", animStartDelay);
        }

        if (score <= ScoreForOneStar && score != 0)
        {
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
    }

    IEnumerator HandleSecondStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        showTwoStars = false;

        if (showThreeStars)
            StartCoroutine("HandleThirdStarAnim", Star3);
    }

    IEnumerator HandleThirdStarAnim(Image starImg)
    {
        DoAnim(starImg);
        yield return new WaitForSeconds(animDelay);

        showThreeStars = false;

    }

    void DoAnim(Image starImg)
    {
        starImg.rectTransform.sizeDelta = new Vector2(150f, 150f);
        starImg.sprite = goldenStar;
        RectTransform t = starImg.rectTransform;
        t.DOSizeDelta(new Vector2(100f, 100f), 0.5f, false);
        AudioCtrl.instance.KeyFound(starImg.gameObject.transform.position);
        SFXCtrl.instance.ShowBulletSparkle(starImg.gameObject.transform.position); 
    }

    void Update()
    {

    }
}
