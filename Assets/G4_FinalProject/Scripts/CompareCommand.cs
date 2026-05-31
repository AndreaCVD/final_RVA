using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CompareCommand : MonoBehaviour
{
    public XRSocketInteractor socket;
    private LevelController levelController;
    public int maxPointsPerPizza = 100;

    [Header("Assigna des de l'inspector")]
    public HandicapOrderManager orderManager; // Arrossegar manualment
    private HandicapManager handicapManager;

    private void Start()
    {
        levelController = GetComponent<LevelController>();
        GameObject objBox = GameObject.Find("PizzaBox");
        if (objBox != null)
        {
            socket = objBox.GetComponent<XRSocketInteractor>();
            if (socket != null)
                socket.selectEntered.AddListener(SelectEnterEvent);
        }

        handicapManager = HandicapManager.instance;
        if (orderManager == null)
            Debug.LogWarning("CompareCommand: HandicapOrderManager no assignat manualment. Intentant FindObjectOfType...");
        if (orderManager == null)
            orderManager = FindObjectOfType<HandicapOrderManager>();
    }

    private void SelectEnterEvent(SelectEnterEventArgs args)
    {
        Debug.Log("Pizza preparada per entregar");
        MirarPizzas();
    }

    public void MirarPizzas()
    {
        GameObject pizzaEntregada = GameObject.Find("BasePizza");
        GameObject ingredientesEntregados = GameObject.Find("PizzaSocket");

        if (pizzaEntregada == null || ingredientesEntregados == null)
        {
            Debug.LogWarning("No s'ha trobat BasePizza o PizzaSocket");
            return;
        }

        // Obtenir llista d'ingredients esperats des de la recepta actual
        List<string> expected = new List<string>();
        if (levelController.recetaActual != null)
        {
            foreach (string ing in levelController.recetaActual.ingredients)
            {
                expected.Add(NormalizeName(ing));
            }
        }
        else
        {
            Debug.LogError("No hi ha recepta actual (recetaActual és null)");
            return;
        }

        // Obtenir llista d'ingredients entregats (fills de PizzaSocket)
        List<string> delivered = new List<string>();
        foreach (Transform child in ingredientesEntregados.transform)
        {
            string name = NormalizeName(child.gameObject.name);
            delivered.Add(name);
            Debug.Log($"Ingredient entregat: {name}");
        }

        Debug.Log($"Ingredients esperats: {string.Join(", ", expected)}");
        Debug.Log($"Ingredients entregats: {string.Join(", ", delivered)}");

        // Comparació
        List<string> expectedCopy = new List<string>(expected);
        int correctCount = 0;
        foreach (string d in delivered)
        {
            if (expectedCopy.Contains(d))
            {
                correctCount++;
                expectedCopy.Remove(d);
            }
        }
        int errors = delivered.Count - correctCount;
        int totalExpected = expected.Count;
        int pointsPerIngredient = maxPointsPerPizza / Mathf.Max(1, totalExpected);
        int basePoints = correctCount * pointsPerIngredient;

        Debug.Log($"Entrega validada: {correctCount}/{totalExpected} correctes, {errors} errors. Punts base: {basePoints}");

        // Aplicar puntuació i penalitzacions
        if (handicapManager != null)
            handicapManager.OnOrderDelivered(basePoints);
        else
            Debug.LogError("HandicapManager no disponible");

        // Aturar spawn de handicaps
        if (orderManager != null)
            orderManager.OnOrderFinished();

        // Destruir pizza
        Destroy(pizzaEntregada);
        levelController.DismissAnim();
        levelController.OnClientDismissed();
    }

    private string NormalizeName(string name)
    {
        // Elimina "(Clone)" i espais
        name = name.Replace("(Clone)", "").Trim();
        return name;
    }
}