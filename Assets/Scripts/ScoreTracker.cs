using UnityEngine;
using TMPro;

public class ScoreTracker : MonoBehaviour
{
    public TMP_Text score;
    public TMP_Text streak;
    private void OnEnable()
    {
        Main.OnScoreChanged += RefreshScore;
        Main.OnStreakChanged += RefreshStreak;
    }

    private void OnDisable()
    {
        Main.OnScoreChanged -= RefreshScore;
        Main.OnStreakChanged -= RefreshStreak;
    }

    public void RefreshScore()
    {

        score.text = $"{Data.GamePlay.Score}\n";
        if (Data.GamePlay.ScoreMultiplier > 1)
        {
            score.text += string.Format("{0:0.0}", Data.GamePlay.ScoreMultiplier);
        }
    }
    public void RefreshStreak()
    {
        var alpha = (float)Mathf.Min((float)(Data.GamePlay.Streak / 10f), 1f);
        streak.color = new Color(streak.color.r, streak.color.g, streak.color.b, alpha);
        streak.text = $"x{Data.GamePlay.Streak}";
    }
}
