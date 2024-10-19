using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace

public class CraftUIMove : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Image backgroundToFade; // New serialized field for the UI image
    [SerializeField, Range(0f, 1f)] private float maxTransparency = 0.5f; // Set default max transparency to 50%

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool objectIsDown = true; // Track if the object is down
    private float movementDuration = 1f; // Duration of movement
    private float movementProgress = 0f; // Track how far we are in the movement
    private bool isMovingObject = false; // Track if the object is moving

    private bool isFadingIn = false; // Track if we are fading in
    private bool isFadingOut = false; // Track if we are fading out
    private float fadeProgress = 0f; // Track the progress of the fade

    private void Start()
    {
        // Store the original position of objectToMove
        originalPosition = objectToMove.transform.position;

        // Move the object 500 units down at the start
        objectToMove.transform.position = originalPosition + new Vector3(0, -Screen.height, 0);
        objectIsDown = true; // Set the initial state to down

        // Initialize the background image to be fully transparent
        Color imageColor = backgroundToFade.color;
        imageColor.a = 0f; // Set alpha to 0
        backgroundToFade.color = imageColor;
    }

    private void Update()
    {
        // Check if the user wants to move the object up or down
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleObjectPosition();
        }

        // Handle movement of objectToMove
        if (isMovingObject)
        {
            movementProgress += Time.deltaTime / movementDuration; // Increment progress based on time
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, targetPosition, movementProgress);

            // Stop moving if we reached the target position
            if (movementProgress >= 1f)
            {
                isMovingObject = false; // Stop the movement
                movementProgress = 0f; // Reset progress
            }
        }

        // Handle fading in and out of the background
        if (isFadingIn)
        {
            FadeInBackground();
        }
        else if (isFadingOut)
        {
            FadeOutBackground();
        }
    }

    // Method to toggle the object's position between up and down
    public void ToggleObjectPosition()
    {
        // Check the current position of objectToMove
        if (Vector3.Distance(objectToMove.transform.position, originalPosition + new Vector3(0, -Screen.height, 0)) < 0.1f)
        {
            // If it's close to the down position, move it back up to its original position
            targetPosition = originalPosition;
            objectIsDown = false; // Update the state
            isFadingIn = true; // Start fading in
            fadeProgress = 0f; // Reset fade progress
        }
        else
        {
            // Move object down by screen height
            targetPosition = originalPosition + new Vector3(0, -Screen.height, 0);
            objectIsDown = true; // Update the state
            isFadingOut = true; // Start fading out
            fadeProgress = 0f; // Reset fade progress
        }

        isMovingObject = true; // Start the movement
        movementProgress = 0f; // Reset movement progress
    }

    // Fade in the background image
    private void FadeInBackground()
    {
        fadeProgress += Time.deltaTime / movementDuration; // Increment fade progress
        Color imageColor = backgroundToFade.color;
        imageColor.a = Mathf.Lerp(0f, maxTransparency, fadeProgress); // Fade to specified max transparency
        backgroundToFade.color = imageColor;

        // Stop fading in when fully opaque
        if (fadeProgress >= 1f)
        {
            isFadingIn = false; // Stop fading in
            fadeProgress = 0f; // Reset fade progress
        }
    }

    // Fade out the background image
    private void FadeOutBackground()
    {
        fadeProgress += Time.deltaTime / movementDuration; // Increment fade progress
        Color imageColor = backgroundToFade.color;
        imageColor.a = Mathf.Lerp(maxTransparency, 0f, fadeProgress); // Fade to transparent
        backgroundToFade.color = imageColor;

        // Stop fading out when fully transparent
        if (fadeProgress >= 1f)
        {
            isFadingOut = false; // Stop fading out
            fadeProgress = 0f; // Reset fade progress
        }
    }
}