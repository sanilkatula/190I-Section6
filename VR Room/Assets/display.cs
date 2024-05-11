using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayDiceValue3D : MonoBehaviour
{
    public TextMeshPro textMesh; // Reference to the TextMeshPro 3D text component
    public diceRoll dice; // Add this line to declare the reference to the diceRoll script
    private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component from the GameObject
        if (dice != null) {
            dice.TopFaceValueChanged += HandleValueChanged; // Subscribe to the event
        }
    }

    void OnDestroy()
    {
        if (dice != null) {
            dice.TopFaceValueChanged -= HandleValueChanged; // Unsubscribe to prevent memory leaks
        }
    }

    private void HandleValueChanged(int newValue)
    {
        if (textMesh != null)
            textMesh.text = newValue.ToString(); // Update the text only when there is a change
        PlaySound(); // Play sound when the text updates
    }

    private void PlaySound()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Play the audio clip
        }
    }
}
