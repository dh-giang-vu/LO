using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    [SerializeField] private float normalSpeed = 6.0f;
    [SerializeField] private float sprintingSpeed = 9.0f;
    private float currentSpeed;
    private readonly float gravityValue = -9.81f;


    void Start()
    {
        currentSpeed = normalSpeed;
        controller = gameObject.GetComponent<CharacterController>();
    }

    public void MoveCharacter(Vector3 move, bool isSprinting)
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (isSprinting)
        {
            currentSpeed = sprintingSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        controller.Move(currentSpeed * Time.deltaTime * move);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}