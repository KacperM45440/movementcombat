using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightController : MonoBehaviour
{
    private void FixedUpdate()
    {
        CheckHeight();
    }

    private void CheckHeight()
    {
        float newScale = Mathf.Clamp(transform.position.y + 0.5f, 1f, 1.5f);
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
