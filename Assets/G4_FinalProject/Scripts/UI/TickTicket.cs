using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Color = UnityEngine.Color;
public class TickTicket : MonoBehaviour
{
    //refrenecia a Images
    public Sprite Tomatoe;
    [SerializeField] LevelController levelController; //recetaActual

    //[SerializeField] Sprite check;
    //public List<SpriteRenderer> Imagenes = new List<SpriteRenderer>();
    //[SerializeField] TMP_Text op_fuerza;
    [SerializeField] List<TMP_Text> Pomodoro = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> Gorgonzola = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> Mozzarella = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> Parmigiano = new List<TMP_Text>();
    [SerializeField] List<TMP_Text> Fontina = new List<TMP_Text>();

    //public List<GameObject> ingredients = new List<GameObject>();
    private void Start()
    {
        
    }
    public void RevisarTicks(string ingrediente)
    {
        //tiene las letras mayusculas o minisculas en su nombre de TomatoeSauce
        //ver cada ingrediente que  tiene la receta actual
        //Si alguno coincide con el ingrediente que se ha mandado, se activa el tick del ticket
        foreach( string a in levelController.recetaActual.ingredients)
        {
            //Si coincide se envia a su respectivo
            if (ingrediente.Contains(a))
            {
                Tick(ingrediente);
            }
        }
        //cuando entra ingrediente en el socket - me pasa el nombre
        //si ese nombre coincide con el nombre de la referencia 
        //image con esa nombre cambio a imagetick


    }
    private void Tick(string ingrediente)
    {
        //Color A0 B8 0C --> 160, 184, 012
        Color verde = new Color(160, 184, 12);
        switch (ingrediente)
        {
            case string b when b.Contains("pomodo"):
                for (int i = 0; i > Pomodoro.Count; i--)
                {
                    Pomodoro[i].color = verde;
                }
                break;
            case string b when b.Contains("gorgonzola"):
                for (int i = 0; i > Gorgonzola.Count; i--)
                {
                    Gorgonzola[i].color = verde;
                }
                break;
            case string b when b.Contains("mozz"):
                for (int i = 0; i > Mozzarella.Count; i--)
                {
                    Mozzarella[i].color = verde;
                }
                break;
            case string b when b.Contains("parmigi"):
                for (int i = 0; i > Parmigiano.Count; i--)
                {
                    Parmigiano[i].color = verde;
                }
                break;
            case string b when b.Contains("fonti"):
                for (int i = 0; i > Fontina.Count; i--)
                {
                    Fontina[i].color = verde;
                }
                break;
            default:
                Debug.Log("No se ha hecho bien el check de la lista");
                break;
        }

    }
}
