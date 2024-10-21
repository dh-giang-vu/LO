using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;  // Include PlayableDirector for timeline control
using UnityEngine.SceneManagement;

public class DialogueScript : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // Text component to display dialogue
    public string[] lines; // Array of dialogue lines
    public float textSpeed; // Speed of typing text
    public float shakeMagnitude = 0.5f; // Magnitude of the shake effect
    public float shakeDuration = 0.05f; // Duration for each shake
    private int index; // Current index of the dialogue line

    // New variables for audio
    public AudioClip audioClip; // Audio clip to play
    private AudioSource audioSource; // AudioSource component
    [SerializeField, Range(0,1)] private float audioSourceVolume = 0.1f;

    // Reference to the PlayableDirector controlling the cutscene
    public PlayableDirector timeline; 

    void Start()
    {
        textComponent.text = string.Empty; // Clear text on start
        audioSource = gameObject.AddComponent<AudioSource>(); // Add an AudioSource component
        audioSource.clip = audioClip; // Assign the audio clip
        audioSource.volume = audioSourceVolume;
    }

    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.E)) // Check for E key press
        {
            if (textComponent.text == lines[index]) // If the text is fully displayed
            {
                NextLine(); // Go to the next line
            }
            else
            {
                StopAllCoroutines(); // Stop typing coroutine
                textComponent.text = lines[index]; // Show full text immediately
            }

            // Play audio clip when E is pressed
            audioSource.Play();
        }
    }

    public void StartDialogue()
    {
        
        Debug.Log("i should work");
        
        index = 0; // Reset index
        timeline.Pause(); // Pause the timeline here

        // Enable the dialogue UI components (image, text, character image)
        Transform dialogueUI = transform.Find("DialogueUI"); // Find child by name
        Transform textComponentTransform = transform.Find("Text"); // Find the Text object
        Transform textContinueTransform = transform.Find("ContinueText");
        Transform characterImage = transform.Find("CharacterImage"); // Find character image
        

        // Enable these components if they are found
        if (dialogueUI != null) dialogueUI.gameObject.SetActive(true);
        if (textComponentTransform != null) textComponentTransform.gameObject.SetActive(true);
        if (textContinueTransform != null) textContinueTransform.gameObject.SetActive(true);
        if (characterImage != null) characterImage.gameObject.SetActive(true);

        // Assuming Text is the component you want to display dialogue
        textComponent = textComponentTransform.GetComponent<TextMeshProUGUI>();
        
        // Start typing the dialogue text
        if (textComponent != null)
        {
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
    }


    IEnumerator TypeLine()
    {
        Vector3 originalPosition = textComponent.transform.localPosition; // Store original position

        foreach (char c in lines[index].ToCharArray()) // Type out each character
        {
            textComponent.text += c; // Append the character
            StartCoroutine(ShakeText(textComponent.transform)); // Start shaking for the current letter
            yield return new WaitForSeconds(textSpeed); // Wait for the specified speed
        }

        // Reset position after typing is done
        textComponent.transform.localPosition = originalPosition; 
    }

    IEnumerator ShakeText(Transform letterTransform)
    {
        Vector3 originalPosition = letterTransform.localPosition; // Store original position for the current letter
        float elapsed = 0f; // Timer to track the shake duration

        while (elapsed < shakeDuration)
        {
            // Generate random shake offsets
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            letterTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0); // Apply shake

            elapsed += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait for the next frame
        }

        // Reset position to the original after shaking
        letterTransform.localPosition = originalPosition;
    }

    void NextLine()
    {
        if (index < lines.Length - 1) // If there are more lines to show
        {
            index++; // Increment the index
            textComponent.text = string.Empty; // Clear the text
            StartCoroutine(TypeLine()); // Start typing the next line
        }
        else
        {
            EndDialogue(); // End the dialogue when all lines are done
        }
    }

    IEnumerator WaitForTimelineAndLoadScene()
    {
        // Wait until the timeline has finished playing
        while (timeline.state == PlayState.Playing)
        {
            yield return null; // Wait for the next frame
        }

        // Load the next scene
        SceneManager.LoadScene(2); // Replace with your actual scene name
    }
    void EndDialogue()
    {
        // Disable the dialogue UI components (image, text, character image)
        Transform dialogueUI = transform.Find("DialogueUI"); 
        Transform textComponentTransform = transform.Find("Text");
        Transform textContinueTransform = transform.Find("ContinueText");
        Transform characterImage = transform.Find("CharacterImage");

        // Disable these components
        if (dialogueUI != null) dialogueUI.gameObject.SetActive(false);
        if (textComponentTransform != null) textComponentTransform.gameObject.SetActive(false);
        if (textContinueTransform != null) textContinueTransform.gameObject.SetActive(false);
        if (characterImage != null) characterImage.gameObject.SetActive(false);

        // Resume the timeline
        if (timeline != null)
        {
            timeline.Play();
        }

        StartCoroutine(WaitForTimelineAndLoadScene());
    }

}
