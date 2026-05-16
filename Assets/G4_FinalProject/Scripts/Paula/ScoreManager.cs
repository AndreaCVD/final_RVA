using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int totalPoints = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddPoints(int points)
    {
        totalPoints += points;
        Debug.Log($"Points added: +{points}. Total: {totalPoints}");
    }

    public int GetTotalPoints() => totalPoints;
    public void ResetPoints() => totalPoints = 0;
}