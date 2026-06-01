using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Display (fix a l'escena)")]
    public Transform commandDisplay;
    public float slotSpacing = 0.3f;

    public int customersCounter = 0;

    [HideInInspector] public List<string> currentOrder = new List<string>();

    // LevelController crida aix� en el moment del spawn
    public void SetOrder(Recipe recipe, List<GameObject> prefabs)
    {
        currentOrder = new List<string>(recipe.ingredients);
        DisplayOrder(prefabs);
    }

    void DisplayOrder(List<GameObject> prefabs)
    {
        foreach (Transform child in commandDisplay)
            Destroy(child.gameObject);

        float totalWidth = (currentOrder.Count - 1) * slotSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < currentOrder.Count; i++)
        {
            GameObject prefab = prefabs.Find(p => p.name == currentOrder[i]);
            if (prefab == null)
            {
                Debug.LogWarning($"Prefab no trobat per ingredient: {currentOrder[i]}");
                continue;
            }
            Vector3 offset = new Vector3(startX + i * slotSpacing, 0, 0);
            GameObject slot = Instantiate(prefab, commandDisplay);
            slot.transform.localPosition = offset;
            slot.transform.localRotation = Quaternion.identity;
        }
    }

    public void DismissClient()
    {
        gameObject.SetActive(false);
    }
}