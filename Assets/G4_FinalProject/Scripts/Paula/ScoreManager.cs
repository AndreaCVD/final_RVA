using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int total_points = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddPoints(int points)
    {
        total_points += points;
        Debug.Log($"Points added: +{points}. Total: {total_points}");
    }

    public int GetTotalPoints() => total_points;
    public void ResetPoints() => total_points = 0;
}