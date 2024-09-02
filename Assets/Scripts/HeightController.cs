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
        float newScale = Mathf.Lerp(1f, 1.5f, Mathf.Clamp01(transform.position.y * 0.333f));
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }
}
