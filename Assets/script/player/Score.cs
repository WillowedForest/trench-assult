using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI scoreText;
    
    private int score = 0;
    
    public void AddScore(int scoreIn)
    {
        score = score + scoreIn;
        scoreText.SetText("score:" + " " + score);
    }
}
