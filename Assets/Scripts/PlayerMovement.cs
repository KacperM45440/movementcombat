using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody bodyRef;
    private int moveX;
    private int moveY;
    private float baseSpeed = 4f;

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
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1;
        }
    }
    private void MovePlayer()
    {
        bodyRef.velocity = new Vector3(moveX, 0, moveY) * baseSpeed;
        moveX = 0;
        moveY = 0;
    }
    private void RotatePlayer()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(bodyRef.velocity), 0.15f);
    }
}
