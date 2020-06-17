using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static BuildManager s_Instance = null;

    public static BuildManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(BuildManager)) as BuildManager;
            }
            if (s_Instance == null)
            {
                var obj = new GameObject("BuildManager");
                s_Instance = obj.AddComponent<BuildManager>();
            }

            return s_Instance;
        }
    }

    public GameObject testCube;
    public BuildingData[] buildingData = new BuildingData[] { };

    public void ActiveBuildPhase(BuildingManager.BuildingType buildingType)
    {
        ResourcesSpend(buildingType);
        testCube.SetActive(true);
        testCube.GetComponent<PreBuilding>().baseObject = buildingData.FirstOrDefault(a => a.GetBuildingType() == buildingType).GetBuildingData();
        var tempMesh = buildingData.FirstOrDefault(a => a.GetBuildingType() == buildingType).GetMestFromState(0);
        testCube.GetComponent<MeshFilter>().sharedMesh = tempMesh;
        testCube.GetComponent<MeshCollider>().sharedMesh = tempMesh;
    }

    public bool ResourcesIsEnough(BuildingManager.BuildingType buildingType)
    {
        var resourcesNeed = buildingData.FirstOrDefault(a => a.GetBuildingType() == buildingType).GetBuildingResources();

        for (int i = 0; i < resourcesNeed.Length; i++)
        {
            if (!ResourcesManager.instance.IsEnough(resourcesNeed[i].resourcesType, resourcesNeed[i].count))
                return false;
        }
        return true;
    }

    private void ResourcesSpend(BuildingManager.BuildingType buildingType)
    {
        if (!ResourcesIsEnough(buildingType))
            throw new System.Exception("Resources not enought");
        var resourcesNeed = buildingData.FirstOrDefault(a => a.GetBuildingType() == buildingType).GetBuildingResources();

        for (int i = 0; i < resourcesNeed.Length; i++)
        {
            ResourcesManager.instance.GetResources(resourcesNeed[i].resourcesType, resourcesNeed[i].count);
        }
    }
}
