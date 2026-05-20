using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PizzaSalsa : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    // Materiales referenciados desde el inspector
    public Material nuevoMaterial;  // Asigna este material en el inspector

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("ENTREA");
        // Verificar si el objeto tiene el tag "Pizza"
        if (other.CompareTag("Pizza"))
        {
            // Buscar el componente MeshRenderer
            MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();

            // Cambiar el material si existe el MeshRenderer
            if (meshRenderer != null && nuevoMaterial != null)
            {
                meshRenderer.material = nuevoMaterial;
                Debug.Log("Material cambiado en: " + other.name);
            }
            else
            {
                Debug.LogWarning("No se encontró MeshRenderer" + other.name);
            }
        }
    }
}