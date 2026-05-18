using System.Collections.Generic;
using UnityEngine;

public class PizzaAttach : MonoBehaviour
{
    // PUBLIC LIST - Stores all attached ingredients
    public List<GameObject> ingredients = new List<GameObject>();

    // When an ingredient enters the pizza
    void OnTriggerEnter(Collider other)
    {
        // Check if the object has the "Ingredient" tag
        if (other.CompareTag("Ingredient"))
        {
            GameObject ingredient = other.gameObject;

            // Check if not already attached
            if (!ingredients.Contains(ingredient))
            {
                AttachIngredient(ingredient);
            }
        }
    }

    // Attach ingredient to pizza (just change parent!)
    void AttachIngredient(GameObject ingredient)
    {
        // Disable Rigidbody physics while attached
        Rigidbody rb = ingredient.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        // JUST CHANGE PARENT - that's it!
        ingredient.transform.SetParent(transform);

        // Add to list
        ingredients.Add(ingredient);

        Debug.Log($"ATTACHED: {ingredient.name} | Total: {ingredients.Count}");
    }

    // Remove ingredient (call this when grabbing from pizza)
    public void RemoveIngredient(GameObject ingredient)
    {
        if (ingredients.Contains(ingredient))
        {
            // Re-enable physics
            Rigidbody rb = ingredient.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
            }

            // Remove parent (detach from pizza)
            ingredient.transform.SetParent(null);

            // Remove from list
            ingredients.Remove(ingredient);

            Debug.Log($"REMOVED: {ingredient.name} | Remaining: {ingredients.Count}");
        }
    }

    // Get all ingredients (for other scripts)
    public List<GameObject> GetAllIngredients()
    {
        return ingredients;
    }
}