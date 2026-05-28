using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{

    public void ChangeScene(string scene_name) //Anar a una escena en especific
    {
        //Leer escena actual
        Scene active_scene = SceneManager.GetActiveScene();
        if (active_scene.name == "combat_scene")
        {
            // Unload Scene
            SceneManager.UnloadSceneAsync(active_scene);
        }
        else
        {
            //Load Scene
            SceneManager.LoadScene(scene_name);
        }
    }
    public void Exit() //Anar al menu principal
    {

    }
}
