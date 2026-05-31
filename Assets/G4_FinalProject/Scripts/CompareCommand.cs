using System.Collections.Generic;
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
    public List<GameObject> Correctos = new List<GameObject>();
    public List<GameObject> Incorrectos = new List<GameObject>();

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
        GameObject pizzaEntregada = GameObject.Find("BasePizza");
        GameObject ingredientesEntregados = GameObject.Find("PizzaSocket");

        //Leemos cuantos ingredientes tiene la receta de la comanda

        //Si alguno de estos ingredientes coincide, se ha hehco bien ese ingrediente
        //Si no coincide ninguno, es un ingrediente extra
        string lastName = "";
        foreach (Transform child in ingredientesEntregados.transform)
        {
            GameObject childObj = child.gameObject;

            for (int i = 0; i < levelController.recetaActual.ingredients.Count; i++)
            {
                if ( childObj.name == levelController.recetaActual.ingredients[i] && childObj.name != lastName)
                {
                    Correctos.Add(childObj);
                    lastName = childObj.name;
                }
                else if (childObj.name != lastName)
                {
                    Incorrectos.Add(childObj);
                    lastName = childObj.name;
                }
            }
        }
        Debug.Log("Se cierra la caja de entrega");
        Debug.Log("Se destruye la pizza");
        Destroy(pizzaEntregada);

    }
}