using UnityEngine;
using UnityEngine.UI;


public class ScoreUI : MonoBehaviour
{
    [SerializeField] Text scoreText;

    private void OnEnable()
    {
        ScoreManager.OnScoreChanged += UpdateScore;

        UpdateScore(ScoreManager.score);
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateScore;
    }

    void UpdateScore(int newScore)
    {
        scoreText.text="Score: "+newScore;
    }
}
