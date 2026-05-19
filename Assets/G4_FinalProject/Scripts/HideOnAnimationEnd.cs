using UnityEngine;

public class HideOnAnimationEnd : MonoBehaviour
{
    [SerializeField] private GameObject cubeToHide;

    // Llama esto al final de la animación
    public void HideCube()
    {
        cubeToHide.SetActive(false);
    }
}