using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PizzaSocket : MonoBehaviour
{
    // PUBLIC LIST - This stores all the ingredients on the pizza
    public List<GameObject> ingredients = new List<GameObject>();

    private XRSocketInteractor socket;

    void Start()
    {
        // Get the Socket component on the pizza
        socket = GetComponent<XRSocketInteractor>();

        // LISTEN TO EVENTS: When something enters or exits the socket
        socket.selectEntered.AddListener(OnIngredientAdded);
        socket.selectExited.AddListener(OnIngredientRemoved);
    }

    // This happens WHEN an ingredient is PUT ON the pizza
    void OnIngredientAdded(SelectEnterEventArgs args)
    {
        // The ingredient that was just added
        GameObject newIngredient = args.interactableObject.transform.gameObject;

        // ADD to the list
        ingredients.Add(newIngredient);

        // Message to confirm it worked
        Debug.Log($"ADDED: {newIngredient.name} | Total: {ingredients.Count}");
    }

    // This happens WHEN an ingredient is TAKEN OFF the pizza
    void OnIngredientRemoved(SelectExitEventArgs args)
    {
        // The ingredient that was just removed
        GameObject removedIngredient = args.interactableObject.transform.gameObject;

        // REMOVE from the list
        ingredients.Remove(removedIngredient);

        // Message to confirm it worked
        Debug.Log($"REMOVED: {removedIngredient.name} | Remaining: {ingredients.Count}");
    }
}