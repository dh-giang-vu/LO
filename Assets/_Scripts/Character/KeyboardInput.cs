using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector3 directionVector;
    private bool isGathering;
    private bool isMoving;
    private bool isSprinting;

    private CharacterAnimation characterAnimation;
    private Movement movement;
    private StaminaManager staminaManager;
    private InteractionHandler interactionHandler;
    [SerializeField] private Transform cameraTransform; // for calculating player's movement vector relative to camera's orientation

    void Start()
    {
        directionVector = Vector3.zero;
        isGathering = false;
        isMoving = false;
        isSprinting = false;

        characterAnimation = GetComponentInChildren<CharacterAnimation>();
        movement = GetComponent<Movement>();
        staminaManager = GetComponent<StaminaManager>();
        interactionHandler = GetComponentInChildren<InteractionHandler>();
    }

    void Update()
    {
        directionVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        directionVector = CalculateCameraRelativeMovement(directionVector);
        directionVector = Vector3.ClampMagnitude(directionVector, 1f);
        isMoving = !isGathering && directionVector != Vector3.zero;
        
        if (!isGathering)
        {
            isSprinting = CheckIfSprinting();
            movement.MoveCharacter(directionVector, isSprinting);
            characterAnimation.SetMovementAnimation(isMoving, isSprinting);
        }

        if (isSprinting)
        {
            staminaManager.ConsumeStamina();
        }
        else
        {
            staminaManager.RecoverStamina();
        }

        if (!isGathering && Input.GetKeyDown(KeyCode.E))
        {
            isGathering = true;
            string animationType = interactionHandler.GatherResources();
            characterAnimation.PlayGatheringAnimation(animationType);
            interactionHandler.RefuelLightSources();
        }
    }

    // Converts the input to be relative to the camera's direction
    private Vector3 CalculateCameraRelativeMovement(Vector3 inputVector)
    {
        // Get the forward direction of the camera (parallel to x-z plane)
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Get the right direction relative to the camera (parallel to x-z plane)
        Vector3 cameraRight = cameraTransform.right;
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Calculate the movement relative to the camera's forward and right directions
        Vector3 relativeMovement = inputVector.z * cameraForward + inputVector.x * cameraRight;
        return relativeMovement;
    }

    private bool CheckIfSprinting()
    {
        if (!isMoving)
        {
            return false;
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            return false;
        }
        if (!staminaManager.CanSprint())
        {
            return false;
        }
        return true;
    }

    void StopGathering() {
        isGathering = false;
        Debug.Log("StopGathering");
    }
}