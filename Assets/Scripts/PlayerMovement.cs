using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundController groundRef;
    [SerializeField] private Rigidbody bodyRef;
    [SerializeField] private Animator animatorRef;
    private float moveX;
    private float moveZ;
    private float jumpForce = 5f;
    private float baseSpeed = 5f;
    private float stopSpeed = 1.5f;
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
        moveZ = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Slide();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (movementState == MovementState.Slide)
            {
                Dash();
            }
            else
            {
                Jump();
            }   
        }
    }

    private void Jump()
    {
        if (!groundRef.IsGrounded())
        {
            return;
        }

        if (movementState == MovementState.Jump)
        {
            return;
        }

        movementState = MovementState.Jump;
        StartCoroutine(JumpCoroutine());
    }

    private void Slide()
    {
        if (!groundRef.IsGrounded())
        {
            return;
        }

        if (movementState == MovementState.Slide)
        {
            return;
        }

        movementState = MovementState.Slide;
        StartCoroutine(SlideCoroutine());
    }

    private void Dash()
    {
        if (!groundRef.IsGrounded())
        {
            return;
        }

        if (movementState == MovementState.Dash)
        {
            return;
        }

        movementState = MovementState.Dash;
        StartCoroutine(DashCoroutine());
    }

    private void MovePlayer()
    {
        stopSpeed = movementState switch
        {
            MovementState.Run => 1.5f,
            MovementState.Jump => 1.5f,
            MovementState.Slide => 6f,
            MovementState.Dash => 0f,
            _ => 1.5f,
        };

        //bool isMoving = Mathf.Abs(moveX) > 0 || Mathf.Abs(moveZ) > 0;
        //if (isMoving && movementState == MovementState.Run)
        if (movementState == MovementState.Run)
        {
            Vector3 targetVelocity = new Vector3(moveX, 0, moveZ).normalized * baseSpeed;
            Vector3 currentVelocity = new(bodyRef.velocity.x, 0, bodyRef.velocity.z);
            Vector3 velocityChange = targetVelocity - currentVelocity;

            bodyRef.AddForce(velocityChange, ForceMode.VelocityChange);
        }
        else
        {
            Vector3 currentVelocity = new(bodyRef.velocity.x, 0, bodyRef.velocity.z);
            Vector3 deceleration = stopSpeed * Time.deltaTime * -currentVelocity.normalized;

            if (deceleration.magnitude > currentVelocity.magnitude)
            {
                bodyRef.velocity = new Vector3(0, bodyRef.velocity.y, 0);
            }
            else
            {
                bodyRef.AddForce(deceleration, ForceMode.VelocityChange);
            }
        }
    }
    private void RotatePlayer()
    {
        if (bodyRef.velocity.sqrMagnitude <= 0.01f)
        {
            return;
        }

        Vector3 direction = new Vector3(moveX, 0, moveZ);
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

    private IEnumerator JumpCoroutine()
    {
        bodyRef.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        yield return new WaitUntil(() => groundRef.IsGrounded() == false);
        yield return new WaitUntil(() => groundRef.IsGrounded() == true);
        movementState = MovementState.Run;
    }

    private IEnumerator SlideCoroutine()
    {
        animatorRef.SetTrigger("StartSlide");
        Vector3 targetVelocity = new Vector3(moveX, 0, moveZ).normalized * baseSpeed;
        bodyRef.AddForce(targetVelocity, ForceMode.VelocityChange);
        yield return new WaitUntil(() => bodyRef.velocity.magnitude <= 1.5f);
        animatorRef.SetTrigger("EndSlide");
        movementState = MovementState.Run;
    }

    private IEnumerator DashCoroutine()
    {
        Vector3 targetVelocity = new Vector3(moveX, jumpForce, moveZ).normalized * baseSpeed;
        bodyRef.AddForce(targetVelocity, ForceMode.VelocityChange);
        yield return new WaitUntil(() => groundRef.IsGrounded() == false);
        yield return new WaitUntil(() => groundRef.IsGrounded() == true);
        animatorRef.SetTrigger("EndSlide");
        movementState = MovementState.Run;
    }
}
