using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distractores : MonoBehaviour
{
    public GameObject[] distracciones;
    public float activationTimeLimit = 15f;

    private void Start()
    {
        // Start the coroutine to activate random objects
        StartCoroutine(ActivateRandomObject());
    }

    private IEnumerator ActivateRandomObject()
    {
        while (true)
        {
            // Wait for a random time within the limit
            yield return new WaitForSeconds(Random.Range(0, activationTimeLimit));

            // Activate a random object from the array and deactivate others
            ActivateRandomDistraccion();
        }
    }

    private void ActivateRandomDistraccion()
    {
        // Check if there are any objects in the array
        if (distracciones.Length > 0)
        {
            // Deactivate all objects first
            foreach (GameObject distraccion in distracciones)
            {
                distraccion.SetActive(false);
            }

            // Activate a random object
            int randomIndex = Random.Range(0, distracciones.Length);
            distracciones[randomIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No objects in the 'distracciones' array.");
        }
    }
}