using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distractores : MonoBehaviour
{
    public GameObject[] distracciones;

    public void StartDistractor()
    {
        // Start the coroutine to activate random objects
        StartCoroutine(ActivateRandomObject());
    }

    public void StopDistractor()
    {
        StopAllCoroutines();
    }

    private IEnumerator ActivateRandomObject()
    {
        while (true)
        {
            // Wait for a random time within the limit
            yield return new WaitForSeconds(5f);

            // Activate a random object from the array and deactivate others
            ActivateRandomDistraccion();
        }
    }

    private int _prevIndex = 0;

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

            while (randomIndex == this._prevIndex)
            {
                randomIndex = Random.Range(0, distracciones.Length);
            }

            distracciones[randomIndex].SetActive(true);
            this._prevIndex = randomIndex;

            //_distractionIndex = (_distractionIndex + 1) % this.distracciones.Length;
            //this.distracciones[_distractionIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("No objects in the 'distracciones' array.");
        }
    }
}