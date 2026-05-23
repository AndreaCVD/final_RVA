using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class HandicapTestUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text totalPointsText;
    public TMP_Text activeHandicapsText;
    public Slider pizzaPointsSlider;
    public Button startOrderButton;
    public Button deliverButton;
    public Button spawnManualButton;

    [Header("Simulation Settings")]
    public float minSpawnDelay = 3f;
    public float maxSpawnDelay = 8f;
    public int maxHandicapsPerOrder = 2;

    private bool orderActive = false;
    private int handicapsSpawnedThisOrder = 0;
    private Coroutine spawnCoroutine;

    private void Start()
    {
        InvokeRepeating(nameof(UpdateUI), 0f, 0.5f);

        startOrderButton.onClick.AddListener(StartOrder);
        deliverButton.onClick.AddListener(DeliverPizza);
        spawnManualButton.onClick.AddListener(() => HandicapManager.instance.SpawnRandomHandicap());
    }

    void UpdateUI()
    {
        if (ScoreManager.instance != null)
            totalPointsText.text = $"Total Points: {ScoreManager.instance.GetTotalPoints()}";

        if (HandicapManager.instance != null)
        {
            int unresolved = HandicapManager.instance.GetUnresolvedCount();
            activeHandicapsText.text = $"Unresolved Handicaps: {unresolved}";
        }
    }

    void StartOrder()
    {
        if (orderActive) return;
        orderActive = true;
        handicapsSpawnedThisOrder = 0;
        Debug.Log("=== New order started ===");
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnDuringOrder());
    }

    IEnumerator SpawnDuringOrder()
    {
        while (orderActive && handicapsSpawnedThisOrder < maxHandicapsPerOrder)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSeconds(delay);
            if (!orderActive) break;
            HandicapManager.instance.SpawnRandomHandicap();
            handicapsSpawnedThisOrder++;
            Debug.Log($"Handicap spawned #{handicapsSpawnedThisOrder} (max {maxHandicapsPerOrder})");
        }
    }

    void DeliverPizza()
    {
        if (!orderActive) return;
        orderActive = false;
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);

        int basePoints = Mathf.RoundToInt(pizzaPointsSlider.value);
        Debug.Log($"Delivering pizza with {basePoints} base points.");
        HandicapManager.instance.OnOrderDelivered(basePoints);
        Debug.Log("Order finished.");
    }
}