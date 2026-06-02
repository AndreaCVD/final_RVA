using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;

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

            //llame al script TickTicket i le pase el nombre del ingrediente
        }
        if(other.CompareTag("SpritePepper")) //nuevo
        {
            other.GetComponent<BoxCollider>().enabled = false;
            GameObject ingredient = other.gameObject;
            Debug.Log("Pepper no boxcoll");
            AttachIngredient(ingredient);
        }
        if (other.CompareTag("SpriteOregan")) //nuevo
        {
            other.GetComponent<BoxCollider>().enabled = false;
            GameObject ingredient = other.gameObject;
            Debug.Log("Oregan no boxcoll");
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

        // enable false the XRGrabInteractable
        XRGrabInteractable grab = ingredient.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            grab.enabled = false;
        }

        // aquesta linea se suposa es per arreglar el bug del [Near-Far Interactor] Dynamic Attach
        XRGeneralGrabTransformer trans = ingredient.GetComponent<XRGeneralGrabTransformer>();
        if (trans != null)
        {
            trans.enabled = false;
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