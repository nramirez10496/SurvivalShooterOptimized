using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreManager : MonoBehaviour
{
    public static int score { get; private set;}
    public static event Action<int> OnScoreChanged;

    void Awake ()
    {
        score = 0;
        OnScoreChanged?.Invoke(score);
    }
    
    public static void AddScore(int amount)
    {
        score += amount;
        OnScoreChanged?.Invoke(score);
    }
}
