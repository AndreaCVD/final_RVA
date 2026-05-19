using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PizzaAttach : MonoBehaviour
{
    // PUBLIC LIST - Stores all attached ingredients
    public List<GameObject> ingredients = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Ingredient" tag
        if (other.CompareTag("Ingredient"))
        {
            GameObject ingredient = other.gameObject;
            AttachIngredient(ingredient);
        }
    }

    // Attach ingredient to pizza - PERMANENTE
    void AttachIngredient(GameObject ingredient)
    {
        // Disable Rigidbody physics
        Rigidbody rb = ingredient.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // DESTROY the XRGrabInteractable (can never be grabbed again)
        XRGrabInteractable grab = ingredient.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            Destroy(grab);
        }

        // Change parent to pizza
        ingredient.transform.SetParent(transform);

        // Add to list
        ingredients.Add(ingredient);

        Debug.Log($"PERMANENT: {ingredient.name} attached | Total: {ingredients.Count}");
    }

    // Get all ingredients (for other scripts)
    public List<GameObject> GetAllIngredients()
    {
        return ingredients;
    }
}