using System.Collections.Generic;
using UnityEngine;

public class HandicapManager : MonoBehaviour
{
    public static HandicapManager Instance;

    [Header("Handicap Prefabs")]
    [SerializeField] private GameObject[] handicapPrefabs;

    [Header("Spawn Area (dins del foodtruck)")]
    [SerializeField] private Vector3 spawnAreaCenter = Vector3.zero;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(4f, 1f, 3f);

    [Header("Penalització per handicap no resolt")]
    [SerializeField] private int penaltyPerUnresolvedHandicap = 20; // restarà 20 punts per cada handicap no resolt

    private List<IHandicap> activeHandicaps = new List<IHandicap>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Cridat per l'Andrea (o qui sigui) per generar un handicap
    public void SpawnRandomHandicap()
    {
        if (handicapPrefabs == null || handicapPrefabs.Length == 0)
        {
            Debug.LogWarning("No handicap prefabs assignats.");
            return;
        }

        int idx = Random.Range(0, handicapPrefabs.Length);
        Vector3 pos = spawnAreaCenter + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(0, spawnAreaSize.y),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        GameObject go = Instantiate(handicapPrefabs[idx], pos, Quaternion.identity);
        IHandicap h = go.GetComponent<IHandicap>();
        if (h != null)
        {
            activeHandicaps.Add(h);
            Debug.Log($"Handicap spawned: {go.name}");
        }
        else
        {
            Debug.LogError("El prefab no té IHandicap. S'ha destruït.");
            Destroy(go);
        }
    }

    // Cridat per l'Andrea quan entrega la pizza. pointsEarned ja inclou penalització per ingredients (0-100)
    public void OnOrderDelivered(int pointsEarned)
    {
        int finalPoints = pointsEarned;

        // Restar punts per cada handicap actiu NO resolt
        int unresolvedCount = 0;
        foreach (IHandicap handicap in activeHandicaps)
        {
            if (!handicap.IsResolved)
            {
                finalPoints -= penaltyPerUnresolvedHandicap;
                unresolvedCount++;
            }
        }

        if (unresolvedCount > 0)
            Debug.Log($"S'han restat {unresolvedCount * penaltyPerUnresolvedHandicap} punts per handicaps no resolts.");

        if (finalPoints < 0) finalPoints = 0;

        ScoreManager.Instance.AddPoints(finalPoints);

        // Netejar handicaps que s'hagin resolt (per si de cas)
        activeHandicaps.RemoveAll(h => h.IsResolved);
    }

    // Cridat des del mateix handicap quan es resol
    public void RemoveHandicap(IHandicap h)
    {
        if (activeHandicaps.Contains(h))
        {
            activeHandicaps.Remove(h);
            Debug.Log("Handicap resolt i eliminat de la llista.");
        }
    }
}