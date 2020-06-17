using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDemo : MonoBehaviour
{
    public Vector3 dot;
    public Vector3 startDot;

    public float spead;


    private void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(startDot, dot, spead);
    }
}
