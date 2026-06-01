using UnityEngine;

public class ParticlesOnColl : MonoBehaviour
{
    public GameObject MeshPizza;
    public Material TomatoSauce;
    public Material WhiteSauce;
    //public GameObject pepperPrefab;
    //public GameObject oreganPrefab;
    public Transform attachPizza;
    public GameObject PizzaSocket;
    public GameObject EmptyTomatoeSauce;
    public GameObject EmptyWhiteSauce;

    private bool Tomatoe = false;
    private bool White = false;
    //private bool Pepper = false;
    //private bool Oregan = false;

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log($"Hay colision con: {other?.name}");
        if (other.CompareTag("SalsaTomatoe"))
        {
            if (Tomatoe) return;

            // Obtener el array actual de materiales
            Material[] mats = MeshPizza.GetComponent<MeshRenderer>().materials;
            // Asegurar que exista al menos índice 1
            if (mats.Length > 1)
            {
                mats[1] = TomatoSauce;
                MeshPizza.GetComponent<MeshRenderer>().materials = mats;
                Tomatoe = true;

                if (PizzaSocket != null && EmptyTomatoeSauce != null)
                {
                    GameObject tomatoSauceObject = Instantiate(EmptyTomatoeSauce, PizzaSocket.transform.position, Quaternion.identity);
                    tomatoSauceObject.transform.SetParent(PizzaSocket.transform);
                    tomatoSauceObject.transform.localPosition = Vector3.zero;
                    tomatoSauceObject.transform.localRotation = Quaternion.identity;
                }
            }
            else
            {
                Debug.LogWarning("El MeshRenderer no tiene material en índice 1");
            }
        }
        else if (other.CompareTag("WitheSauce"))
        {
            if (White) return;

            Material[] mats = MeshPizza.GetComponent<MeshRenderer>().materials;
            if (mats.Length > 1)
            {
                mats[1] = WhiteSauce;
                MeshPizza.GetComponent<MeshRenderer>().materials = mats;
                White = true;

                if (PizzaSocket != null && EmptyWhiteSauce != null)
                {
                    GameObject whiteSauceObject = Instantiate(EmptyWhiteSauce, PizzaSocket.transform.position, Quaternion.identity);
                    whiteSauceObject.transform.SetParent(PizzaSocket.transform);
                    whiteSauceObject.transform.localPosition = Vector3.zero;
                    whiteSauceObject.transform.localRotation = Quaternion.identity;
                }
            }
        }
            /*else if (other.CompareTag("Pepper"))
            {
                if (Pepper == true)
                {
                    return;
                }
                else
                {
                    Pepper = true;
                    //instanciar Sprite Pimienta i donarli el 
                    //AttachPizza transform + rotation + scale
                    GameObject newPepper = Instantiate(pepperPrefab, attachPizza.position, Quaternion.Euler(-90, 0, 0));
                }
            }
            else if (other.CompareTag("Oregan"))
            {
                if (Oregan == true)
                {
                    return;
                }
                else
                {
                    Oregan = true;
                    GameObject newPepper = Instantiate(oreganPrefab, attachPizza.position, Quaternion.Euler(-90, 0, 0));
                }
            }*/
    }
}
