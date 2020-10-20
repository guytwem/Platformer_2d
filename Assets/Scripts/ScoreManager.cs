using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI killText;
    
    int score;
    int killScore;
    

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    public void ChangeScore(int coinValue)
    {
        score += coinValue;
        coinText.text = "Coins: " + score.ToString();
    }

    public void KillCounter(int killValue)
    {
        killScore += killValue;
        killText.text = "Kills: " + killScore.ToString();
    }

   
}

