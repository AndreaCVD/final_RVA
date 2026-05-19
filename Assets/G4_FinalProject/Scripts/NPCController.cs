using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Comanda")]
    public List<GameObject> ingredientPrefabs;
    public float slotSpacing = 0.3f;  

    [Header("Display (fix a l'escena)")]
    public Transform commandDisplay; // arrossega el CommandDisplay de l'escena

    [HideInInspector] public List<string> currentOrder = new List<string>();

    void Start()
    {
        GenerateOrder();
        DisplayOrder();
    }

    void GenerateOrder()
    {
        currentOrder.Clear();

        List<GameObject> shuffled = new List<GameObject>(ingredientPrefabs);
        shuffled.Sort((a, b) => Random.Range(-1, 2));

        int count = Random.Range(2, 5);
        for (int i = 0; i < count && i < shuffled.Count; i++)
            currentOrder.Add(shuffled[i].name);
    }

    void DisplayOrder()
    {
        foreach (Transform child in commandDisplay)
            Destroy(child.gameObject);

        float totalWidth = (currentOrder.Count - 1) * slotSpacing;
        float startX = -totalWidth / 2f;

        for (int i = 0; i < currentOrder.Count; i++)
        {
            GameObject prefab = ingredientPrefabs.Find(p => p.name == currentOrder[i]);
            if (prefab == null) continue;

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