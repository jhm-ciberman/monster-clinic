using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Public variable to store the scene name
    public string sceneToLoad;

    // Method to load the scene
    public void CargarJuego()
    {
        // Check if the scene name is not empty or null
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty or null. Please set a valid scene name in the inspector.");
        }
    }
}
