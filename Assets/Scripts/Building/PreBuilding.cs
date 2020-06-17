using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuilding : MonoBehaviour
{
    public BuildingData baseObject;
    [SerializeField]
    private Material greenMaterial;
    [SerializeField]
    private Material redMaterial;
    private bool tooLow;
    private bool isTriggered;
    private MeshRenderer meshRenderer;
    [SerializeField]
    private bool isReadyToBuild;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.material = greenMaterial;
    }

    private void Update()
    {
        transform.position = TouchManager.instance.currentMouseRay;
        tooLow = transform.localPosition.y < -1;
        isReadyToBuild = !tooLow && !isTriggered;
        if (isReadyToBuild)
        {

            meshRenderer.material = greenMaterial;
            if (Input.GetMouseButtonDown(0))
            {
                GameObject newBuild = GameObject.Instantiate(baseObject.GetGameObjectFromState(0), transform.position, transform.rotation);
                newBuild.AddComponent<MeshCollider>();
                var taskTarget = newBuild.AddComponent<TaskTarget>();
                newBuild.AddComponent<Building>().SetBuildingData(baseObject);
                taskTarget.taskType = TaskManager.TaskType.BuildBuilding;
                TaskManager.instance.AddTask(taskTarget);
                this.gameObject.SetActive(false);
            }
        }
        else
            meshRenderer.material = redMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != 8)
        {
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 8)
        {
            isTriggered = false;
        }
    }
}
