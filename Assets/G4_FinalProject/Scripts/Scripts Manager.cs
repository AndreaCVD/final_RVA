using UnityEngine;
using UnityEngine.UIElements;

public class ScriptsManager : MonoBehaviour
{
    [Header("Script que no se destruye")]
    [SerializeField] GameObject world_manager;

    [Header("Script que no se destruye")]
    [SerializeField] GameObject pizza;
    [SerializeField] Transform pizza_position;

    void Awake()
    {
        if (!GameObject.Find("--World Management--") && !GameObject.Find("--World Management--(Clone)"))
        {
            GameObject new_obj = Instantiate(world_manager);
            new_obj.name = "--Preload--";
            DontDestroyOnLoad(new_obj);
        }
        else
        {
            GameObject new_obj = GameObject.Find("--World Management--");
            DontDestroyOnLoad(new_obj);
        }
    }

    private void Update()
    {
        if (!GameObject.Find("BasePizza") && !GameObject.Find("BasePizza(Clone)"))
        {
            Instantiate(pizza, pizza_position.position, pizza_position.rotation);
        }
    }
}
