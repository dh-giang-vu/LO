using UnityEngine;
using UnityEngine.UI; // Make sure to include the UI namespace

public class CraftUIMove : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Image backgroundToFade; // New serialized field for the UI image
    [SerializeField, Range(0f, 1f)] private float maxTransparency = 0.5f; // Set default max transparency to 50%

    private Vector3 centerScreenPosition; // Center of the screen
    private Vector3 offScreenPosition; // Position off the bottom of the screen
    private float movementDuration = 1f; // Duration of movement
    private float movementProgress = 0f; // Track how far we are in the movement
    private bool isMovingObject = false; // Track if the object is moving

    private bool isFadingIn = false; // Track if we are fading in
    private bool isFadingOut = false; // Track if we are fading out
    private float fadeProgress = 0f; // Track the progress of the fade
    private bool objectIsDown = true; // Track if the object is down

    private void Start()
    {
        // Initialize the background image to be fully transparent
        Color imageColor = backgroundToFade.color;
        imageColor.a = 0f; // Set alpha to 0
        backgroundToFade.color = imageColor;

        // Calculate positions initially based on screen size
        UpdatePositions();
        // Set initial position off-screen
        objectToMove.transform.position = offScreenPosition;
    }

    private void Update()
    {
        // Continuously update screen positions to ensure responsiveness to resolution changes
        UpdatePositions();

        // Check if the user wants to move the object up or down
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleObjectPosition();
        }

        // Handle movement of objectToMove
        if (isMovingObject)
        {
            movementProgress += Time.deltaTime / movementDuration; // Increment progress based on time
            objectToMove.transform.position = Vector3.Lerp(objectToMove.transform.position, objectIsDown ? offScreenPosition : centerScreenPosition, movementProgress);

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
        if (objectIsDown)
        {
            // Move it to the center of the screen
            objectIsDown = false; // Update the state
            isFadingIn = true; // Start fading in
            fadeProgress = 0f; // Reset fade progress
        }
        else
        {
            // Move object off the bottom of the screen
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

    // Recalculate positions based on screen size
    private void UpdatePositions()
    {
        centerScreenPosition = new Vector3(Screen.width / 2f, Screen.height / 2f, objectToMove.transform.position.z);
        offScreenPosition = centerScreenPosition + new Vector3(0, -Screen.height, 0);
    }
}
