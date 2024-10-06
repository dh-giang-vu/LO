using UnityEngine;

public class CraftUIMove : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool objectIsDown = true; // Track if the object is down
    private float movementDuration = 1f; // Duration of movement
    private float movementProgress = 0f; // Track how far we are in the movement
    private bool isMovingObject = false; // Track if the object is moving

    private void Start()
    {
        // Store the original position of objectToMove
        originalPosition = objectToMove.transform.position;

        // Move the object 500 units down at the start
        objectToMove.transform.position = originalPosition + new Vector3(0, -500, 0);
        objectIsDown = true; // Set the initial state to down
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
    }

    // Method to toggle the object's position between up and down
    private void ToggleObjectPosition()
    {
        // Check the current position of objectToMove
        if (Vector3.Distance(objectToMove.transform.position, originalPosition + new Vector3(0, -500, 0)) < 0.1f)
        {
            // If it's close to the down position, move it back up to its original position
            targetPosition = originalPosition;
            objectIsDown = false; // Update the state
        }
        else
        {
            // Move object down 500 units
            targetPosition = originalPosition + new Vector3(0, -500, 0);
            objectIsDown = true; // Update the state
        }

        isMovingObject = true; // Start the movement
        movementProgress = 0f; // Reset progress
    }
}
