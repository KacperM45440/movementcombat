using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField] private GroundController controllerRef;
    [SerializeField] private TMP_Text groundValue;
    [Space]
    [SerializeField] private PlayerMovement movementRef;
    [SerializeField] private TMP_Text velocityValue;

    private void Update()
    {
        UpdateValues();
    }

    private void UpdateValues()
    {
        groundValue.text = controllerRef.IsGrounded().ToString();
        velocityValue.text = movementRef.GetVelocity().ToString("F3");
    }
}
