using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundController groundRef;
    [SerializeField] private Rigidbody bodyRef;
    private float moveX;
    private float moveY;
    private float jumpForce = 20f;
    private float baseSpeed = 4f;
    public enum MovementState
    {
        Run,
        Jump,
        Slide,
        Dash
    }
    private MovementState movementState;

    private void Start()
    {
        movementState = MovementState.Run;
    }

    private void Update()
    {
        TakeInput();
    }
    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }
    private void TakeInput() //Good enough for a quick project, would've gone with the New Input System otherwise
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide();
        }
    }

    private void Jump()
    {
        if (!groundRef.IsGrounded())
        {
            return;
        }
        movementState = MovementState.Jump;
        bodyRef.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        //StartCoroutine(WaitForLanding());
    }

    private void Slide()
    {

    }

    private void MovePlayer()
    {
        if (movementState == MovementState.Run)
        {
            bodyRef.velocity = baseSpeed * new Vector3(moveX, 0, moveY);
        }
        if (movementState == MovementState.Jump) 
        {
            //bodyRef.velocity += Vector3.down * 9.81f;
        }
    }
    private void RotatePlayer()
    {
        if (bodyRef.velocity.sqrMagnitude <= 0.01f)
        {
            return;
        }

        Vector3 direction = bodyRef.velocity;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
        }
    }

    public float GetVelocity()
    {
        float velocity = (bodyRef.velocity.magnitude < 0.01f) ? 0 : bodyRef.velocity.magnitude;
        return velocity;
    }

    public MovementState GetMovementState()
    {
        return movementState;
    }    

    private IEnumerator WaitForLanding()
    {
        yield return new WaitUntil(() => groundRef.IsGrounded() == true);
        movementState = MovementState.Run;
    }
}
