using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Image = System.Drawing.Image;
public class TickTicket : MonoBehaviour
{
    //refrenecia a Images
    public Sprite Tomatoe;
    [SerializeField] LevelController levelController; //recetaActual

    [SerializeField] Sprite check;
    //public List<SpriteRenderer> Imagenes = new List<SpriteRenderer>();

    public List<Image> Pomodoro = new List<Image>();
    public List<SpriteRenderer> Gorgonzola = new List<SpriteRenderer>();
    public List<SpriteRenderer> Mozzarella = new List<SpriteRenderer>();
    public List<SpriteRenderer> Parmigiano = new List<SpriteRenderer>();
    public List<SpriteRenderer> Fontina = new List<SpriteRenderer>();

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
        switch (ingrediente)
        {
            case string b when b.Contains("pomodo"):
                for (int i = 0; i > Pomodoro.Count; i--)
                {
                    Pomodoro[i].color = Color.red;

                    //Pomodoro[i].sprite = check;
                }
                break;
            case string b when b.Contains("gorgonzola"):
                for (int i = 0; i > Gorgonzola.Count; i--)
                {
                    Gorgonzola[i].sprite = check;
                }
                break;
            case string b when b.Contains("mozz"):
                for (int i = 0; i > Mozzarella.Count; i--)
                {
                    Mozzarella[i].sprite = check;
                }
                break;
            case string b when b.Contains("parmigi"):
                for (int i = 0; i > Parmigiano.Count; i--)
                {
                    Parmigiano[i].sprite = check;
                }
                break;
            case string b when b.Contains("fonti"):
                for (int i = 0; i > Fontina.Count; i--)
                {
                    Fontina[i].sprite = check;
                }
                break;
            default:
                Debug.Log("No se ha hecho bien el check de la lista");
                break;
        }

    }
}
