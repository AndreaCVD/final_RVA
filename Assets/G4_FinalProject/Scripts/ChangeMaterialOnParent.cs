using UnityEngine;

public class ChangeMaterialOnParent : MonoBehaviour
{
    public Material newMaterial;  // El material de salsa

    // Este método será llamado desde el evento OnEnter del OnTrigger
    public void ChangeParentMaterial(GameObject triggeredObject)
    {
        // El objeto que entra es Pizzazona (hijo). Su padre es la Pizza.
        Transform parent = triggeredObject.transform.parent;
        if (parent != null)
        {
            MeshRenderer mr = parent.GetComponent<MeshRenderer>();
            if (mr != null && newMaterial != null)
            {
                mr.material = newMaterial;
                Debug.Log("Material cambiado en la pizza: " + parent.name);
            }
        }
    }
}