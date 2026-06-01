using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] CompareCommand compare;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pizza")
        {
            compare.MirarPizzas(other.gameObject);
        }
    }
}
