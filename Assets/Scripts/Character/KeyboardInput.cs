using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    private Vector3 directionVector;
    private bool isGathering;
    private bool isRunning;

    private CharacterAnimation characterAnimation;
    private Movement movement;

    void Start()
    {
        directionVector = Vector3.zero;
        isGathering = false;
        isRunning = directionVector == Vector3.zero;

        characterAnimation = GetComponentInChildren<CharacterAnimation>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (!isGathering)
        {
            directionVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            directionVector = Vector3.ClampMagnitude(directionVector, 1f);
            movement.MoveCharacter(directionVector);

            isRunning = directionVector != Vector3.zero;
            characterAnimation.SetAnimation(isRunning);
        }

        if (!isGathering && Input.GetKeyDown(KeyCode.E))
        {
            isGathering = true;
            characterAnimation.PlayGatheringAnimation();
        }
    }

    void StopGathering() {
        isGathering = false;
    }
}
