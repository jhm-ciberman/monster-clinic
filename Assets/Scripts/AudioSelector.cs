using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    // Public AudioSource variable to store the Audio Source component
    public AudioSource audioSource;

    // Public array of AudioClips to choose from
    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the AudioSource component is assigned
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is not assigned. Please assign it in the inspector.");
            return;
        }

        // Check if the array of AudioClips is not empty
        if (audioClips.Length > 0)
        {
            // Select a random AudioClip from the array
            AudioClip selectedClip = audioClips[Random.Range(0, audioClips.Length)];

            // Assign the selected AudioClip to the AudioSource component
            audioSource.clip = selectedClip;

            // Play the audio
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No AudioClips are assigned. Please assign AudioClips to the array in the inspector.");
        }
    }
}
