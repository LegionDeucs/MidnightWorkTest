using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : UnitMovementController
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private float gravityValue = -9.81f;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public override void MoveDirection(Vector3 moveDirection)
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        controller.Move(moveDirection * Time.deltaTime * playerSpeed);

        if (moveDirection != Vector3.zero)
            gameObject.transform.forward = moveDirection;

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}
