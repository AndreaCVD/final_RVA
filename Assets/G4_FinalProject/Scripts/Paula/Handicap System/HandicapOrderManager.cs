using System.Collections;
using UnityEngine;

public class HandicapOrderManager : MonoBehaviour
{
    [SerializeField] private Timer levelTimer;
    [SerializeField] private float min_spawn_delay = 3f;
    [SerializeField] private float max_spawn_delay = 8f;
    [SerializeField] private int max_handicaps_per_order = 2;

    private Coroutine spawn_coroutine;
    private bool order_active = false;
    private int handicaps_spawned = 0;
    private bool order_completed = false;   // <-- NOVA VARIABLE

    private void OnEnable()
    {
        Debug.Log("HandicapOrderManager: OnEnable");
        if (levelTimer != null)
        {
            Debug.Log("HandicapOrderManager: levelTimer asignado, añadiendo listeners");
            levelTimer.onOrderStarted.AddListener(OnOrderStarted);
            levelTimer.onTimeFinished.AddListener(OnTimeFinished);
        }
        else
        {
            Debug.LogError("HandicapOrderManager: levelTimer es NULL! Asigna el Timer en el inspector.");
        }
    }

    private void OnDisable()
    {
        if (levelTimer != null)
        {
            levelTimer.onOrderStarted.RemoveListener(OnOrderStarted);
            levelTimer.onTimeFinished.RemoveListener(OnTimeFinished);
        }
    }

    private void OnOrderStarted()
    {
        Debug.Log("🎯 HandicapOrderManager: OnOrderStarted RECIBIDO");
        order_active = true;
        handicaps_spawned = 0;
        order_completed = false;       // Reiniciem per a la nova comanda
        if (spawn_coroutine != null) StopCoroutine(spawn_coroutine);
        spawn_coroutine = StartCoroutine(SpawnDuringOrder());
    }

    private IEnumerator SpawnDuringOrder()
    {
        while (order_active && handicaps_spawned < max_handicaps_per_order)
        {
            float delay = Random.Range(min_spawn_delay, max_spawn_delay);
            yield return new WaitForSeconds(delay);
            if (!order_active) break;
            HandicapManager.instance.SpawnRandomHandicap();
            handicaps_spawned++;
            Debug.Log($"Handicap spawned #{handicaps_spawned}");
        }
    }

    private void OnTimeFinished()
    {
        if (order_completed) return;   // Ja s'ha entregat la pizza abans, no tornem a penalitzar
        order_completed = true;
        if (!order_active) return;
        order_active = false;
        if (spawn_coroutine != null) StopCoroutine(spawn_coroutine);
        HandicapManager.instance.OnOrderDelivered(0);
        Debug.Log("HandicapOrderManager: Order finished (timeout)");
    }

    // Mètode públic perquè el pugui cridar CompareCommand
    public void OnOrderFinished()
    {
        if (order_completed) return;
        order_completed = true;
        if (!order_active) return;
        order_active = false;
        if (spawn_coroutine != null) StopCoroutine(spawn_coroutine);
        // No cridem OnOrderDelivered aquí perquè ja ho ha fet CompareCommand amb els punts reals
        Debug.Log("HandicapOrderManager: Order finished by delivery");
    }
}