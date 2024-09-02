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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == groundLayer)
        {
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grounded = false; //&= ?
    }

    public bool IsGrounded()
    {
        return grounded;
    }
}
