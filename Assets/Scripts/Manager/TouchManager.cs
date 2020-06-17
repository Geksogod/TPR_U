using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class TouchManager : MonoBehaviour
{
    private Camera mainCamera;
    private ITouchListener lastTouchTarget;
    private ITouchListener touchTarget;
    private bool colliderEnter;
    public Vector3 currentMouseRay;

    private static TouchManager s_Instance = null;

    public static TouchManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(TouchManager)) as TouchManager;
            }
            if (s_Instance == null)
            {
                var obj = new GameObject("BuildingManager");
                s_Instance = obj.AddComponent<TouchManager>();
            }

            return s_Instance;
        }
    }



    private void Start()
    {
        mainCamera = Camera.main;
    }
    void LateUpdate()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //currentMouseRay = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            currentMouseRay = hit.point;
            if (hit.collider?.gameObject.GetComponent<ITouchListener>() != null &&(touchTarget==null || hit.collider?.gameObject.GetComponent<ITouchListener>() == touchTarget))
            {
                touchTarget = hit.collider.gameObject.GetComponent<ITouchListener>();
                CheckForColliderEnter();
                CheckForColliderDown();
            }
            else if((hit.collider == null && touchTarget != null) || (touchTarget !=null && hit.collider != null && hit.collider?.gameObject.GetComponent<ITouchListener>() == null))
            {
                CheckForColliderExit();
            }

        }
    }

    private void CheckForColliderEnter()
    {
        if (lastTouchTarget == null || lastTouchTarget != touchTarget)
        {
            colliderEnter = true;
            lastTouchTarget = touchTarget;
            touchTarget.MouseEnter();
        }
        else if (colliderEnter)
            colliderEnter = false;
    }

    private void CheckForColliderDown()
    {
        if (touchTarget != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Click on collider");
                touchTarget.MouseDown();
            }
        }
    }

    private void CheckForColliderExit()
    {
        if (touchTarget != null)
        {
            touchTarget.MouseExit();
            lastTouchTarget = null;
            touchTarget = null;
        }
    }

}

public interface ITouchListener
{
    void MouseEnter();
    void MouseDown();
    void MouseExit();
}