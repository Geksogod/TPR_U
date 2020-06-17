using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxSetting : MonoBehaviour
{
    [SerializeField]
    private bool isRotate;
    [SerializeField]
    private float rotateSpeed;

    private void FixedUpdate()
    {
        if (isRotate)
        {
            RenderSettings.skybox.SetFloat("_Rotation",Time.time*rotateSpeed);
        }
    }
}
