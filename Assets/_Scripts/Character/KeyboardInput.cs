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
            characterAnimation.PlayGatheringAnimation();
            interactionHandler.GatherResources();
            interactionHandler.RefuelLightSources();
        }
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