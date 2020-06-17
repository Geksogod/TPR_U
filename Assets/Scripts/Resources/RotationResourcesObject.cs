using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationResourcesObject : MonoBehaviour
{
    [SerializeField]
    [Range(0,3)]
    private float rotationFactor;
    private bool direction;
    private float myRotationTime;

    private void Start()
    {
        myRotationTime = Random.Range(0.1f, rotationFactor);
    }
    private void FixedUpdate()
    {
        if (direction)
        {
            if (myRotationTime <= rotationFactor)
            {
                myRotationTime += Time.deltaTime * 0.4f;
            }
            else
            {
                direction = false;
            }
        }
        else
        {
            if (myRotationTime >= -1*rotationFactor)
            {
                myRotationTime -= Time.deltaTime *0.4f;
            }
            else
            {
                direction = true;
            }
        }
        Vector3 rot = gameObject.transform.localRotation.eulerAngles;
        rot.Set(0f, 0f, myRotationTime);
        gameObject.transform.localRotation = Quaternion.Euler(rot);
    }
}
