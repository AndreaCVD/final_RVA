using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CompareCommand : MonoBehaviour
{
    public XRSocketInteractor socket;
    LevelController levelController;
    //Cuando entre la pizza a la caja hay que:
    // 1. Ver los hijos
    // 2. Comparar con la receta
    //Si uno coincide ya cuenta para bien, no pasa nada si hay 4 de peperoni

    void Start()
    {
        this.levelController = GetComponent<LevelController>();
        GameObject objBox = GameObject.Find("PizzaBox");
        socket = objBox.GetComponent<XRSocketInteractor>();

        // Se suscribe al evento cuando un objeto se conecta
        socket.selectEntered.AddListener(SelectEnterEvent);
    }
    void SelectEnterEvent(SelectEnterEventArgs obj)
    {
        //GameObject interactableGameObject = obj.interactable.gameObject; 
        GameObject a = obj.manager.gameObject;
        Debug.Log("Pizza preparada para entregar");

        // Aqu� puedes enviar datos o activar l�gica
        MirarPizzas();
    }

    public void MirarPizzas()
    {
        GameObject pizzaEntregada = GameObject.Find("PizzaBase");
        //Leemos cuantos ingredientes tiene la receta de la comanda
        for (int i = 0; i < levelController.recetaActual.ingredients.Count ; i++)
        {
            //Si alguno de estos ingredientes coincide, se ha hehco bien ese ingrediente
            //Si no coincide ninguno, es un ingrediente extra
        }
    }
}
