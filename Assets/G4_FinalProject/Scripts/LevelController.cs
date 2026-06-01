// LevelController.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Recipe recetaActual;

    [Header("Configuració del nivell")]
    public float timePerNPC = 30f;
    public int totalNPCs = 5;
    public List<Recipe> availableRecipes;

    [Header("Persiana")]
    public float blindsAnimationDuration = 10f; // ajusta segons la durada

    [Header("Spawn")]
    public GameObject npcPrefab;
    public Transform spawnPoint;
    public Transform commandDisplay;

    private int npcSpawnedCount = 0;
    private NPCController currentNPC;

    [Header("Ingredients")]
    public List<GameObject> ingredientPrefabs; // todos los prefabs aquí

    [Header("Timer")]
    public Timer timer;

    // --- Llamado desde MainLoop ---
    public void SetupLevel(float timePerNPC, int totalNPCs, List<Recipe> recipes)
    {
        this.timePerNPC = timePerNPC;
        this.totalNPCs = totalNPCs;
        this.availableRecipes = recipes;
        npcSpawnedCount = 0;

        Debug.Log($"Nivell configurat: {totalNPCs} NPCs, {timePerNPC}s per NPC");

        StartCoroutine(WaitForBlindsAndSpawn());
    }

    public void SpawnNextNPC() 
    {
        if (npcSpawnedCount >= totalNPCs)
        {
            Debug.Log("Tots els NPCs han estat atesos. Nivell acabat!");
            return;
        }

        // Instancia el NPC al spawnPoint
        GameObject npcObject = Instantiate(npcPrefab, spawnPoint.position, spawnPoint.rotation);
        npcObject.name = "CLIENTE";
        currentNPC = npcObject.GetComponent<NPCController>();

        // Assigna CommandDisplay (referència de l'escena)
        currentNPC.commandDisplay = commandDisplay;

        // Assigna la comanda + prefabs
        Recipe recipe = GetRandomRecipe();
        if (recipe != null)
            currentNPC.SetOrder(recipe, ingredientPrefabs);
        else
            Debug.LogWarning("No hi ha receptes disponibles al LevelController!");

        npcSpawnedCount++;
        recetaActual = recipe;
        Debug.Log($"NPC {npcSpawnedCount}/{totalNPCs} spawnejat amb recepta: {recipe?.recipeName}");

        //Comenzar el timer
        if(timer != null)
        {
            timer.SetTime(timePerNPC);
        } else
        {
            Debug.LogWarning("LevelController: no hay Timer asignado en el Inspector");
        }
    }

    IEnumerator WaitForBlindsAndSpawn()
    {
        yield return new WaitForSeconds(blindsAnimationDuration);
        SpawnNextNPC();
    }

    public Recipe GetRandomRecipe()
    {
        if (availableRecipes == null || availableRecipes.Count == 0) return null;
        return availableRecipes[Random.Range(0, availableRecipes.Count)];
    }

    // --- Cridat pel Timer o Validació quan el NPC marxa ---
    public void OnClientDismissed() //
    {
        if (currentNPC != null)
            currentNPC.DismissClient(); //aquest va a NPC controller - enable false

        SpawnNextNPC(); // busca un npc (activo), y instancia uno nuevo si no
    }

}