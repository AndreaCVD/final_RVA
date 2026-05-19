using UnityEngine;

public class ScriptsManager : MonoBehaviour
{
    //Las cosas que no se van a destruir
    [SerializeField] GameObject world_manager;

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
}
