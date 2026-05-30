using System.Collections.Generic;
using UnityEngine;

public class HandicapManager : MonoBehaviour
{
    public static HandicapManager instance;

    [Header("Handicap Prefabs")]
    [SerializeField] private GameObject[] handicap_prefabs;

    [Header("Spawn Area (inside food truck)")]
    [SerializeField] private Vector3 spawn_area_center = Vector3.zero;
    [SerializeField] private Vector3 spawn_area_size = new Vector3(4f, 1f, 3f);

    [Header("Penalty per unresolved handicap")]
    [SerializeField] private int penalty_per_unresolved = 20;

    [Header("Max unresolved handicaps allowed at once")]
    [SerializeField] private int max_unresolved_handicaps = 2;

    private List<IHandicap> active_handicaps = new List<IHandicap>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetUnresolvedCount()
    {
        int count = 0;
        foreach (var h in active_handicaps)
            if (!h.is_resolved) count++;
        return count;
    }

    public void SpawnRandomHandicap()
    {
        if (handicap_prefabs == null || handicap_prefabs.Length == 0)
        {
            Debug.LogWarning("No handicap prefabs assigned.");
            return;
        }
        if (GetUnresolvedCount() >= max_unresolved_handicaps)
        {
            Debug.Log($"Max unresolved handicaps reached ({max_unresolved_handicaps}). Not spawning.");
            return;
        }
        int idx = Random.Range(0, handicap_prefabs.Length);
        Vector3 pos = spawn_area_center + new Vector3(
            Random.Range(-spawn_area_size.x / 2, spawn_area_size.x / 2),
            Random.Range(0, spawn_area_size.y),
            Random.Range(-spawn_area_size.z / 2, spawn_area_size.z / 2)
        );
        GameObject go = Instantiate(handicap_prefabs[idx], pos, Quaternion.identity);
        IHandicap h = go.GetComponent<IHandicap>();
        if (h != null)
        {
            active_handicaps.Add(h);
            Debug.Log($"Handicap spawned: {go.name}");
        }
        else
        {
            Debug.LogError("Prefab does not have IHandicap. Destroyed.");
            Destroy(go);
        }
    }

    public void OnOrderDelivered(int points_earned)
    {
        int final_points = points_earned;
        int unresolved_count = 0;
        foreach (IHandicap handicap in active_handicaps)
        {
            if (!handicap.is_resolved)
            {
                final_points -= penalty_per_unresolved;
                unresolved_count++;
            }
        }
        if (unresolved_count > 0)
            Debug.Log($"Subtracted {unresolved_count * penalty_per_unresolved} points for unresolved handicaps.");
        if (final_points < 0) final_points = 0;
        ScoreManager.instance.AddPoints(final_points);
        active_handicaps.RemoveAll(h => h.is_resolved);
    }

    public void RemoveHandicap(IHandicap h)
    {
        if (active_handicaps.Contains(h))
        {
            active_handicaps.Remove(h);
            Debug.Log("Handicap resolved and removed from list.");
        }
    }
}