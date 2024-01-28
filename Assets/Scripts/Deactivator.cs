using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : MonoBehaviour
{
  public float deactivationTimer = 3f;

    private void Start()
    {
        // Start the coroutine to deactivate the GameObject after a specified time
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        // Wait for the specified deactivation timer duration
        yield return new WaitForSeconds(deactivationTimer);

        // Deactivate the entire GameObject
        gameObject.SetActive(false);
    }
}
