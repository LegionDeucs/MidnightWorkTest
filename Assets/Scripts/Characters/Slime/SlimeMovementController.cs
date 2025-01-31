using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlimeMovementController : UnitMovementController
{
    [SerializeField] private Vector2 randomMovementDelay;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector2 randomJumpForceStrength;

    [SerializeField] private Transform groundCheckersBox;

    private Coroutine jumpRoutine;

    private void Start()
    {
        jumpRoutine = StartCoroutine(RandomMoveCall());
    }

    private bool IsGrounded
    {
        get
        {
            if (Time.frameCount != GroundChecker.timeFrame)
            {
                GroundChecker.timeFrame = Time.frameCount;
                GroundChecker.isGrounded = Physics.OverlapBox(groundCheckersBox.position, groundCheckersBox.localScale / 2).Length > 0;
            }
            return GroundChecker.isGrounded;
        }
    }
    private GroundChecker GroundChecker;

    public override void MoveDirection(Vector3 moveDirection)
    {
        if (IsGrounded)
            rb.AddForce((moveDirection.normalized + Vector3.up) * Random.Range(randomJumpForceStrength.x, randomJumpForceStrength.y), ForceMode.Impulse);
    }

    internal void StartMovingRandom()
    {
        if (jumpRoutine != null)
            jumpRoutine = StartCoroutine(RandomMoveCall());
    }

    public void StopRandomMoving()
    {
        if(jumpRoutine != null)
        {
            StopCoroutine(jumpRoutine);
            jumpRoutine = null;
        }
    }

    private IEnumerator RandomMoveCall()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(randomMovementDelay.x, randomMovementDelay.y));
            MoveDirection(Random.insideUnitCircle.normalized.YtoZ());
        }
    }
}

public struct GroundChecker
{
    public bool isGrounded;
    public int timeFrame;
}
