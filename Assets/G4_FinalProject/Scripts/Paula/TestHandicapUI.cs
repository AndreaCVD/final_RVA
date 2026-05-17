using UnityEngine;
using UnityEngine.UI;

public class TestHandicapUI : MonoBehaviour
{
    public Button spawnButton;
    public Button deliverButton;
    public Slider pointsSlider;    // 0-100 punts que l'Andrea et donaria (descomptant ingredients)
    public Text pointsValueText;

    private void Start()
    {
        if (spawnButton) spawnButton.onClick.AddListener(() => HandicapManager.Instance.SpawnRandomHandicap());
        if (deliverButton) deliverButton.onClick.AddListener(SimulateDelivery);
        if (pointsSlider) pointsSlider.onValueChanged.AddListener((v) => pointsValueText.text = v.ToString("F0"));
    }

    private void SimulateDelivery()
    {
        int points = Mathf.RoundToInt(pointsSlider.value);
        HandicapManager.Instance.OnOrderDelivered(points);
    }
}