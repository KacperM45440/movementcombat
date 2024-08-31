using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    private bool grounded = false;
    private int groundLayer;

    void Awake()
    {
        groundLayer = LayerMask.NameToLayer("Ground");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false; //&= ?
    }

    public bool IsGrounded()
    {
        return grounded;
    }
}
